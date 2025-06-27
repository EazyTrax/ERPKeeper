using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public class EmployeePaymentPeriod
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public DateTime Date { get; set; }
        public string? TrGroup => this.Date.ToString("yy-MM");

        public virtual ICollection<EmployeePayment> EmployeePayments { get; set; } = new List<EmployeePayment>();


        public decimal TotalEarning { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal TotalPayment => TotalEarning - TotalDeduction;
        public int PaymentCount { get; set; }



        public string? Description { get; set; }

        public Guid? PayFromAccountId { get; set; }
        [ForeignKey("PayFromAccountId")]
        public virtual Accounting.Account PayFromAccount { get; set; }

        public void UpdateBalance()
        {
            if (EmployeePayments == null)
                return;

            foreach (var employeePayment in EmployeePayments)
            {
                employeePayment.UpdateBalance();
            }


            PaymentCount = EmployeePayments.Count();
            TotalEarning = EmployeePayments.Select(l => l.TotalEarning).DefaultIfEmpty(0).Sum();
            TotalDeduction = EmployeePayments.Select(l => l.TotalDeduction).DefaultIfEmpty(0).Sum();

        }

        public void Update(EmployeePaymentPeriod model)
        {
            this.Name = TrGroup;
            this.Description = model.Description;
            this.Date = model.Date;
            this.PayFromAccountId = model.PayFromAccountId;


        }

        public void UpdateName()
        {
            this.Name = TrGroup;
        }
    }
}
