using ERPKeeper.Node.DAL;
using ERPKeeper.Node.Models.Taxes;
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
                Console.WriteLine($"> ########################################################");
                Console.WriteLine($"> {enterpriseDB}");
                Console.WriteLine($"> ########################################################");
                var newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
                var oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);

                newOrganization.ErpCOREDBContext.Database.Migrate();

                if (false)
                    (new ERPMigrationTool(enterpriseDB)).Migrate();


                if (true)
                    GeneralOperations(newOrganization, oldOrganization);

                if (false && newOrganization != null)
                {
                    newOrganization.Transactions.Post_Ledgers();
                    newOrganization.ChartOfAccount.Refresh_CurrentBalance();
                    newOrganization.ChartOfAccount.Refresh_HostoriesBalances();
                    newOrganization.Transactions.Clear_EmptyLedgers();
                    newOrganization.FiscalYears.Update_AllYearsAccountsBalance();
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
            }

            static void GeneralOperations(EnterpriseRepo newOrganization, Organization oldOrganization)
            {

                newOrganization.SaleQuotes.GetAll().ForEach(sq =>
                {
                    sq.UpdateName();
                });

                newOrganization.SaveChanges();
            }
        }
    }
}
