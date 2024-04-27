using ERPKeeperCore.Enterprise.Models.Employment.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Employment
{
    public class EmployeePaymentItem
    {

        public Guid Id { get; set; }
        public String Memo { get; set; }

        public Guid? EmployeePaymentTypeId { get; set; }
        [ForeignKey("EmployeePaymentTypeId")]
        public virtual EmployeePaymentType EmployeePaymentType { get; set; }

        public Guid? EmployeePaymentId { get; set; }
        [ForeignKey("EmployeePaymentId")]
        public virtual EmployeePayment EmployeePayment { get; set; }

        public Decimal Amount { get; set; }

        public Decimal? EarningAmount
        {
            get
            {
                switch (EmployeePaymentType.PayDirection)
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
                switch (EmployeePaymentType.PayDirection)
                {
                    case PayDirection.Deduction:
                        return Amount;
                    case PayDirection.Eanring:
                    default:
                        return null;
                }

            }
        }

        public void UpdateAmount(EmployeePaymentItem paymentItem)
        {
            this.Amount = Math.Abs(paymentItem.Amount);
            this.Memo = paymentItem.Memo;
        }
    }
}