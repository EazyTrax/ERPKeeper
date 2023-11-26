using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Transactions.Commercials;
using ERPKeeper.Node.Models.Transactions;

namespace ERPKeeper.Node.Models.Financial.Payments
{
    public class ReceivePayment : GeneralPayment
    {
        public ReceivePayment()
        {
            Uid = Guid.NewGuid();
        }

      
    }
}




