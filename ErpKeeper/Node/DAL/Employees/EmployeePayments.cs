using ERPKeeper.Node.DAL.Transactions;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Employees;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.DAL.Employees
{
    public class EmployeePayments : ERPNodeDalRepository
    {
        public EmployeePayments(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.EmployeePayment;
        }

        public Models.Employees.EmployeePayment Find(Guid id) => erpNodeDBContext.EmployeePayments.Find(id);

        public IQueryable<EmployeePayment> GetQuery() => erpNodeDBContext.EmployeePayments;

        public void Reorders()
        {
            int i = 0;
            erpNodeDBContext.EmployeePayments.OrderBy(ep => ep.EmployeePaymentPeriod.TrnDate).ThenBy(ep => ep.No).ToList()
                .ForEach(ep => ep.No = ++i);
            erpNodeDBContext.SaveChanges();
        }

        public EmployeePayment Save(EmployeePayment employeePayment)
        {
            var existPayment = erpNodeDBContext.EmployeePayments.Find(employeePayment.Uid);
            existPayment.EmployeePaymentPeriodUid = employeePayment.EmployeePaymentPeriodUid;
            existPayment.PayFromAccountGuid = employeePayment.PayFromAccountGuid ?? organization.SystemAccounts.Cash.Uid;
            existPayment.Calculate();
            SaveChanges();
            return existPayment;
        }

        private int GetNextNumber() => (erpNodeDBContext.EmployeePayments.Max(e => (int?)e.No) ?? 0) + 1;

        public List<EmployeePayment> GetReadyForPost()
        {
            return erpNodeDBContext.EmployeePayments
                    .Where(s => s.EmployeePaymentPeriod.TrnDate >= organization.FirstDate)
                    .Where(s => s.PostStatus == LedgerPostStatus.Editable)
                    .Where(s => s.Status != EmployeePaymentStatus.Void)
                    .ToList();
        }

        public void PostLedger()
        {
            var postingTransactions = GetReadyForPost();
            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            postingTransactions.ForEach(s =>
            {
                PostLedger(s, false);
            });

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;
            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        public bool PostLedger(EmployeePayment tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            tr.Calculate();

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Uid = tr.Uid,
                TrnDate = tr.EmployeePaymentPeriod.TrnDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType,
                ProfileName = tr.Employee.Profile.Name

            };

            if (tr.PayFromAccount == null)
                tr.PayFromAccount = organization.SystemAccounts.Cash;

            trLedger.Credit(tr.PayFromAccount, tr.TotalPayment);
            PostEanringToGL(trLedger, tr);
            PostDeductionToGL(trLedger, tr);

            if (trLedger.FinalValidate())
            {
                tr.PostStatus = LedgerPostStatus.Locked;
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
            }

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
        }

        public bool Delete(Guid id)
        {
            var payment = organization.EmployeePayments.Find(id);

            if (payment.PostStatus != LedgerPostStatus.Locked)
            {
                erpNodeDBContext.EmployeePayments.Remove((EmployeePayment)payment);
                organization.SaveChanges();
                return true;
            }
            return false;
        }


        public EmployeePayment Copy(EmployeePayment originalEmployeePayment, DateTime paydate)
        {
            var cloneEmployeePayment = erpNodeDBContext.EmployeePayments
                    .AsNoTracking()
                    .Include(p => p.PaymentItems)
                    .FirstOrDefault(x => x.Uid == originalEmployeePayment.Uid);

            cloneEmployeePayment.Uid = Guid.NewGuid();
            cloneEmployeePayment.No = organization.EmployeePayments.GetNextNumber();
            cloneEmployeePayment.PostStatus = LedgerPostStatus.Editable;
            cloneEmployeePayment.PaymentItems.ToList().ForEach(ci => ci.Uid = Guid.NewGuid());
            cloneEmployeePayment.EmployeePaymentPeriodUid = organization.EmployeePaymentPeriods.FindByDate(paydate, true).Uid;

            erpNodeDBContext.EmployeePayments.Add(cloneEmployeePayment);
            erpNodeDBContext.SaveChanges();

            return cloneEmployeePayment;
        }
        private void PostEanringToGL(Models.Accounting.TransactionLedger trLedger, EmployeePayment employeePayment)
        {
            foreach (var paymentItem in employeePayment.PaymentItems.Where(t => t.PaymentType.PayDirection == Models.Employees.PayDirection.Eanring).ToList())
            {
                trLedger.Debit(paymentItem.PaymentType.Account, paymentItem.Amount);
            }
        }

        public EmployeePayment CreateNew(Guid id, DateTime trDate)
        {
            var newEmployeePayment = new EmployeePayment()
            {
                Uid = Guid.NewGuid(),
                EmployeeUid = id,

            };

            erpNodeDBContext.EmployeePayments.Add(newEmployeePayment);
            organization.SaveChanges();

            return newEmployeePayment;
        }

        private void PostDeductionToGL(Models.Accounting.TransactionLedger trLedger, EmployeePayment employeePayment)
        {
            var paymentDeductionItems = employeePayment.PaymentItems
                .Where(t => t.PaymentType.PayDirection == Models.Employees.PayDirection.Deduction)
                .ToList();

            foreach (var paymentItem in paymentDeductionItems)
            {
                trLedger.Credit(paymentItem.PaymentType.Account, paymentItem.Amount);
            }
        }
        public void UnPost(EmployeePayment employeePayment)
        {
            organization.LedgersDal.RemoveTransaction(employeePayment.Uid);
            employeePayment.PostStatus = LedgerPostStatus.Editable;
            organization.SaveChanges();

        }
        public void UnPost()
        {
            Console.WriteLine("> Un Post " + transactionType.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            erpNodeDBContext.EmployeePayments.ToList().ForEach(b => b.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }


    }
}