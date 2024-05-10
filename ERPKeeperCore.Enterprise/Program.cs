using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using static System.Collections.Specialized.BitVector32;

namespace ERPKeeperCore.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Enterprises = new string[] { "tec", "bit" };

            foreach (var enterpriseDB in Enterprises)
            {
                Console.WriteLine($"###{enterpriseDB}########################################################");
                var newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
                newOrganization.ErpCOREDBContext.Database.Migrate();
                var oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);
                Console.WriteLine($">{newOrganization.ErpCOREDBContext.Database.GetConnectionString()}");
                Console.WriteLine("###########################################################" + Environment.NewLine + Environment.NewLine);
                newOrganization.SaveChanges();


                //newOrganization.ErpCOREDBContext.TransactionLedgers.Where(t => t.Debit == 0 && t.Credit == 0)
                //    .ExecuteDelete();
                //newOrganization.SaveChanges();

                var migrationTool = new ERPMigrationTool(enterpriseDB);
                // migrationTool.Migrate();

                //  newOrganization.ChartOfAccount.CreateOpeningJournalEntry();
                newOrganization.Transactions.PostLedgers();
                newOrganization.FiscalYears.UpdateAccountBalance();
            }
        }


        private static void GeneralOperations(EnterpriseRepo newOrganization)
        {

            newOrganization.ErpCOREDBContext.FundTransferItems
                .Where(m => m.Debit == 0)
                .ToList()
                .ForEach(model =>
                {
                    Console.WriteLine($"{model.Account.Name}");
                    newOrganization.ErpCOREDBContext.FundTransferItems.Remove(model);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                });
        }

    }
}
