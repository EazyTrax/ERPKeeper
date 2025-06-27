using ERPKeeper.Node.DAL;
using ERPKeeper.Node.Models.Taxes;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Assets;
using ERPKeeperCore.Enterprise.Models.Assets.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using static System.Collections.Specialized.BitVector32;
using OpenAI;
using DevExpress.XtraRichEdit.Model;
using ERPKeeper.Node.Reports.Employees;
using static System.Runtime.InteropServices.JavaScript.JSType;


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


                //newOrganization.TaxPeriods.UnPostToTransactions();
                //newOrganization.TaxPeriods.PostToTransactions();

                if (false && newOrganization != null)
                {
                    newOrganization.Transactions.Clear_EmptyLedgers();
                    newOrganization.Transactions.Post_Ledgers();
                    newOrganization.ChartOfAccount.Refresh_CurrentBalance();
                    newOrganization.ChartOfAccount.Refresh_HostoriesBalances();
                    newOrganization.FiscalYears.Update_AllYearsAccountsBalance();
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }

                GeneralOperations(newOrganization);
            }

            static void GeneralOperations(EnterpriseRepo newOrganization)
            {


                newOrganization.ErpCOREDBContext.Sales.OrderBy(x => x.Date)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.UpdateName();
                        Console.WriteLine($">{x.No} {x.Name}");
                    });
                newOrganization.SaveChanges();

                // UpdateCurrentBalance(newOrganization);
            }

            static void Export_To_PDF(EnterpriseRepo newOrganization, Organization oldOrganization)
            {

                var report = new ERPKeeperCore.Enterprise.Reports.Report1();
                var sales = newOrganization.Sales.GetAll();
                report.DataSource = sales;

                // Export to PDF
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "sales_report.pdf");
                report.ExportToPdf(outputPath);

                Console.WriteLine($"Report generated: {outputPath}");
            }


        }

        private static void UpdateCurrentBalance(EnterpriseRepo newOrganization)
        {
            int index = 1;

            newOrganization.ErpCOREDBContext.TransactionLedgers
                .GroupBy(p => p.AccountId)
                .Select(g => new { AccountId = g.Key, Ledger = g.ToList() })
                .ToList()
                .ForEach(x =>
                {
                    decimal amount = 0;
                    foreach (var ledger in x.Ledger.OrderBy(y => y.Date).ToList())
                    {
                        amount = amount + ledger.Debit - ledger.Credit;
                        ledger.CurrentBalance = amount;
                    }
                    newOrganization.SaveChanges();
                });
        }
    }
}
