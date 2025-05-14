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
                var oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);


                //  newOrganization.TaxPeriods.UnPostToTransactions();
                //   newOrganization.TaxPeriods.PostToTransactions();



                if (false && newOrganization != null)
                {
                    newOrganization.Transactions.Clear_EmptyLedgers();
                    newOrganization.Transactions.Post_Ledgers();
                    newOrganization.ChartOfAccount.Refresh_CurrentBalance();
                    newOrganization.ChartOfAccount.Refresh_HostoriesBalances();
                    newOrganization.FiscalYears.Update_AllYearsAccountsBalance();
                    newOrganization.ErpCOREDBContext.SaveChanges();


                    var fiscalYears = newOrganization
              .FiscalYears.GetAll()
              .OrderBy(x => x.StartDate)
              .ToList();

                    fiscalYears.ForEach(fy =>
                    {
                        fy.PostToTransaction();

                        fy.Create_Empty_Accounts_Balance(newOrganization.ChartOfAccount.All());
                        newOrganization.FiscalYears.Update_AccountsBalance(fy);

                        fy.PostToTransaction();
                    });
                }

                GeneralOperations(newOrganization, oldOrganization);
            }

            static void GeneralOperations(EnterpriseRepo newOrganization, Organization oldOrganization)
            {

                var supplierPayments = newOrganization.ErpCOREDBContext
                    .SupplierPayments
                    .Include(x => x.Purchase)
                    .ThenInclude(x => x.Items)
                    .Include(x => x.Transaction)
                    .ToList();

                supplierPayments
                    .ToList()
                    .ForEach(x =>
                    {
                        x.Purchase.UpdateBalance();

                        if (Math.Abs(x.Purchase.Total - x.Amount) > 1)
                        {
                            Console.WriteLine($"Purchase: {x.Purchase.Name} - {x.Purchase.Total} != {x.Amount}");
                            x.UnPostLedger();
                            x.Amount = x.Purchase.Total;
                        }
                    });

                newOrganization.SaveChanges();


                var purchases = newOrganization.ErpCOREDBContext.Purchases
                    .Where(x => x.SupplierPayment == null)
                    .ToList();

                purchases.ForEach(purchase =>
                {
                    var payDate = purchase.Date;

                    if (purchase.SupplierPayment == null)
                    {
                        purchase.SupplierPayment = new Enterprise.Models.Suppliers.SupplierPayment()
                        {
                            Date = payDate,
                            Amount = purchase.Total,
                            AssetAccount_PayFrom = newOrganization.SystemAccounts.Cash,
                        };

                        purchase.Status = Enterprise.Models.Suppliers.Enums.PurchaseStatus.Paid;
                        newOrganization.SaveChanges();
                    }
                });

                //newOrganization.Purchases.Post_Ledgers();
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
    }
}
