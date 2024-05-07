
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Employees;
using ERPKeeperCore.Enterprise.Models.Employees.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class EmployeePayments : ERPNodeDalRepository
    {
        public EmployeePayments(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<EmployeePayment> GetAll()
        {
            return erpNodeDBContext.EmployeePayments.ToList();
        }
        public int Count() => erpNodeDBContext.EmployeePayments.Count();


        public EmployeePayment? Find(Guid Id) => erpNodeDBContext.EmployeePayments.Find(Id);

        public void PostToTransactions()
        {
            this.CreateTransactions();

            var employeePayments = erpNodeDBContext.EmployeePayments
                .Where(s => s.TransactionId != null)
                .Where(s => !s.IsPosted)
                .OrderBy(s => s.EmployeePaymentPeriod.Date).ToList();

            employeePayments.ForEach(model =>
            {
                if (model.EmployeePaymentPeriod.PayFromAccountId == null)
                {
                    model.EmployeePaymentPeriod.PayFromAccountId =
                    organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Cash).Id;
                }

                model.PostToTransaction();
                erpNodeDBContext.SaveChanges();
            });
        }

        public void CreateTransactions()
        {
            var employeePayments = erpNodeDBContext
                .EmployeePayments
                .Where(s => s.TransactionId == null)
                .ToList();

            employeePayments.ForEach(model =>
            {
                var transaction = erpNodeDBContext.Transactions.Find(model.Id);

                if (transaction == null)
                {
                    Console.WriteLine($"Create TR:{model.Name}");
                    transaction = new Transaction()
                    {
                        Id = model.Id,
                        Date = model.EmployeePaymentPeriod.Date,
                        Reference = model.Name,
                        Type = Models.Accounting.Enums.TransactionType.EmployeePayment,
                        EmployeePayment = model,
                    };
                    erpNodeDBContext.Transactions.Add(transaction);
                    erpNodeDBContext.SaveChanges();
                }
            });

        }
    }
}