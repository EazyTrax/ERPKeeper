
using ERPKeeper.Node.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeper.Node.DAL.Profiles
{
    public class Employees : ERPNodeDalRepository
    {
        public Employees(Organization organization) : base(organization)
        {

        }

        public IQueryable<Employee> GetAll() => erpNodeDBContext.Employees;

        public Employee Create(Models.Profiles.Profile model)
        {
            var existModel = erpNodeDBContext.Employees.Find(model.Uid);

            if (existModel != null)
                return existModel;

            existModel = new Employee()
            {
                ProfileUid = model.Uid,
                Status = EmployeeStatus.Active,
                OnBoardDate = DateTime.Now,
            };

            erpNodeDBContext.Employees.Add(existModel);
            erpNodeDBContext.SaveChanges();
            return existModel;
        }

        public Employee Find(Guid id) => erpNodeDBContext.Employees.Find(id);

        public void Delete(Guid id)
        {
            var employee = organization.Employees.Find(id);
            erpNodeDBContext.Employees.Remove(employee);
            organization.SaveChanges();
        }

        public Employee Search(string searchDocument)
        {
            var profile = erpNodeDBContext.Employees
                .Where(c => c.Profile.TaxNumber == searchDocument || c.Profile.ShotName == searchDocument)
                .FirstOrDefault();
            return profile;
        }
    }
}