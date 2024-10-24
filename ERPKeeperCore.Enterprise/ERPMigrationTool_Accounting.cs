using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        private void Copy_Accounting()
        {
            Console.WriteLine("> Copy_Accounting");

            Copy_Accounting_Accounts();
            Copy_Accounting_DefaultAccounts();
            Copy_Accounting_FiscalYear();
            Copy_Accounting_JournalEntryTypes();
            Copy_Accounting_JournalEntries();
            Copy_Accounting_JournalEntryItems();
        }

        private void Copy_Accounting_JournalEntryTypes()
        {
            Console.WriteLine("> Copy_Accounting_JournalEntryTypes");
            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntryTypes.ToList();

            oldModels.ForEach(a =>
            {
             
                var exist = newOrganization.ErpCOREDBContext.JournalEntryTypes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntryType()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        IsActive = a.IsActive,
                    };

                    newOrganization.ErpCOREDBContext.JournalEntryTypes.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void Copy_Accounting_JournalEntries()
        {
            Console.WriteLine("Copy_Accounting_JournalEntries");

            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntries.ToList();

            oldModels.ForEach(a =>
            {
             
                var exist = newOrganization.ErpCOREDBContext.JournalEntries.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntry()
                    {
                        Id = a.Uid,
                        Date = a.TrnDate,
                        No = a.No,
                        Credit = a.Credit,
                        Debit = a.Debit,
                        Memo = a.Memo,
                        JournalEntryTypeId = a.JournalEntryTypeGuid,
                    };

                    newOrganization.ErpCOREDBContext.JournalEntries.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void Copy_Accounting_JournalEntryItems()
        {
            Console.WriteLine("Copy_Accounting_JournalEntryItems");
            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntryItems.ToList();

            oldModels.ForEach(a =>
            {
                var exist = newOrganization.ErpCOREDBContext.JournalEntryItems.FirstOrDefault(x => x.Id == a.JournalEntryItemId);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntryItem()
                    {
                        Id = a.JournalEntryItemId,
                        Credit = a.Credit,
                        Debit = a.Debit,
                        Memo = a.Memo,
                        JournalEntryId = a.JournalEntryId,
                        AccountId = a.AccountUid,
                    };

                    newOrganization.ErpCOREDBContext.JournalEntryItems.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void Copy_Accounting_Accounts()
        {
            Console.WriteLine("> Copy_Accounting_Accounts");
            var oldAccounts = oldOrganization.ErpNodeDBContext.AccountItems.ToList();



            oldAccounts.ForEach(oldAccount =>
            {
                var exist = newOrganization.ErpCOREDBContext.Accounts.FirstOrDefault(x => x.Id == oldAccount.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"ACC: {oldAccount.No}-{oldAccount.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.Account()
                    {
                        Id = oldAccount.Uid,
                        Name = oldAccount.Name,
                        Description = oldAccount.Description,
                        No = oldAccount.No,
                        Type = (AccountTypes)oldAccount.Type,
                        SubType = (AccountSubTypes?)oldAccount.SubEnumType,
                        IsCashEquivalent = oldAccount.IsCashEquivalent,
                        IsLiquidity = oldAccount.IsLiquidity,
                    };
                    newOrganization.ErpCOREDBContext.Accounts.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    //Console.WriteLine($"Dr.{oldAccount.OpeningDebitBalance} , Cr.{oldAccount.OpeningCreditBalance}");
                    //exist.OpeningDebit = oldAccount.OpeningDebitBalance;
                    //exist.OpeningCredit = oldAccount.OpeningCreditBalance;

                    //newOrganization.ErpCOREDBContext.SaveChanges();
                    //Console.WriteLine($"Dr.{exist.OpeningDebit} , Cr.{exist.OpeningCredit}");
                    //Console.WriteLine($"-------------------------------");
                }
            });
        }
        private void Copy_Accounting_DefaultAccounts()
        {
            Console.WriteLine("> Copy_Accounting_DefaultAccounts");
            var oldModels = oldOrganization.ErpNodeDBContext.SystemAccounts.ToList();

            oldModels.ForEach(oldSystemAccount =>
            {
                var exist = newOrganization.ErpCOREDBContext.DefaultAccounts
                .FirstOrDefault(x => x.Type == (DefaultAccountType)oldSystemAccount.AccountType);

                if (exist == null)
                {
                    Console.WriteLine($"PROF:{oldSystemAccount.AccountType}-{oldSystemAccount.AccountItem.Name}");

                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.DefaultAccount()
                    {
                        Type = (DefaultAccountType)oldSystemAccount.AccountType,
                        AccountId = oldSystemAccount.AccountItemUid,
                        LastUpdate = oldSystemAccount.LastUpdate,
                    };

                    newOrganization.ErpCOREDBContext.DefaultAccounts.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }

            });
        }
        private void Copy_Accounting_FiscalYear()
        {
            Console.WriteLine("> Copy_Accounting_FiscalYear");
            var oldModels = oldOrganization.ErpNodeDBContext.FiscalYears.ToList();

            oldModels.ForEach(a =>
            {
                var exist = newOrganization.ErpCOREDBContext.FiscalYears.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"PROF:{a.Name}-{a.StartDate}");

                    exist = new ERPKeeperCore.Enterprise.Models.Accounting.FiscalYear()
                    {
                        Id = a.Uid,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Memo = a.Memo,
                        Status = (FiscalYearStatus)a.Status,
                    };

                    newOrganization.ErpCOREDBContext.FiscalYears.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
    }
}
