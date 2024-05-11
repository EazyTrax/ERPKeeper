using ERPKeeperCore.Enterprise.DAL;
using ERPKeeperCore.Enterprise.DAL.Assets;
using ERPKeeperCore.Enterprise.DAL.Customers;
using ERPKeeperCore.Enterprise.DAL.Employees;
using ERPKeeperCore.Enterprise.DAL.Financial;
using ERPKeeperCore.Enterprise.DAL.ObsoleteAssets;
using ERPKeeperCore.Enterprise.DAL.Products;
using ERPKeeperCore.Enterprise.DAL.Taxes;
using ERPKeeperCore.Enterprise.DBContext;
using ERPKeeperCore.Enterprise.Models;


namespace ERPKeeperCore.Enterprise
{
    public class EnterpriseRepo : IDisposable
    {
        public ERPCoreDbContext ErpCOREDBContext { get; set; }

        private DAL.Accounting.ChartOfAccounts _chartOfAccountDal;
        private DAL.Accounting.SystemAccounts _systemAccountsDal;
        private DAL.Accounting.FiscalYears _fiscalYearsDal;
        private JournalEntries _JournalEntries;
        private JournalEntryTypes _JournalEntryTypes;
        private DAL.Company.DataItems _DataItems;



        private Projects _Projects;
        private Assets _Assets;
        private AssetTypes _AssetTypes;


        public AssetDeprecateSchedules AssetDeprecateSchedules;
        public ObsoleteAssets ObsoleteAssets;


        private Members _Members;
        private Items _Items;
        public DAL.Customers.Sales Sales;
        public DAL.Purchases Purchases;
        public DAL.Customers.Customers Customers;
        public DAL.Customers.ReceivePayments ReceivePayments;
        public SupplierPayments SupplierPayments;

        public Suppliers Suppliers;

        public Employees Employees;
        public DAL.Employees.EmployeePositions EmployeePositions;
        public DAL.Employees.EmployeePaymentTypes EmployeePaymentTypes;
        public DAL.Employees.EmployeePayments EmployeePayments;


        public Investors Investors;
        public ItemGroups ItemGroups;
        public Brands Brands;
        public Profiles Profiles;
        public TaxCodes TaxCodes;
        public TaxPeriods TaxPeriods;


        public Transactions Transactions;
        public FundTransfers FundTransfers;
        public LiabilityPayments LiabilityPayments;

        public IncomeTaxes IncomeTaxes;
        public Loans Loans;
        public Lends Lends;

        public EnterpriseRepo(string dbName = "tec", bool useLazyLoading = false)
        {
            DatabaseName = dbName;
            ErpCOREDBContext = new ERPCoreDbContext(dbName, useLazyLoading);
            Initial();
        }

        public void Initial()
        {
            Sales = new Sales(this);
            Purchases = new Purchases(this);
            Customers = new Customers(this);
            Suppliers = new Suppliers(this);
            Employees = new Employees(this);
            EmployeePositions = new DAL.Employees.EmployeePositions(this);
            EmployeePaymentTypes = new DAL.Employees.EmployeePaymentTypes(this);
            EmployeePayments = new DAL.Employees.EmployeePayments(this);

            Investors = new Investors(this);
            Brands = new Brands(this);
            ItemGroups = new ItemGroups(this);
            Profiles = new Profiles(this);
            TaxCodes = new TaxCodes(this);
            TaxPeriods = new TaxPeriods(this);
            FundTransfers = new FundTransfers(this);
            Transactions = new Transactions(this);
            Loans = new Loans(this);
            Lends = new Lends(this);
            IncomeTaxes = new IncomeTaxes(this);

            ReceivePayments = new DAL.Customers.ReceivePayments(this);
            SupplierPayments = new SupplierPayments(this);
            LiabilityPayments = new LiabilityPayments(this);

            AssetDeprecateSchedules = new AssetDeprecateSchedules(this);
            ObsoleteAssets = new ObsoleteAssets(this);
        }


        public DAL.Accounting.ChartOfAccounts ChartOfAccount
        {
            get
            {
                if (_chartOfAccountDal == null)
                    _chartOfAccountDal = new DAL.Accounting.ChartOfAccounts(this);
                return _chartOfAccountDal;
            }
        }
        public DAL.Accounting.SystemAccounts SystemAccounts
        {
            get
            {
                if (_systemAccountsDal == null)
                    _systemAccountsDal = new DAL.Accounting.SystemAccounts(this);
                return _systemAccountsDal;
            }
        }
        public DAL.Company.DataItems DataItems
        {
            get
            {
                if (_DataItems == null)
                    _DataItems = new DAL.Company.DataItems(this);
                return _DataItems;
            }
        }
        public Projects Projects
        {
            get
            {
                if (_Projects == null)
                    _Projects = new Projects(this);
                return _Projects;
            }
        }
        public Assets Assets
        {
            get
            {
                if (_Assets == null)
                    _Assets = new Assets(this);
                return _Assets;
            }
        }
        public AssetTypes AssetTypes
        {
            get
            {
                if (_AssetTypes == null)
                    _AssetTypes = new AssetTypes(this);
                return _AssetTypes;
            }
        }


        public Members Members
        {
            get
            {
                if (_Members == null)
                    _Members = new Members(this);
                return _Members;
            }
        }


        public Items Items
        {
            get
            {
                if (_Items == null)
                    _Items = new Items(this);
                return _Items;
            }
        }

        public DAL.Accounting.FiscalYears FiscalYears
        {
            get
            {
                if (_fiscalYearsDal == null)
                    _fiscalYearsDal = new DAL.Accounting.FiscalYears(this);
                return _fiscalYearsDal;
            }
        }


        public JournalEntries JournalEntries
        {
            get
            {
                if (_JournalEntries == null)
                    _JournalEntries = new JournalEntries(this);
                return _JournalEntries;
            }
        }
        public JournalEntryTypes JournalEntryTypes
        {
            get
            {
                if (_JournalEntryTypes == null)
                    _JournalEntryTypes = new JournalEntryTypes(this);
                return _JournalEntryTypes;
            }
        }
        public string? DatabaseName { get; set; }

        public void Dispose()
        {
            ErpCOREDBContext.Dispose();
        }
        public void SaveChanges()
        {
            ErpCOREDBContext.SaveChanges();
        }

        private void CreateSystemAccounts()
        {
            Console.WriteLine("> Create System Accounts");

            if (DataItems.Get(DataItemKey.DefaultSystemAccountAssign) == null)
            {
                SystemAccounts.AutoAssignSystemAccount();
                DataItems.Set(DataItemKey.DefaultSystemAccountAssign, true.ToString());
            }
        }

        public DateTime FirstDate => DataItems.FirstDate;



        private void SetFirstDate(DateTime firstDate)
        {
            Console.WriteLine("> Set FirstDate");
            DateTime verifyFirstDate = new DateTime(DateTime.Now.Year, firstDate.Month, firstDate.Day);
            ErpCOREDBContext.SaveChanges();
        }
    }
}
