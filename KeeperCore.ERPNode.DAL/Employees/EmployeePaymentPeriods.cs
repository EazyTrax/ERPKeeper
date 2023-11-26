
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Employees;

namespace KeeperCore.ERPNode.DAL.Employees
{
    public class EmployeePaymentPeriods : ERPNodeDalRepository
    {
        public EmployeePaymentPeriods(Organization organization) : base(organization)
        {

        }

        public List<EmployeePaymentPeriod> GetAll()
        {
            return erpNodeDBContext.EmployeePaymentPeriods.ToList();
        }
        public List<EmployeePaymentPeriod> GetAll(DateTime date)
        {
            return erpNodeDBContext.EmployeePaymentPeriods
                .Where(p => p.TrnDate.Month == date.Month && p.TrnDate.Year == date.Year)
                .ToList();
        }

        public EmployeePaymentPeriod Find(Guid id) => erpNodeDBContext.EmployeePaymentPeriods.Find(id);

        public void Refresh()
        {
            organization.ErpNodeDBContext.EmployeePaymentPeriods.ToList()
                    .ForEach(payment =>
                    {
                        payment.Name = payment.TrGroup;
                        Console.WriteLine($"{payment.Name}");

                    });
            organization.ErpNodeDBContext.SaveChanges();

            var removeEP = organization.ErpNodeDBContext.EmployeePaymentPeriods.ToList().Where(ep => ep.TotalEarning == 0).ToList();
            organization.ErpNodeDBContext.EmployeePaymentPeriods.RemoveRange(removeEP);
            organization.ErpNodeDBContext.SaveChanges();
        }

        public EmployeePaymentPeriod CreateNew(DateTime date)
        {

            var newPeriod = new EmployeePaymentPeriod()
            {
                Id = Guid.NewGuid(),
                TrnDate = date.Date
            };
            newPeriod.Name = newPeriod.TrGroup;
            erpNodeDBContext.EmployeePaymentPeriods.Add(newPeriod);
            erpNodeDBContext.SaveChanges();

            return newPeriod;
        }



        public EmployeePaymentPeriod FindByDate(DateTime date, bool createIfNotFound = false)
        {
            var period = this.erpNodeDBContext.EmployeePaymentPeriods
                .Where(p => p.TrnDate.Month == date.Month && p.TrnDate.Year == date.Year)
                .OrderByDescending(p=>p.TrnDate)
                .FirstOrDefault();

            if (period != null)
                return period;
            else if (createIfNotFound)
                return this.CreateNew(date);
            else return null;
        }
    }
}