
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Transactions : ERPNodeDalRepository
    {
        public Transactions(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Transaction> GetAll()
        {
            return erpNodeDBContext.Transactions.ToList();
        }

        public Transaction? Find(Guid Id) => erpNodeDBContext.Transactions.Find(Id);

        public void Post_Ledgers()
        {
            //Capital Activities
            //organization.CapitalActivities.PostLedger();

            //Commercial Section
            organization.Sales.PostToTransactions();
            organization.ReceivePayments.PostToTransactions();
            organization.Purchases.Post_Ledgers();
            organization.SupplierPayments.PostToTransactions();

            //Financial Section
            organization.FundTransfers.PostToTransactions();
            organization.Loans.PostToTransactions();
            organization.Lends.PostToTransactions();
            organization.LendReturns.PostToTransactions();


            organization.LiabilityPayments.PostToLedgers();

            //Taxes Section
            //organization.TaxPeriods.PostLedger();
            organization.IncomeTaxes.PostToTransactions();

            //Employee Section
            organization.EmployeePayments.PostToTransactions();

            //Other Section
            organization.JournalEntries.PostToTransactions();

            //Assets
            organization.Assets.PostToTransactions();
            organization.AssetDeprecateSchedules.PostToTransactions();
            organization.ObsoleteAssets.PostToTransactions();


            //Other Section
            organization.IncomeTaxes.PostToTransactions();
            organization.TaxPeriods.PostToTransactions(false);
            organization.FiscalYears.PostToTransactions(false);

        }

        public void Clear_EmptyLedgers()
        {

            organization.ErpCOREDBContext.TransactionLedgers
                .Where(t => t.Debit == 0 && t.Credit == 0)
                .ExecuteDelete();
            organization.SaveChanges();
        }
    }
}