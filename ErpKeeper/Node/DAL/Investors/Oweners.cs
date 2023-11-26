
using ERPKeeper.Node.Models.Equity;
using ERPKeeper.Node.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeper.Node.DAL.Profiles
{
    public class Investors : ERPNodeDalRepository
    {
        public Investors(Organization organization) : base(organization)
        {

        }

        public List<Investor> GetListAll() => erpNodeDBContext.Investors.ToList();


        public Investor Find(Guid id) => erpNodeDBContext.Investors.Find(id);

        public void UpdateStockCount()
        {
            erpNodeDBContext.Investors
                .ToList()
                .ForEach(i => i.UpdateStockCount());
            erpNodeDBContext.SaveChanges();
        }

        public Investor Create(Profile model)
        {
            var existModel = erpNodeDBContext.Investors.Find(model.Uid);

            if (existModel != null)
                return existModel;

            existModel = new Investor()
            {
                ProfileUid = model.Uid,
                Status = Models.Equity.Enums.InvestorStatus.Active,
            };

            erpNodeDBContext.Investors.Add(existModel);
            erpNodeDBContext.SaveChanges();
            return existModel;
        }

        public void Delete(Guid id)
        {
            var investor = erpNodeDBContext.Investors.Find(id);
            erpNodeDBContext.Investors.Remove(investor);
            erpNodeDBContext.SaveChanges();
        }
    }
}