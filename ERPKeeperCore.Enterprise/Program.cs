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


                //newOrganization.ErpCOREDBContext.TransactionLedgers
                //    .Include(t => t.Transaction)
                //    .ToList()
                //    .ForEach(model => model.Date = model.Transaction.Date);
                //newOrganization.SaveChanges();


                //newOrganization.ErpCOREDBContext.Accounts
                //    .ToList()
                //    .ForEach(model => model.CreateBalance());

                newOrganization.SaveChanges();




                //GeneralOperations(newOrganization);
                //newOrganization.ErpCOREDBContext.TransactionLedgers.Where(t => t.Debit == 0 && t.Credit == 0)
                //.ExecuteDelete();
                //newOrganization.SaveChanges();

                var migrationTool = new ERPMigrationTool(enterpriseDB);
                // migrationTool.Migrate();
                // newOrganization.ChartOfAccount.CreateOpeningJournalEntry();
                 newOrganization.Transactions.PostLedgers();
                newOrganization.FiscalYears.UpdateAccountBalance();
                // }
            }


            static void GeneralOperations(EnterpriseRepo newOrganization)
            {
                newOrganization.ErpCOREDBContext.PurchaseItems
                    .Where(m => m.Price < 0)
                    .ToList()
                    .ForEach(model =>
                    {
                        if (model.PurchaseId != null)
                        {
                            Console.WriteLine($"{model.Purchase.Name} {model.Purchase.Items.Where(x => x.LineTotal < 0).Sum(x => x.LineTotal)} {model.PartNumber} {model.Price}");
                            model.Purchase.Discount = model.Purchase.Items.Where(x => x.LineTotal < 0).Sum(x => x.LineTotal);
                            model.Purchase.Discount = Math.Abs(model.Purchase.Discount);

                            model.Purchase.IncomeAccount_DiscountTaken = newOrganization.SystemAccounts.GetAccount(DefaultAccountType.Income_DiscountTaken);
                            model.Purchase.IsPosted = false;
                            newOrganization.SaveChanges();

                            model.Purchase.Transaction.ClearLedger();
                            newOrganization.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine($"NF:  {model.PartNumber} {model.Price}");
                            newOrganization.ErpCOREDBContext.PurchaseItems.Remove(model);
                            newOrganization.SaveChanges();
                        }
                    });

                newOrganization.ErpCOREDBContext.SaleItems
                  .Where(m => m.Price < 0)
                  .ToList()
                  .ForEach(model =>
                  {
                      if (model.SaleId != null)
                      {
                          Console.WriteLine($"{model.Sale.Name} {model.Sale.Items.Where(x => x.LineTotal < 0).Sum(x => x.LineTotal)} {model.PartNumber} {model.Price}");
                          model.Sale.Discount = model.Sale.Items.Where(x => x.LineTotal < 0).Sum(x => x.LineTotal);
                          model.Sale.Discount = Math.Abs(model.Sale.Discount);

                          model.Sale.Discount_Given_Expense_Account = newOrganization.SystemAccounts.GetAccount(DefaultAccountType.DiscountGiven);
                          model.Sale.IsPosted = false;
                          newOrganization.SaveChanges();

                          model.Sale.Transaction.ClearLedger();
                          newOrganization.SaveChanges();
                      }
                      else
                      {
                          Console.WriteLine($"NF:  {model.PartNumber}  {model.SaleId} {model.Price}");
                          newOrganization.ErpCOREDBContext.SaleItems.Remove(model);
                          newOrganization.SaveChanges();
                      }


                  });
            }

        }
    }
}
