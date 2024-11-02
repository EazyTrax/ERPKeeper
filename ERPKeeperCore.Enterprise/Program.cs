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
                newOrganization.ErpCOREDBContext.Database.Migrate();

                var oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);


                newOrganization.SaleQuotes.RemoveExpired(360);
                newOrganization.Customers.RemoveNoSaleOrQuote();


                newOrganization.PurchaseQuotes.RemoveExpired(360);

                newOrganization.Suppliers.GetAll().ForEach(s =>
                {
                    if (s.Purchases.Count() == 0 && s.PurchaseQuotes.Count() == 0)
                    {
                        Console.WriteLine($"Supplier {s.Profile.Name} has no purchases or purchase quotes");
                        s.SupplierItems.Clear();
                        s.Profile.Supplier = null;

                        newOrganization.ErpCOREDBContext.Suppliers.Remove(s);

                        newOrganization.SaveChanges();
                    }
                });




                newOrganization.Customers.GetAll().ForEach(s =>
                {
                    if (s.Sales.Count() == 0 && s.SaleQuotes.Count() == 0)
                    {
                        Console.WriteLine($"Customer {s.Profile.Name} has no sales or sales quotes");
                        s.CustomerItems.Clear();
                        s.Profile.Customer = null;

                        newOrganization.ErpCOREDBContext.Customers.Remove(s);

                        newOrganization.SaveChanges();
                    }

                });
                newOrganization.SaveChanges();

                //var eRPMigrationTool = new ERPMigrationTool(enterpriseDB);
                //eRPMigrationTool.Migrate();
                //eRPMigrationTool.Copy_Profiles();
                //eRPMigrationTool.Copy_Items();
                //eRPMigrationTool.Copy_Suppliers();
                //eRPMigrationTool.Copy_Customers();
                //eRPMigrationTool.Copy_Employees();





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
