
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
        public Guid ProfileId => Profile.Id;
        public String? Code { get; set; }
        public EmployeeStatus Status { get; set; }

        public Guid? EmployeePositionId { get; set; }
        [ForeignKey("EmployeePositionId")]
        public virtual EmployeePosition? EmployeePosition { get; set; }


    }
}
