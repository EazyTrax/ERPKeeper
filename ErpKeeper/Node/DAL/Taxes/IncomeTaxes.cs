using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Company;
using ERPKeeper.Node.Models.Taxes;
using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using ERPKeeper.Node.Models.Taxes.Enums;

namespace ERPKeeper.Node.DAL.Taxes
{
    public class IncomeTaxes : ERPNodeDalRepository
    {
        public IncomeTaxes(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.IncomeTax;
        }

        public List<IncomeTax> GetList()
        {
            return erpNodeDBContext.IncomeTaxes.ToList();
        }
        public IncomeTax Find(Guid id) => erpNodeDBContext.IncomeTaxes.Find(id);
        public IncomeTax Create()
        {
            var taxDate = DateTime.Now;

            var newIncomeTax = new IncomeTax()
            {
                Uid = Guid.NewGuid(),
                TrDate = taxDate,
                FiscalYearUid = organization.FiscalYears.Find(taxDate).Uid,
            };
            erpNodeDBContext.IncomeTaxes.Add(newIncomeTax);
            erpNodeDBContext.SaveChanges();
            return newIncomeTax;

        }
        public bool Delete(Guid id)
        {
            var incomeTax = this.Find(id);
            if (incomeTax.PostStatus != LedgerPostStatus.Locked)
            {
                erpNodeDBContext.IncomeTaxes.Remove(incomeTax);
                this.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool Edit(Guid id)
        {
            var incomeTax = this.Find(id);
            if (incomeTax.PostStatus == LedgerPostStatus.Locked)
            {
                this.UnPost(incomeTax);
                this.SaveChanges();
                return true;
            }
            return false;
        }
        public List<IncomeTax> GetAssignAble(ERPObjectType transactionType, DateTime transactionDate)
        {
            var taxPeriods = erpNodeDBContext.IncomeTaxes
                .Where(s => s.TrDate >= transactionDate)
                .OrderBy(s => s.TrDate)
                .ToList();
            return taxPeriods;
        }
        public IncomeTax Save(Models.Taxes.IncomeTax salesTax)
        {
            var existSalesTax = erpNodeDBContext.IncomeTaxes.Find(salesTax.Uid);

            if (existSalesTax.PostStatus == LedgerPostStatus.Locked)
                return existSalesTax;

            existSalesTax.TrDate = new DateTime(salesTax.TrDate.Year, salesTax.TrDate.Month, 1).AddMonths(1).AddDays(-1);
            erpNodeDBContext.SaveChanges();

            return existSalesTax;
        }
        public IncomeTax Create(Guid taxCodeUid)
        {
            var newCoperateTax = new IncomeTax()
            {
                Uid = Guid.NewGuid(),
                TrDate = DateTime.Now
            };
            erpNodeDBContext.IncomeTaxes.Add(newCoperateTax);
            erpNodeDBContext.SaveChanges();

            return newCoperateTax;
        }
        public void UpdateStartDate()
        {
            Console.WriteLine("> Generate Sales Tax");

            this.GetList()
                .ToList()
                .ForEach(a =>
                {
                    this.Save(a);
                });

            erpNodeDBContext.SaveChanges();
        }
        public List<IncomeTax> GetReadyForPost()
        {
            return erpNodeDBContext.IncomeTaxes
.Where(s => s.PostStatus == LedgerPostStatus.Editable).ToList();
        }
        public List<IncomeTax> GetLedger(IncomeTax taxPeriod) => erpNodeDBContext.IncomeTaxes
            .Where(tr => tr.Uid == taxPeriod.Uid)
            .ToList();
        public void ReOrder()
        {
            var transactions = erpNodeDBContext.IncomeTaxes
                .OrderBy(t => t.TrDate)
                .ToList();

            int i = 1;
            transactions.ForEach(tr => tr.No = i++);

            erpNodeDBContext.SaveChanges();
        }
        public void PostLedger()
        {
            var postingTransactions = this.GetReadyForPost();
            Console.WriteLine($"> Post {this.transactionType} [{postingTransactions.Count()}]");

            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = false;
            postingTransactions.ForEach(s =>
            {
                this.PostLedger(s, false);
            });
            erpNodeDBContext.Configuration.AutoDetectChangesEnabled = true;

            erpNodeDBContext.ChangeTracker.DetectChanges();
            erpNodeDBContext.SaveChanges();
        }
        public bool PostLedger(IncomeTax tr, bool SaveImmediately = true)
        {
            if (tr.PostStatus == LedgerPostStatus.Locked)
                return false;

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Uid = tr.Uid,
                TrnDate = tr.TrDate,
                TransactionName = tr.Name,
                TransactionNo = tr.No,
                TransactionType = transactionType
            };

            trLedger.Credit(tr.LiabilityAccount, tr.TaxAmount);
            trLedger.Debit(tr.IncomeTaxExpenAccount, tr.TaxAmount);

            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                tr.PostStatus = LedgerPostStatus.Locked;
            }
            else
            {
                return false;
            }

            if (SaveImmediately)
                erpNodeDBContext.SaveChanges();

            return true;
        }
        public void UnPost(IncomeTax tax)
        {
            if (tax.PostStatus == LedgerPostStatus.Locked)
            {


                organization.LedgersDal.RemoveTransaction(tax.Uid);
                tax.PostStatus = LedgerPostStatus.Editable;
                erpNodeDBContext.SaveChanges();
            }
        }
        public void UnPost()
        {
            Console.WriteLine("> Un Post" + ERPObjectType.IncomeTax.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            erpNodeDBContext.IncomeTaxes.ToList().ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }
    }
}