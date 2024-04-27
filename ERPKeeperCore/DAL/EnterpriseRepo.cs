using ERPKeeperCore.Enterprise.DBContext;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;




using ERPKeeperCore.Enterprise.Models;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class EnterpriseRepo : IDisposable
    {
        public ERPCoreDbContext ErpCOREDBContext { get; private set; }

        private DAL.Accounting.ChartOfAccounts _chartOfAccountDal;
        private DAL.Accounting.SystemAccounts _systemAccountsDal;
        private DAL.Accounting.FiscalYears _fiscalYearsDal;
        private DAL.Company.DataItems _DataItems;


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
