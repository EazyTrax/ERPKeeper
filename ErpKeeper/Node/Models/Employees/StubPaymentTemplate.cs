using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Employees
{
    [Table("ERP_Employee_PaymentTemplates")]
    public class EmployeePaymentTemplate
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }
        public String Name { get; set; }

        public virtual ICollection<EmployeePaymentTemplateItem> Items { get; set; }
    }
}
