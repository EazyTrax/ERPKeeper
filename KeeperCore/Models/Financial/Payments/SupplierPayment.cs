using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using KeeperCore.ERPNode.Models.Financial.Payments.Enums;
using KeeperCore.ERPNode.Models.Transactions;

namespace KeeperCore.ERPNode.Models.Financial.Payments
{
    public class SupplierPayment : GeneralPayment
    {
        public SupplierPayment()
        {
            
        }

        public decimal TotalBillPaymentAmount => (this.Amount) - (this.AmountRetention + this.DiscountAmount);

        
    }
}




