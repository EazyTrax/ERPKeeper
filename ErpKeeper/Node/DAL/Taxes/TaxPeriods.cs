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
    public class TaxPeriods : ERPNodeDalRepository
    {
        public TaxPeriods(Organization organization) : base(organization)
        {
            transactionType = ERPObjectType.TaxPeriod;
        }

        public List<TaxPeriod> GetList() => erpNodeDBContext.TaxPeriods
            .OrderBy(t => t.TrnDate)
            .ToList();

        public TaxPeriod Find(Guid id) => erpNodeDBContext.TaxPeriods.Find(id);

        public TaxPeriod Save(TaxPeriod salesTax)
        {
            var existSalesTax = erpNodeDBContext.TaxPeriods.Find(salesTax.Uid);

            if (existSalesTax.PostStatus == LedgerPostStatus.Locked)
                return existSalesTax;

            existSalesTax.TrnDate = new DateTime(salesTax.TrnDate.Year, salesTax.TrnDate.Month, 1).AddMonths(1).AddDays(-1);
            existSalesTax.CloseToAccountGuid = salesTax.CloseToAccountGuid;
            erpNodeDBContext.SaveChanges();

            return existSalesTax;
        }

        public TaxPeriod Create()
        {
            var newTaxPeriod = new TaxPeriod()
            {
                Uid = Guid.NewGuid(),
                TrnDate = DateTime.Now,
            };
            erpNodeDBContext.TaxPeriods.Add(newTaxPeriod);
            erpNodeDBContext.SaveChanges();

            return newTaxPeriod;
        }

        public void ReCalculate()
        {
            this.GetList().ToList().ForEach(a => a.ReCalculate());
            erpNodeDBContext.SaveChanges();
        }

        public List<TaxPeriod> GetReadyForPost() => erpNodeDBContext.TaxPeriods
                .Where(s => s.PostStatus == LedgerPostStatus.Editable && s.CloseToAccountGuid != null)
                .ToList();

        public List<Commercial> GetUnassignCommercial(TaxPeriod taxPeriod) =>
                organization.CommercialTaxes.GetAssignAbleCommercial()
                .Where(com => com.TrnDate.Date <= taxPeriod.TrnDate.Date)
                .ToList();

        public List<Commercial> GetUnassignCommercials() => erpNodeDBContext.Commercials
                .Where(com => com.TaxCodeId != null && com.TaxPeriodId == null)
                .ToList();

        public int AssignCommercialTaxs(TaxPeriod taxPeriod, string commercialsUid)
        {
            List<Guid> commercialsUidList = commercialsUid?.Trim().Split(',')
                .Select(s => Guid.Parse(s))
                .ToList();

            var unassignComercial = erpNodeDBContext.Commercials
                    .Where(c => commercialsUidList.Contains(c.Uid))
                    .ToList();

            int assignCount = 0;

            unassignComercial.ForEach(commercial =>
            {
                assignCount++;
                taxPeriod.Commercials.Add(commercial);
            });

            erpNodeDBContext.SaveChanges();
            taxPeriod.ReCalculate();
            erpNodeDBContext.SaveChanges();

            return assignCount;
        }

        public List<TaxPeriod> GetLedger(TaxPeriod taxPeriod) => erpNodeDBContext.TaxPeriods
            .Where(tr => tr.Uid == taxPeriod.Uid)
            .ToList();

        public void AutoAssignCommercial()
        {
            this.GetList()
                .Where(s => s.PostStatus == LedgerPostStatus.Locked)
                .ToList()
                .ForEach(a => this.AutoAssignCommercial(a));

            erpNodeDBContext.SaveChanges();
        }

        public void AutoAssignCommercial(TaxPeriod taxPeriod)
        {
            if (taxPeriod.PostStatus == LedgerPostStatus.Locked)
                this.UnPost(taxPeriod);

            var commercials = this.GetUnassignCommercial(taxPeriod);

            commercials.ForEach(commercial =>
            {
                commercial.TaxPeriod = taxPeriod;
                commercial.TaxPeriodId = taxPeriod.Uid;
            });

            taxPeriod.ReCalculate();
            erpNodeDBContext.SaveChanges();
        }

        public void Delete(TaxPeriod taxPeriod)
        {
            taxPeriod.Commercials.ToList().ForEach(c =>
            {
                taxPeriod.Commercials.Remove(c);
            });

            erpNodeDBContext.TaxPeriods.Remove(taxPeriod);

            organization.SaveChanges();
        }

        public void ReOrder()
        {
            var transactions = erpNodeDBContext.TaxPeriods
                .OrderBy(t => t.TrnDate)
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

        public bool PostLedger(TaxPeriod taxPeriod, bool SaveImmediately = true)
        {
            if (taxPeriod.PostStatus == LedgerPostStatus.Locked)
                return false;

            taxPeriod.ReCalculate();

            var trLedger = new Models.Accounting.TransactionLedger()
            {
                Uid = taxPeriod.Uid,  
                TrnDate = taxPeriod.TrnDate,
                TransactionName = taxPeriod.Name,
                TransactionNo = taxPeriod.No ?? 0,
                TransactionType = transactionType
            };

            taxPeriod.GetCommercialTaxGroups().ForEach(taxGroup =>
            {

                switch (taxGroup.TaxCode.TaxDirection)
                {
                    case TaxDirection.Input:
                     //   Console.WriteLine($"INP =>{taxGroup.TaxCode.TaxAccount.Name}{taxGroup.TaxBalance}");
                        trLedger.Credit(taxGroup.TaxCode.TaxAccount, taxGroup.TaxBalance);
                        break;
                    case TaxDirection.Output:
                      //  Console.WriteLine($"OUT =>{taxGroup.TaxCode.TaxAccount.Name}{taxGroup.TaxBalance}");
                        trLedger.Debit(taxGroup.TaxCode.TaxAccount, taxGroup.TaxBalance);
                        break;
                }
            });

            if (taxPeriod.CloseToAccount.Type == Models.Accounting.AccountTypes.Asset)
                trLedger.Debit(taxPeriod.CloseToAccount, Math.Abs(taxPeriod.ClosingAmount));

            else if (taxPeriod.CloseToAccount.Type == Models.Accounting.AccountTypes.Liability)
                trLedger.Credit(taxPeriod.CloseToAccount, Math.Abs(taxPeriod.ClosingAmount));

            if (trLedger.FinalValidate())
            {
                erpNodeDBContext.TransactionLedgers.Add(trLedger);
                taxPeriod.PostStatus = LedgerPostStatus.Locked;
            }

            if (SaveImmediately && taxPeriod.PostStatus == LedgerPostStatus.Locked)
                erpNodeDBContext.SaveChanges();

            return true;
        }


        public void UnPost(TaxPeriod salesTax)
        {
            // Console.WriteLine("> Un Posting ,salesTax" + salesTax.No);
            organization.LedgersDal.RemoveTransaction(salesTax.Uid);
            salesTax.PostStatus = LedgerPostStatus.Editable;
            erpNodeDBContext.SaveChanges();
        }

        public void UnPost()
        {
            Console.WriteLine("> Un Post" + Models.Accounting.Enums.ERPObjectType.TaxPeriod.ToString());

            string sqlCommand = "DELETE FROM [dbo].[ERP_Ledgers] WHERE TransactionType = {0} ";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            sqlCommand = "DELETE FROM [dbo].[ERP_Ledger_Transactions] WHERE TransactionType =  {0}";
            sqlCommand = string.Format(sqlCommand, (int)this.transactionType);
            erpNodeDBContext.Database.ExecuteSqlCommand(sqlCommand);

            erpNodeDBContext.TaxPeriods.ToList().ForEach(s => s.PostStatus = LedgerPostStatus.Editable);
            erpNodeDBContext.SaveChanges();
        }


    }
}