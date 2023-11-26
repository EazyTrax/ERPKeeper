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
    [Table("ERP_Employee_PaymentTemplateItems")]
    public class EmployeePaymentTemplateItem
    {
        [Key]
        public Guid Uid { get; set; }

        [ForeignKey("PaymentTemplate")]
        public Guid PaymentTemplateId { get; set; }
        public virtual EmployeePaymentTemplate PaymentTemplate { get; set; }




        [ForeignKey("PaymentType")]
        public Guid? PaymentTypeGuid { get; set; }
        public virtual EmployeePaymentType PaymentType { get; set; }

    }
}
