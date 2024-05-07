using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPKeeper.Node.DAL;
using ERPKeeper.Node.DAL.Employees;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        private string enterpriseDB;
        public EnterpriseRepo newOrganization;
        public Organization oldOrganization;

        public ERPMigrationTool(string enterpriseDB)
        {
            this.enterpriseDB = enterpriseDB;

            Console.WriteLine($"###{enterpriseDB}########################################################");
            newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
            newOrganization.ErpCOREDBContext.Database.Migrate();
            Console.WriteLine(newOrganization.ErpCOREDBContext.Transactions.Count());
            oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);
            Console.WriteLine("###########################################################");
        }


        public void Migrate()
        {

            Copy_Employees();
            Copy_Taxes();


            //newOrganization.FiscalYears.PrepareFiscalYearBalances();
            //newOrganization.FiscalYears.UpdateTransactionFiscalYears();

            //CopyProfiles();
            //CopySuppliers();

            //CopyAccounts();
            //Copy_Financial_LiabilityPayments();
            //Copy_Financial_Loans();
            //Copy_Financial_Lends();

            //Copy_Customers_Sales();
            //Copy_Customers_SaleItems();
            ////CopyReceivePayments();

            //Copy_Suppliers_Purchases();
            //Copyy_Suppliers_PurchaseItems();
            ////CopySupplierPayments();





            //CopyProfiles();
            //CopyBrands();
            //CopyItemGroups();
            //CopyItems();
            //CopyProjects();
            //CopyCustomers();
            //CopySuppliers();
            //CopyFiscalYear();
            //CopyTaxCode();
            //CopyTaxPeriod();
            //CopySales();
            //CopySaleItems();
            //CopyPurchases();
            //CopyPurchaseItems();
            //CreateTransactionForSales(newOrganization);
            //CreateTransactionForPurchases(newOrganization);
            //CopyFundTransfers();
            //CopyAssetTypes();
            //CopyAssets();
            //CopyAssetDeprecreates();
            //CopyJournalEntryTypes();
            //CopyJournalEntries();
            //CopyJournalEntryItems();
            //CopyEmployeePositions();
            //CopyEmployees();
            //CopyAccounts();
            //CopyDefaultAccounts();
            //CopyRetentionTypes();



        }

        private void Copy_Employees()
        {
            // Employees Section
            Copy_EmployeesEmployeePositions();
            Copy_Employees_PaymentTypes();
            Copy_Employees_Employees();
            Copy_Employees_EmployeePaymentPeriods();
            Copy_Employees_EmployeePayments();
            Copy_Employees_EmployeePaymentItems();
        }

        private void Copy_Taxes()
        {
            //Taxes
            Copy_Taxes_IncomeTaxes();
        }

        private void CopyProfiles()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Profiles.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Profiles.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Profiles.Profile()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        TaxNumber = a.TaxNumber,
                        Note = a.Note,

                    };
                    newOrganization.ErpCOREDBContext.Profiles.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyProjects()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Projects.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Projects.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Projects.Project()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        StartDate = a.CreatedDate,
                        EndDate = a.CreatedDate.AddYears(1),
                    };
                    newOrganization.ErpCOREDBContext.Projects.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }

        private void CopyRetentionTypes()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.RetentionTypes.ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"RetentionTypes:{oldModel.Name}-{oldModel.Uid}");
                var exist = newOrganization.ErpCOREDBContext
                .RetentionTypes
                .FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    Console.WriteLine($"> Add");

                    exist = new ERPKeeperCore.Enterprise.Models.Financial.RetentionType()
                    {
                        Id = oldModel.Uid,
                        IsActive = oldModel.IsActive,
                        Description = oldModel.Description ?? "NA",
                        Name = oldModel.Name,
                        Direction = (Enterprise.Models.Financial.Enums.RetentionDirection)oldModel.RetentionDirection,
                        Rate = oldModel.Rate,
                        RetentionToAccountId = oldModel.RetentionToAccountGuid,
                    };

                    newOrganization.ErpCOREDBContext.RetentionTypes.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }
            });

            newOrganization.ErpCOREDBContext.SaveChanges();
        }
        private void CopyJournalEntryTypes()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntryTypes.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.JournalEntryTypes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
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
        private void CopyJournalEntries()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.JournalEntries.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.JournalEntries.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
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
        private void CopyJournalEntryItems()
        {
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

    }
}
