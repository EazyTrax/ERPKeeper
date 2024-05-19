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
            string[] Enterprises = new string[] {
                "tec",
                //"bit"
            };

            foreach (var enterpriseDB in Enterprises)
            {
                Console.WriteLine($"###{enterpriseDB}########################################################");
                var newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
                newOrganization.ErpCOREDBContext.Database.Migrate();
                var oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);
                Console.WriteLine($">{newOrganization.ErpCOREDBContext.Database.GetConnectionString()}");
                Console.WriteLine("###########################################################" + Environment.NewLine + Environment.NewLine);

                var migrationTool = new ERPMigrationTool(enterpriseDB);
                //    migrationTool.Migrate();


                if (true)
                    GeneralOperations(newOrganization, oldOrganization);

                if (true)
                {
                    newOrganization.Transactions.PostLedgers();
                    newOrganization.ChartOfAccount.RefreshCurrentBalance();
                    newOrganization.ChartOfAccount.RefreshHostoriesBalances();
                    newOrganization.Transactions.ClearEmptyLedgers();
                    newOrganization.FiscalYears.UpdateAllYearsAccountsBalance();
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }

            }

            static void GeneralOperations(EnterpriseRepo newOrganization, Organization oldOrganization)
            {
                //newOrganization.LiabilityPayments.ClearEmpties();
                //newOrganization.JournalEntries.ClearEmpties();
                //newOrganization.FiscalYears.UpdatePreviusFiscalYear();
                //newOrganization.SaveChanges();


                //  newOrganization.Items.UpdateSupplierItems();


                //var jurnalEntry = newOrganization.JournalEntries.Find(Guid.Parse("210fa8ae-625e-4997-f7a9-08dc77f4309d"));
                //var invertJurnalEntry = new Enterprise.Models.Accounting.JournalEntry()
                //{
                //    Date = jurnalEntry.Date.AddDays(1),
                //    JournalEntryTypeId = jurnalEntry.JournalEntryTypeId,
                //    Description = "กลับรายการปีแล้ว"
                //};

                //jurnalEntry.JournalEntryItems.ToList().ForEach(jei =>
                //{
                //    invertJurnalEntry.AddAcount(jei.AccountId, jei.Credit, jei.Debit);
                //});


                //newOrganization.ErpCOREDBContext.JournalEntries.Add(invertJurnalEntry);
                //newOrganization.SaveChanges();






            }
        }
    }
}
