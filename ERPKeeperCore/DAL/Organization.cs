using KeeperCore.ERPNode.DBContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using KeeperCore.ERPNode.Models.Company;
using KeeperCore.ERPNode.Models.Datum;
using KeeperCore.ERPNode.Models.Taxes.Enums;
using KeeperCore.ERPNode.Models.Transactions;
using KeeperCore.ERPNode.Models.Profiles;

namespace KeeperCore.ERPNode.DAL
{
    public class Organization : IDisposable
    {
        public ERPCoreDbContext ErpNodeDBContext { get; private set; }

        private DAL.Accounting.ChartOfAccounts _chartOfAccountDal;
        private DAL.Accounting.SystemAccounts _systemAccountsDal;
        private DAL.Accounting.ChartOfAccountTemplate _ChartOfAccountTemplate;
        private DAL.Accounting.FiscalYears _fiscalYearsDal;
        private DAL.Accounting.Ledgers _Ledgers;

        private DAL.Company.DataItems _dataItemsDal;
        public DAL.Company.Locations Locations;
        private DAL.Transactions.Terms.ShippingTerms _ShippingTerms;

        private DAL.Transactions.Terms.ShippingMethods _ShippingMethods;
        private DAL.Transactions.Terms.PaymentTerms _PaymentTerms;
        private DAL.Profiles.Profiles _Profiles;
        private DAL.Profiles.ProfileAddresses _ProfileAddresses;
        private DAL.Profiles.ProfileBankAccounts _ProfileBankAccounts;
        private DAL.Profiles.Customers _customers;
        private DAL.Profiles.Suppliers _suppliers;
        private DAL.Profiles.Employees _employees;
        private DAL.Profiles.Investors _investors;
        private DAL.Items.Items _Items;
        private DAL.Items.AssistItems _AssistItems;
        private DAL.Items.GroupItems _GroupItems;
        private DAL.Items.Brands _Brands;
        private DAL.Items.InventoryItems _inventoryItemsDal;

        private DAL.Items.ItemParameters _ItemParameters;
        private DAL.Items.ItemParameterTypes _ItemParameterTypes;
        private DAL.Items.ItemImages _ItemImages;


        public DAL.Accounting.TransactionLedgers _TransactionLedgers;
        private DAL.Estimates.SaleEstimates _SaleEstimates;
        private DAL.Estimates.PurchaseEstimates _PurchaseEstimates;
        private DAL.Estimates.EstimateItems _EstimateItems;
        private DAL.Projects.WorkTasks _Tasks;
        private DAL.Projects.Projects _Projects;
        private DAL.Documents.Documents _Documents;



        private DAL.Transactions.CommercialItems _commercialItems;
        private DAL.Transactions.CommercialShipments _commercialShipments;
        private DAL.Transactions.Commercials _Commercials;
        private DAL.Transactions.Sales _sales;
        private DAL.Transactions.Purchases _purchases;


        private DAL.Financial.GeneralPayments _GeneralPayments;
        private DAL.Financial.ReceivePayments _ReceivePayments;
        private DAL.Financial.SupplierPayments _SupplierPayments;
        private DAL.Financial.LiabilityPayments _LiabilityPayments;
        private DAL.Financial.RetentionTypes _RetentionTypes;

        private DAL.Financial.FundTransfers _FundTransfers;
        private DAL.Financial.Loans _loans;
        private DAL.Financial.Lends _lends;
        private DAL.Investors.CapitalActivities _CapitalActivities;
        private DAL.Taxes.TaxCodes _TaxCodesDal;
        private DAL.Taxes.TaxPeriods _TaxPeriods;
        private DAL.Taxes.IncomeTaxes _IncomeTaxes;
        private DAL.Taxes.CommercialTaxes _CommercialTaxes;


        private DAL.Employees.EmployeePayments _EmployeePayments;
        private DAL.Employees.EmployeePaymentPeriods _EmployeePaymentPeriods;
        private DAL.Employees.EmployeePaymentLines _EmployeePaymentLines;
        private DAL.Employees.EmployeePaymentTypes _EmployeePaymentTypes;
        private DAL.Employees.EmployeePaymentTemplates _EmployeePaymentTemplates;
        private DAL.Employees.EmployeePositions _EmployeePositions;

        private DAL.Accounting.JournalEntries _journalEntries;
        private DAL.Accounting.JournalEntryTypes _JournalEntryTypes;
        private DAL.Accounting.JournalEntryItems _JournalEntryItems;

        private DAL.Items.ItemsCOSG _PeriodItemsCOSG;
        private DAL.Assets.FixedAssetTypes _FixedAssetTypes;
        private DAL.Assets.FixedAssets _FixedAssets;

        private DAL.Assets.DeprecateSchedules _DeprecatedSchedules;
        private DAL.Accounting.OpeningEntries _OpeningEntries;





        private Logging.EventLogs eventLogs;
        public Logging.EventLogs EventLogs
        {
            get
            {
                if (this.eventLogs == null)
                    this.eventLogs = new Logging.EventLogs(this);
                return eventLogs;
            }
        }

        private Accounting.AccountGroups accountGroups;
        public Accounting.AccountGroups AccountGroups
        {
            get
            {
                if (this.accountGroups == null)
                    this.accountGroups = new Accounting.AccountGroups(this);
                return accountGroups;
            }
        }



        public DAL.Accounting.ChartOfAccounts ChartOfAccount
        {
            get
            {
                if (this._chartOfAccountDal == null)
                    this._chartOfAccountDal = new Accounting.ChartOfAccounts(this);
                return _chartOfAccountDal;
            }
        }
        public DAL.Accounting.SystemAccounts SystemAccounts
        {
            get
            {
                if (this._systemAccountsDal == null)
                    this._systemAccountsDal = new Accounting.SystemAccounts(this);
                return _systemAccountsDal;
            }
        }
        public DAL.Accounting.ChartOfAccountTemplate ChartOfAccountTemplate
        {
            get
            {
                if (this._ChartOfAccountTemplate == null)
                    this._ChartOfAccountTemplate = new Accounting.ChartOfAccountTemplate(this);
                return _ChartOfAccountTemplate;
            }
        }
    





        public DAL.Company.DataItems DataItems
        {
            get
            {
                if (this._dataItemsDal == null)
                    this._dataItemsDal = new Company.DataItems(this);
                return _dataItemsDal;
            }
        }




        public DAL.Transactions.Terms.ShippingTerms ShippingTerms
        {
            get
            {
                if (this._ShippingTerms == null)
                    this._ShippingTerms = new Transactions.Terms.ShippingTerms(this);
                return _ShippingTerms;
            }
        }
        public DAL.Transactions.Terms.ShippingMethods ShippingMethods
        {
            get
            {
                if (this._ShippingMethods == null)
                    this._ShippingMethods = new Transactions.Terms.ShippingMethods(this);
                return _ShippingMethods;
            }
        }
        public DAL.Transactions.Terms.PaymentTerms PaymentTerms
        {
            get
            {
                if (this._PaymentTerms == null)
                    this._PaymentTerms = new Transactions.Terms.PaymentTerms(this);
                return _PaymentTerms;
            }
        }



        public DAL.Profiles.Profiles Profiles
        {
            get
            {
                if (this._Profiles == null)
                    this._Profiles = new DAL.Profiles.Profiles(this);
                return _Profiles;
            }
        }

        public DAL.Profiles.ProfileAddresses ProfileAddresses
        {
            get
            {
                if (this._ProfileAddresses == null)
                    this._ProfileAddresses = new DAL.Profiles.ProfileAddresses(this);
                return _ProfileAddresses;
            }
        }

        public DAL.Profiles.ProfileBankAccounts ProfileBankAccounts
        {
            get
            {
                if (this._ProfileBankAccounts == null)
                    this._ProfileBankAccounts = new DAL.Profiles.ProfileBankAccounts(this);
                return _ProfileBankAccounts;
            }
        }


        public DAL.Profiles.Customers Customers
        {
            get
            {
                if (this._customers == null)
                    this._customers = new DAL.Profiles.Customers(this);
                return _customers;
            }
        }
        public DAL.Profiles.Suppliers Suppliers
        {
            get
            {
                if (this._suppliers == null)
                    this._suppliers = new DAL.Profiles.Suppliers(this);
                return _suppliers;
            }
        }
        public DAL.Profiles.Employees Employees
        {
            get
            {
                if (this._employees == null)
                    this._employees = new DAL.Profiles.Employees(this);
                return _employees;
            }
        }
        public DAL.Profiles.Investors Investors
        {
            get
            {
                if (this._investors == null)
                    this._investors = new DAL.Profiles.Investors(this);
                return _investors;
            }
        }


        public DAL.Items.Items Items
        {
            get
            {
                if (this._Items == null)
                    this._Items = new Items.Items(this);
                return _Items;
            }
        }

        public DAL.Items.AssistItems AssistItems
        {
            get
            {
                if (this._AssistItems == null)
                    this._AssistItems = new Items.AssistItems(this);
                return _AssistItems;
            }
        }
        public DAL.Items.Brands ItemBrands
        {
            get
            {
                if (this._Brands == null)
                    this._Brands = new Items.Brands(this);
                return _Brands;
            }
        }
   
        public DAL.Items.GroupItems GroupItems
        {
            get
            {
                if (this._GroupItems == null)
                    this._GroupItems = new Items.GroupItems(this);
                return _GroupItems;
            }
        }
        public DAL.Items.ItemParameters ItemParameters
        {
            get
            {
                if (this._ItemParameters == null)
                    this._ItemParameters = new Items.ItemParameters(this);
                return _ItemParameters;
            }
        }
        public DAL.Items.ItemParameterTypes ItemParameterTypes
        {
            get
            {
                if (this._ItemParameterTypes == null)
                    this._ItemParameterTypes = new Items.ItemParameterTypes(this);
                return _ItemParameterTypes;
            }
        }
        public DAL.Items.ItemImages ItemImages
        {
            get
            {
                if (this._ItemImages == null)
                    this._ItemImages = new Items.ItemImages(this);
                return _ItemImages;
            }
        }



        public DAL.Items.InventoryItems InventoryItemsDal
        {
            get
            {
                if (this._inventoryItemsDal == null)
                    this._inventoryItemsDal = new Items.InventoryItems(this);
                return _inventoryItemsDal;
            }
        }






        public DAL.Taxes.TaxCodes TaxCodes
        {
            get
            {
                if (this._TaxCodesDal == null)
                    this._TaxCodesDal = new Taxes.TaxCodes(this);
                return _TaxCodesDal;
            }
        }
        public DAL.Taxes.IncomeTaxes IncomeTaxes
        {
            get
            {
                if (this._IncomeTaxes == null)
                    this._IncomeTaxes = new Taxes.IncomeTaxes(this);
                return _IncomeTaxes;
            }
        }

        public DAL.Taxes.CommercialTaxes CommercialTaxes
        {
            get
            {
                if (this._CommercialTaxes == null)
                    this._CommercialTaxes = new Taxes.CommercialTaxes(this);
                return _CommercialTaxes;
            }
        }

        public DAL.Accounting.FiscalYears FiscalYears
        {
            get
            {
                if (this._fiscalYearsDal == null)
                    this._fiscalYearsDal = new Accounting.FiscalYears(this);
                return _fiscalYearsDal;
            }
        }

        public DAL.Accounting.TransactionLedgers TransactionLedgers
        {
            get
            {
                if (this._TransactionLedgers == null)
                    this._TransactionLedgers = new Accounting.TransactionLedgers(this);
                return _TransactionLedgers;
            }
        }


        public DAL.Accounting.Ledgers LedgersDal
        {
            get
            {
                if (this._Ledgers == null)
                    this._Ledgers = new Accounting.Ledgers(this);
                return _Ledgers;
            }
        }
        public DAL.Transactions.CommercialItems CommercialItems
        {
            get
            {
                if (this._commercialItems == null)
                    this._commercialItems = new Transactions.CommercialItems(this);
                return _commercialItems;
            }
        }

        public DAL.Transactions.CommercialShipments CommercialShipments
        {
            get
            {
                if (this._commercialShipments == null)
                    this._commercialShipments = new Transactions.CommercialShipments(this);
                return _commercialShipments;
            }
        }



        public DAL.Transactions.Commercials Commercials
        {
            get
            {
                if (this._Commercials == null)
                    this._Commercials = new Transactions.Commercials(this);
                return _Commercials;
            }
        }
        public DAL.Transactions.Sales Sales
        {
            get
            {
                if (this._sales == null)
                    this._sales = new Transactions.Sales(this);
                return _sales;
            }
        }
        public DAL.Transactions.Purchases Purchases
        {
            get
            {
                if (this._purchases == null)
                    this._purchases = new Transactions.Purchases(this);
                return _purchases;
            }
        }

        public DAL.Employees.EmployeePayments EmployeePayments
        {
            get
            {
                if (this._EmployeePayments == null)
                    this._EmployeePayments = new Employees.EmployeePayments(this);
                return _EmployeePayments;
            }
        }

        public DAL.Employees.EmployeePaymentPeriods EmployeePaymentPeriods
        {
            get
            {
                if (this._EmployeePaymentPeriods == null)
                    this._EmployeePaymentPeriods = new Employees.EmployeePaymentPeriods(this);
                return _EmployeePaymentPeriods;
            }
        }
        public DAL.Employees.EmployeePaymentTypes EmployeePaymentTypes
        {
            get
            {
                if (this._EmployeePaymentTypes == null)
                    this._EmployeePaymentTypes = new Employees.EmployeePaymentTypes(this);
                return _EmployeePaymentTypes;
            }
        }
        public DAL.Employees.EmployeePaymentTemplates EmployeePaymentTemplates
        {
            get
            {
                if (this._EmployeePaymentTemplates == null)
                    this._EmployeePaymentTemplates = new Employees.EmployeePaymentTemplates(this);
                return _EmployeePaymentTemplates;
            }
        }
        public DAL.Employees.EmployeePositions EmployeePositions
        {
            get
            {
                if (this._EmployeePositions == null)
                    this._EmployeePositions = new Employees.EmployeePositions(this);
                return _EmployeePositions;
            }
        }
        public DAL.Employees.EmployeePaymentLines EmployeePaymentLines
        {
            get
            {
                if (this._EmployeePaymentLines == null)
                    this._EmployeePaymentLines = new Employees.EmployeePaymentLines(this);
                return _EmployeePaymentLines;
            }
        }

        public DAL.Financial.GeneralPayments GeneralPayments
        {
            get
            {
                if (this._GeneralPayments == null)
                    this._GeneralPayments = new Financial.GeneralPayments(this);
                return _GeneralPayments;
            }
        }


        public DAL.Financial.ReceivePayments ReceivePayments
        {
            get
            {
                if (this._ReceivePayments == null)
                    this._ReceivePayments = new Financial.ReceivePayments(this);
                return _ReceivePayments;
            }
        }








        public DAL.Financial.SupplierPayments SupplierPayments
        {
            get
            {
                if (this._SupplierPayments == null)
                    this._SupplierPayments = new Financial.SupplierPayments(this);
                return _SupplierPayments;
            }
        }
        public DAL.Financial.LiabilityPayments LiabilityPayments
        {
            get
            {
                if (this._LiabilityPayments == null)
                    this._LiabilityPayments = new Financial.LiabilityPayments(this);
                return _LiabilityPayments;
            }
        }
        public DAL.Financial.FundTransfers FundTransfers
        {
            get
            {
                if (this._FundTransfers == null)
                    this._FundTransfers = new Financial.FundTransfers(this);
                return _FundTransfers;
            }
        }


        public DAL.Financial.RetentionTypes RetentionTypes
        {
            get
            {
                if (this._RetentionTypes == null)
                    this._RetentionTypes = new Financial.RetentionTypes(this);
                return _RetentionTypes;
            }
        }


        public DAL.Investors.CapitalActivities CapitalActivities
        {
            get
            {
                if (this._CapitalActivities == null)
                    this._CapitalActivities = new Investors.CapitalActivities(this);
                return _CapitalActivities;
            }
        }
        public DAL.Taxes.TaxPeriods TaxPeriods
        {
            get
            {
                if (this._TaxPeriods == null)
                    this._TaxPeriods = new Taxes.TaxPeriods(this);
                return _TaxPeriods;
            }
        }
        public DAL.Financial.Loans Loans
        {
            get
            {
                if (this._loans == null)
                    this._loans = new Financial.Loans(this);
                return _loans;
            }
        }
        public DAL.Financial.Lends Lends
        {
            get
            {
                if (this._lends == null)
                    this._lends = new Financial.Lends(this);
                return _lends;
            }
        }

        public DAL.Accounting.JournalEntries JournalEntries
        {
            get
            {
                if (this._journalEntries == null)
                    this._journalEntries = new Accounting.JournalEntries(this);
                return _journalEntries;
            }
        }
        public DAL.Accounting.JournalEntryTypes JournalEntryTemplates
        {
            get
            {
                if (this._JournalEntryTypes == null)
                    this._JournalEntryTypes = new Accounting.JournalEntryTypes(this);
                return _JournalEntryTypes;
            }
        }
        public DAL.Accounting.JournalEntryItems JournalEntryItems
        {
            get
            {
                if (this._JournalEntryItems == null)
                    this._JournalEntryItems = new Accounting.JournalEntryItems(this);
                return _JournalEntryItems;
            }
        }
        public DAL.Items.ItemsCOSG ItemsCOGS
        {
            get
            {
                if (this._PeriodItemsCOSG == null)
                    this._PeriodItemsCOSG = new Items.ItemsCOSG(this);
                return _PeriodItemsCOSG;
            }
        }
        public DAL.Assets.FixedAssets FixedAssets
        {
            get
            {
                if (this._FixedAssets == null)
                    this._FixedAssets = new Assets.FixedAssets(this);
                return _FixedAssets;
            }
        }
        public DAL.Assets.FixedAssetTypes FixedAssetTypes
        {
            get
            {
                if (this._FixedAssetTypes == null)
                    this._FixedAssetTypes = new Assets.FixedAssetTypes(this);
                return _FixedAssetTypes;
            }
        }





        public DAL.Assets.DeprecateSchedules FixedAssetDreprecateSchedules
        {
            get
            {
                if (this._DeprecatedSchedules == null)
                    this._DeprecatedSchedules = new Assets.DeprecateSchedules(this);
                return _DeprecatedSchedules;
            }
        }
        public DAL.Accounting.OpeningEntries OpeningEntries
        {
            get
            {
                if (this._OpeningEntries == null)
                    this._OpeningEntries = new Accounting.OpeningEntries(this);
                return _OpeningEntries;
            }
        }

        public Estimates.SaleEstimates SaleEstimates
        {
            get
            {
                if (this._SaleEstimates == null)
                    this._SaleEstimates = new Estimates.SaleEstimates(this);
                return _SaleEstimates;
            }
        }
        public Estimates.PurchaseEstimates PurchaseEstimates
        {
            get
            {
                if (this._PurchaseEstimates == null)
                    this._PurchaseEstimates = new Estimates.PurchaseEstimates(this);
                return _PurchaseEstimates;
            }
        }
        public Estimates.EstimateItems QuoteItems
        {
            get
            {
                if (this._EstimateItems == null)
                    this._EstimateItems = new Estimates.EstimateItems(this);
                return _EstimateItems;
            }
        }
        public Projects.WorkTasks Tasks
        {
            get
            {
                if (this._Tasks == null)
                    this._Tasks = new Projects.WorkTasks(this);
                return _Tasks;
            }
        }
        public Projects.Projects Projects
        {
            get
            {
                if (this._Projects == null)
                    this._Projects = new Projects.Projects(this);
                return _Projects;
            }
        }
        public Documents.Documents Documents
        {
            get
            {
                if (this._Documents == null)
                    this._Documents = new Documents.Documents(this);
                return _Documents;
            }
        }



        public string DatabaseName { get; private set; }
        public void Dispose()
        {
            this.ErpNodeDBContext.Dispose();
        }
        public void SaveChanges()
        {
            ErpNodeDBContext.SaveChanges();
        }
        public Organization(string dbName="erp.advancemaker.com", bool migratedDB = false)
        {
            this.DatabaseName = dbName;
            this.ErpNodeDBContext = new ERPCoreDbContext(dbName, migratedDB);

            Locations = new Company.Locations(this);
        }

        public Models.Profiles.Profile SelfProfile => ErpNodeDBContext.Profiles
            .Where(p => p.IsSelfOrganization == true)
            .FirstOrDefault();



      
        public Company.ExternalData GetExternalData()
        {
            var data = new Company.ExternalData()
            {
                Name = DataItems.OrganizationName,
                TaxId = DataItems.TaxID,
                MemberCount = ErpNodeDBContext.Profiles.Where(p => p.AccessLevel != AccessLevel.None).Count(),
                OpenTaskCount = ErpNodeDBContext.WorkTasks.Count()
            };
            return data;
        }

        public void CreateCompanyDB(Models.Company.NewCompanyModel newCompany)
        {
            this.SetFirstDate(newCompany.FirstDate);
            this.CreateSelfProfile(newCompany);
            this.ChartOfAccountTemplate.CreateAccounts();
            this.CreateSystemAccounts();
            this.CreateFiscalYear(newCompany);
        }
        private void CreateFiscalYear(NewCompanyModel newCompany)
        {
            var fiscalYearDal = new DAL.Accounting.FiscalYears(this);
            fiscalYearDal.Create(newCompany.FirstDate);
            fiscalYearDal.Create(DateTime.Now);
        }
        private void CreateSystemAccounts()
        {
            Console.WriteLine("> Create System Accounts");

            if (this.DataItems.Get(Models.Datum.DataItemKey.DefaultSystemAccountAssign) == null)
            {
                this.SystemAccounts.AutoAssignSystemAccount();
                this.DataItems.Set(Models.Datum.DataItemKey.DefaultSystemAccountAssign, true.ToString());
            }
        }

        public DateTime FirstDate => this.DataItems.FirstDate;
        private void SetFirstDate(DateTime firstDate)
        {
            Console.WriteLine("> Set FirstDate");
            DateTime verifyFirstDate = new DateTime(DateTime.Now.Year, firstDate.Month, firstDate.Day);


            if (this.DataItems.Get(Models.Datum.DataItemKey.FirstDate) == null)
                this.DataItems.FirstDate = verifyFirstDate;

            this.ErpNodeDBContext.SaveChanges();
        }

        private void CreateSelfProfile(Models.Company.NewCompanyModel newCompany)
        {
            Console.WriteLine("> Create Self Organization Profile");

            if (this.DataItems.Get(Models.Datum.DataItemKey.OrganizationName) == null)
            {
                this.DataItems.Set(Models.Datum.DataItemKey.OrganizationName, newCompany.Name);
                this.DataItems.Set(Models.Datum.DataItemKey.TaxId, newCompany.TaxID);

                var erpProfile = new Models.Profiles.Profile()
                {
                    Id = Guid.NewGuid(),
                    ProfileType = Models.Profiles.ProfileType.Organization,
                    Name = newCompany.Name,
                    CreatedDate = DateTime.Now,
                    localizedLanguage = Models.Profiles.EnumLanguage.en,
                    TaxNumber = newCompany.TaxID,
                    IsSelfOrganization = true
                };

                this.ErpNodeDBContext.Profiles.Add(erpProfile);
                this.ErpNodeDBContext.SaveChanges();
            }
        }

        public void UpdateDocumentsName()
        {
            this.Commercials.UpdateDoumentsName();
            this.PurchaseEstimates.UpdateDoumentsName();
            this.SaleEstimates.UpdateDoumentsName();
            this.GeneralPayments.UpdateDoumentsName();
            this.GeneralPayments.UpdateDoumentsName();
        }
    }
}
