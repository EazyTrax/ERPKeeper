﻿
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

namespace ERPKeeperCore.Enterprise.DAL.Employees
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

        public void PostLedgers()
        {
            CreateTransactions();

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

                model.Post_Ledger();
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

        public void Post(EmployeePayment model)
        {
            if (model.EmployeePaymentPeriod.PayFromAccountId == null)
            {
                model.EmployeePaymentPeriod.PayFromAccountId =
                organization.SystemAccounts.GetAccount(Models.Accounting.Enums.DefaultAccountType.Cash).Id;
            }

            model.Post_Ledger();
            erpNodeDBContext.SaveChanges();
        }

        public void UnPostLedgers(EmployeePayment model)
        {

            model.UnPost_Ledger();
            erpNodeDBContext.SaveChanges();
        }
        public void UnPostLedgers()
        {
            erpNodeDBContext.EmployeePayments
                      .ToList()
                      .ForEach(model =>
                      {
                          model.UnPost_Ledger();
                      });

            erpNodeDBContext.SaveChanges();
        }

        public void ReOrder()
        {
            var payments = this.erpNodeDBContext.EmployeePayments
                          .Where(x => x.EmployeePaymentPeriodId != null)
                          .OrderBy(x => x.EmployeePaymentPeriod.Date)
                          .ThenBy(x => x.Employee.Id)
                          .ToList();

            int order = 0;

            foreach (var payment in payments)
            {
                payment.No = ++order;
                payment.UpdateBalance();
                payment.UpdateName();
            }


            erpNodeDBContext.SaveChanges();
        }

        public void Remove(EmployeePayment model)
        {
            if (model.IsPosted)
                throw new Exception("Cannot delete posted payment");

            model.RemoveAllItems();
            model.Transaction = null;

            this.erpNodeDBContext.EmployeePayments.Remove(model);
            this.SaveChanges();


        }
    }
}