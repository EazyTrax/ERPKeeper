using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace KeeperCore.ERPNode.Models.Employees
{

    [Table("ERP_Employee_Payment_Periods")]
    public class EmployeePaymentPeriod
    {
        [Key]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime TrnDate { get; set; }
        public string TrGroup => this.TrnDate.ToString("yy-MM");
        public decimal TotalEarning { get; set; }
        public decimal TotalDeduction { get; set; }
        public virtual decimal TotalPayment => TotalEarning - TotalDeduction;
        public virtual int PaymentCount => EmployeePayments?.Count() ?? 0;
        public String Description { get; set; }


        public Guid? PayFromAccountId { get; set; }
        [ForeignKey("PayFromAccountId")]
        public virtual ChartOfAccount.AccountItem PayFromAccount { get; set; }

        public virtual ICollection<EmployeePayment> EmployeePayments { get; set; }


        public void Update(EmployeePaymentPeriod paymentType)
        {
            this.Name = TrGroup;
            this.Description = paymentType.Description;
            this.TrnDate = paymentType.TrnDate;
        }

        public void Calculate()
        {
            TotalEarning = EmployeePayments.Select(l => l.TotalEarning).DefaultIfEmpty(0).Sum();
            TotalDeduction = EmployeePayments.Select(l => l.TotalDeduction).DefaultIfEmpty(0).Sum();
        }
    }
}
