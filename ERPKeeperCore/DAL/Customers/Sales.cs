using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using Microsoft.EntityFrameworkCore;

namespace ERPKeeperCore.Enterprise.DAL.Customers
{
    public class Sales : ERPNodeDalRepository
    {
        public Sales(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Sale> GetAll()
        {
            return erpNodeDBContext.Sales.ToList();
        }



        public Sale? Find(Guid Id) => erpNodeDBContext.Sales.Find(Id);

        public void CreateTransactions()
        {

            var sales = erpNodeDBContext
                .Sales
                .Where(s => s.TransactionId == null)
                .ToList();

            sales.ForEach(sale =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(sale.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{sale.Name}");
                    transaction = new Transaction()
                    {
                        Id = sale.Id,
                        Date = sale.Date,
                        Reference = sale.Name,
                        Type = Models.Accounting.Enums.TransactionType.Sale,
                        Sale = sale,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });
        }

        public void PostToTransactions(bool rePost = false)
        {
            CreateTransactions();

            var sales = erpNodeDBContext.Sales
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted || rePost)
                .OrderBy(s => s.Date).ToList();


            var ReceivableAccount = organization.SystemAccounts.AccountReceivable;
            var Discount_Given_Expense_Account = organization.SystemAccounts.GetAccount(DefaultAccountType.DiscountGiven);
            var IncomeAccount = organization.SystemAccounts.Income;

            int maxNo = sales.Count();
            sales.ForEach(sale =>
            {
                sale.AssignDefaultAccount(IncomeAccount, Discount_Given_Expense_Account, ReceivableAccount);
                sale.Post_Ledgers(maxNo--);
                erpNodeDBContext.SaveChanges();
            });

        }

        public Sale CreateNew(Sale model, Guid? customerId = null)
        {
            if (customerId != null)
                model.CustomerId = (Guid)customerId;

            model.Date = DateTime.Today;
            model.Status = SaleStatus.Open;
            model.No = GetNextInvoiceNumber();

            model.UpdateBalance();
            model.UpdateName();

            erpNodeDBContext.Sales.Add(model);
            erpNodeDBContext.SaveChanges();

            return model;
        }

        public void UnPostAll()
        {
            var sales = erpNodeDBContext.Sales
                .Where(s => s.IsPosted)
                .Include(s => s.Transaction)
                .ThenInclude(s => s.Ledgers)
                .ToList();

            sales.ForEach(sale =>
            {
                sale.UnPostLedger();
            });

            erpNodeDBContext.SaveChanges();
        }

        public void UpdateStatus()
        {
            var paidSalesButInvices = erpNodeDBContext.Sales
                .Where(s => s.Status != SaleStatus.Paid && s.ReceivePayment != null)
                .ToList();
            paidSalesButInvices.ForEach(sale => sale.Status = SaleStatus.Paid);
            erpNodeDBContext.SaveChanges();

        }

        public int GetNextInvoiceNumber() => (erpNodeDBContext.Sales
                .Select(a => (int?)a.No)
                .Max() ?? 0) + 1;

        public Sale CreateInvoice(SaleQuote saleQuote)
        {
            var model = new Sale()
            {
                Date = DateTime.Today,
                Status = SaleStatus.Open,
                No = GetNextInvoiceNumber(),
                CustomerId = saleQuote.CustomerId,
                Reference = saleQuote.Reference,
                ProfileAddesssId = saleQuote.ProfileAddesssId,
                Items = new List<SaleItem>()
            };

            foreach (var saleQuoteItem in saleQuote.Items.ToList())
            {
                var saleItem = saleQuoteItem.GetSaleItem();
                model.Items.Add(saleItem);
            }


            model.UpdateBalance();
            model.UpdateName();

            erpNodeDBContext.Sales.Add(model);
            erpNodeDBContext.SaveChanges();


            saleQuote.Status = SaleQuoteStatus.Close;
            saleQuote.SaleId = model.Id;

            return model;


        }
    }
}