using KeeperCore.ERPNode.Models.ChartOfAccount;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Employees
{
    [Table("ERP_Employee_PaymentItems")]
    public class EmployeePaymentLine
    {
        [Key, Column("GID")]
        public Guid Id { get; set; }
        public String Memo { get; set; }


        public Guid? PaymentTypeId { get; set; }
        [ForeignKey("PaymentTypeId")]
        public virtual EmployeePaymentType PaymentType { get; set; }



        public Guid? EmployeePaymentId { get; set; }
        [ForeignKey("EmployeePaymentId")]
        public virtual EmployeePayment EmployeePayment { get; set; }





        public Decimal Amount { get; set; }

        public Decimal? EarningAmount
        {
            get
            {
                switch (PaymentType.PayDirection)
                {
                    case PayDirection.Eanring:
                        return Amount;
                    case PayDirection.Deduction:
                    default:
                        return null;
                }

            }
        }
        public Decimal? DeductionAmount
        {
            get
            {
                switch (PaymentType.PayDirection)
                {
                    case PayDirection.Deduction:
                        return Amount;
                    case PayDirection.Eanring:
                    default:
                        return null;
                }

            }
        }

        public void UpdateAmount(EmployeePaymentLine paymentItem)
        {
            this.Amount = Math.Abs(paymentItem.Amount);
            this.Memo = paymentItem.Memo;
        }
    }
}