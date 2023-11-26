using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ERPKeeper.Node.Models.Employees
{

    [Table("ERP_Employee_Payment_Periods")]
    public class EmployeePaymentPeriod
    {
        [Key]
        public Guid Uid { get; set; }
        public String Name { get; set; }

        public DateTime TrnDate { get; set; }
        public string TrGroup => this.TrnDate.ToString("yy-MM");
        public virtual ICollection<EmployeePayment> EmployeePayments { get; set; }


        //Must Be changes soon
        public decimal TotalEarning { get; set; }
        public decimal TotalDeduction { get; set; }
        public virtual decimal TotalPayment => TotalEarning - TotalDeduction;
        public virtual int PaymentCount => EmployeePayments?.Count() ?? 0;

        public String Description { get; set; }

        public Guid? PayFromAccountGuid { get; set; }
        [ForeignKey("PayFromAccountGuid")]
        public virtual Accounting.AccountItem PayFromAccount { get; set; }


        public void Update(EmployeePaymentPeriod model)
        {
            this.Name = TrGroup;
            this.Description = model.Description;
            this.TrnDate = model.TrnDate;
            this.PayFromAccountGuid = model.PayFromAccountGuid;

          
        }

        public void Calculate()
        {
            TotalEarning = EmployeePayments.Select(l => l.TotalEarning).DefaultIfEmpty(0).Sum();
            TotalDeduction = EmployeePayments.Select(l => l.TotalDeduction).DefaultIfEmpty(0).Sum();
        }
    }
}
