using ERPKeeperCore.Enterprise.Models.Financial.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Financial
{
    public class LoanReturn
    {

        [Key]
        public Guid Id { get; set; }
        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }



        public DateTime Date { get; set; }

        public Guid LoanId { get; set; }
        [ForeignKey("LoanId")]
        public virtual Financial.Loan? Loan { get; set; }





        public Guid ReturnFromAccountId { get; set; }
        [ForeignKey("ReturnFromAccountId")]
        public virtual Accounting.Account? ReturnFromAccount { get; set; }

        public decimal Amount { get; set; }
        public String? Description { get; set; }
    }
}