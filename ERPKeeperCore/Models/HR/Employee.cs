using ERPKeeperCore.Enterprise.DAL.Employees;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Employees.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Employees
{


    public partial class Employee
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public virtual Profiles.Profile? Profile { get; set; }

        public String? Code { get; set; }
        public EmployeeStatus Status { get; set; }

        public Guid? EmployeePositionId { get; set; }
        [ForeignKey("EmployeePositionId")]
        public virtual EmployeePosition? EmployeePosition { get; set; }

        public Decimal Salary { get; set; }
        public Decimal TotalEarn { get; set; }
        public Decimal TotalDeduct { get; set; }


        public virtual ICollection<EmployeePayment> EmployeePayments { get; set; }
        public virtual ICollection<EmployeeLeaveRecord> EmployeeLeaveRecords { get; set; }
        public virtual ICollection<EmployeePositions> EmployeePositions { get; set; }
        public virtual ICollection<EmployeeBenefit> EmployeeBenefits { get; set; }
        public virtual ICollection<EmployeeCompensation> EmployeeCompensations { get; set; }



        public void UpdateBalance()
        {
            TotalEarn = this.EmployeePayments.Sum(p => p.TotalEarning);
            TotalDeduct = this.EmployeePayments.Sum(p => p.TotalDeduction);
        }
    }
}
