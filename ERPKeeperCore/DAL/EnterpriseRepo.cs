using ERPKeeperCore.Enterprise.DBContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;




using ERPKeeperCore.Enterprise.Models;
using ERPKeeperCore.Enterprise.Models.Assets;
using ERPKeeperCore.Enterprise.Models.Items;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class EnterpriseRepo : IDisposable
    {
        public ERPCoreDbContext ErpCOREDBContext { get; private set; }

        private DAL.Accounting.ChartOfAccounts _chartOfAccountDal;
        private DAL.Accounting.SystemAccounts _systemAccountsDal;
        private DAL.Accounting.FiscalYears _fiscalYearsDal;
        private DAL.JournalEntries _JournalEntries;

        private DAL.Company.DataItems _DataItems;

        private DAL.Projects _Projects;
        private DAL.Assets _Assets;
        private DAL.AssetTypes _AssetTypes;


        private DAL.Members _Members;
        private DAL.Items _Items;



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
        public DAL.Company.DataItems DataItems
        {
            get
            {
                if (this._DataItems == null)
                    this._DataItems = new Company.DataItems(this);
                return _DataItems;
            }
        }
        public DAL.Projects Projects
        {
            get
            {
                if (this._Projects == null)
                    this._Projects = new Projects(this);
                return _Projects;
            }
        }
        public DAL.Assets Assets
        {
            get
            {
                if (this._Assets == null)
                    this._Assets = new Assets(this);
                return _Assets;
            }
        }
        public DAL.AssetTypes AssetTypes
        {
            get
            {
                if (this._AssetTypes == null)
                    this._AssetTypes = new AssetTypes(this);
                return _AssetTypes;
            }
        }


        public DAL.Members Members
        {
            get
            {
                if (this._Members == null)
                    this._Members = new Members(this);
                return _Members;
            }
        }


        public DAL.Items Items
        {
            get
            {
                if (this._Items == null)
                    this._Items = new Items(this);
                return _Items;
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


        public DAL.JournalEntries JournalEntries
        {
            get
            {
                if (this._JournalEntries == null)
                    this._JournalEntries = new JournalEntries(this);
                return _JournalEntries;
            }
        }

        public String? DatabaseName { get; private set; }

        public void Dispose()
        {
            this.ErpCOREDBContext.Dispose();
        }
        public void SaveChanges()
        {
            ErpCOREDBContext.SaveChanges();
        }
        public EnterpriseRepo(string dbName = "erpCoreDB.TEC", bool migratedDB = false)
        {
            this.DatabaseName = dbName;
            this.ErpCOREDBContext = new ERPCoreDbContext(dbName, migratedDB);
        }


        private void CreateSystemAccounts()
        {
            Console.WriteLine("> Create System Accounts");

            if (this.DataItems.Get(DataItemKey.DefaultSystemAccountAssign) == null)
            {
                this.SystemAccounts.AutoAssignSystemAccount();
                this.DataItems.Set(DataItemKey.DefaultSystemAccountAssign, true.ToString());
            }
        }

        public DateTime FirstDate => this.DataItems.FirstDate;
        private void SetFirstDate(DateTime firstDate)
        {
            Console.WriteLine("> Set FirstDate");
            DateTime verifyFirstDate = new DateTime(DateTime.Now.Year, firstDate.Month, firstDate.Day);
            this.ErpCOREDBContext.SaveChanges();
        }
    }
}
