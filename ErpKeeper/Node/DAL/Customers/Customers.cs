
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Profiles;
using ERPKeeper.Node.Models.Customers;
using ERPKeeper.Node.Models.Customers.Enums;

namespace ERPKeeper.Node.DAL.Profiles
{
    public class Customers : ERPNodeDalRepository
    {
        public Customers(Organization organization) : base(organization)
        {

        }
        public int Count() => erpNodeDBContext.Customers.Count();

        public List<Customer> GetAll() => erpNodeDBContext.Customers.ToList();

        public IQueryable<Customer> GetByStatus(ProfileQueryType Type = Models.Profiles.ProfileQueryType.All)
        {
            switch (Type)
            {
                case Models.Profiles.ProfileQueryType.Active:
                    return erpNodeDBContext.Customers.Where(c => c.Status == CustomerStatus.Active);

                case ERPKeeper.Node.Models.Profiles.ProfileQueryType.InActive:
                    return erpNodeDBContext.Customers.Where(c => c.Status == CustomerStatus.InActive);

                case ERPKeeper.Node.Models.Profiles.ProfileQueryType.All:
                default:
                    return erpNodeDBContext.Customers;
            }
        }

        public Customer Find(Guid id) => erpNodeDBContext.Customers.Find(id);

        public void Delete(Customer customer)
        {
            erpNodeDBContext.Customers.Remove(customer);
            erpNodeDBContext.SaveChanges();
        }

        public Customer Create(Profile model)
        {
            var existModel = erpNodeDBContext.Customers.Find(model.Uid);

            if (existModel != null)
                return existModel;

            existModel = new Customer()
            {
                ProfileUid = model.Uid,
                Status = CustomerStatus.Active,
            };

            erpNodeDBContext.Customers.Add(existModel);
            erpNodeDBContext.SaveChanges();
            return existModel;
        }

        public void UpdateSalesBalance()
        {
            var BalanceTables = erpNodeDBContext.Sales
                .GroupBy(o => o.ProfileGuid)
                .ToList()
                .Select(go => new
                {
                    Profile = go.Select(i => i.Profile).FirstOrDefault(),
                    TotalSale = go.Sum(i => i.Total),
                    CountSale = go.Count(),
                    TotalBalance = go.Sum(i => i.TotalBalance),
                    CountBalance = go.Where(i => i.TotalBalance > 0).Count(),
                })
                .ToList();


            erpNodeDBContext.Customers.ToList().ForEach(p =>
            {
                p.SumSaleBalance = 0;
                p.TotalBalance = 0;
                p.CountSales = 0;
            });

            BalanceTables.ForEach(b =>
            {
                b.Profile.Customer.SumSaleBalance = b.TotalSale;
                b.Profile.Customer.CountSales = b.CountSale;
                b.Profile.Customer.TotalBalance = b.TotalBalance;
                b.Profile.Customer.CountBalance = b.CountBalance;
            });

            erpNodeDBContext.SaveChanges();

        }

        public Customer Search(string searchString)
        {
            var customer = erpNodeDBContext.Customers
                .Where(c => c.Profile.TaxNumber == searchString || c.Profile.ShotName == searchString)
                .FirstOrDefault();
            return customer;
        }


        public List<Customer> GetTopSales(int amount = 30)
        {
            var profiles = erpNodeDBContext.Customers
                     .OrderByDescending(c => c.SumSaleBalance)
                     .Take(amount)
                     .ToList();
            return profiles;
        }
    }
}