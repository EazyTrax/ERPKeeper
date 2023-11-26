using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ERPKeeper.Node.Models.Financial.Payments.Enums;
using ERPKeeper.Node.Models.Transactions;

namespace ERPKeeper.Node.Models.Financial.Payments
{
    public class SupplierPayment : GeneralPayment
    {
        public SupplierPayment()
        {
            Uid = Guid.NewGuid();
        }

        public decimal TotalBillPaymentAmount => (this.Amount) - (this.AmountRetention + this.DiscountAmount);

        
    }
}




