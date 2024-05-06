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
        private void CopyAccounts()
        {
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
        private void CopyDefaultAccounts()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.SystemAccounts.ToList();

            oldModels.ForEach(oldSystemAccount =>
            {
                Console.WriteLine($"PROF:{oldSystemAccount.AccountType}-{oldSystemAccount.AccountItem.Name}");

                var exist = newOrganization.ErpCOREDBContext.DefaultAccounts
                .FirstOrDefault(x => x.Type == (DefaultAccountType)oldSystemAccount.AccountType);

                if (exist == null)
                {
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
        private void CopyFiscalYear()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.FiscalYears.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.StartDate}");

                var exist = newOrganization.ErpCOREDBContext.FiscalYears.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
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
