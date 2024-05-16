using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Assets;
using ERPKeeperCore.Enterprise.Models.Assets.Enums;
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

                var migrationTool = new ERPMigrationTool(enterpriseDB);
                //  migrationTool.Migrate();



                if (true)
                    GeneralOperations(newOrganization);


                if (false)
                {
                    newOrganization.Transactions.PostLedgers();
                    newOrganization.ChartOfAccount.RefreshCurrentBalance();
                    newOrganization.ChartOfAccount.RefreshHostoriesBalances();
                    newOrganization.Transactions.ClearEmptyLedgers();
                    newOrganization.FiscalYears.UpdateAllYearsAccountsBalance();

                    newOrganization.ErpCOREDBContext.SaveChanges();
                }



            }

            static void GeneralOperations(EnterpriseRepo newOrganization)
            {
                newOrganization.LiabilityPayments.ClearEmpties();
                newOrganization.JournalEntries.ClearEmpties();
                newOrganization.FiscalYears.UpdatePreviusFiscalYear();

                newOrganization.SaveChanges();
            }
        }
    }
}
