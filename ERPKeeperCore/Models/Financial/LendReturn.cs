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
    public class LendReturn
    {

        public DateTime Date { get; set; }

        [Key]
        public Guid Id { get; set; }
        public String? Name { get; set; }
        public String? Reference { get; set; }
        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }



        public Guid LendId { get; set; }
        [ForeignKey("LendId")]
        public virtual Financial.Lend? Lend { get; set; }


        public Guid ReturnToAccountId { get; set; }
        [ForeignKey("ReturnToAccountId")]
        public virtual Accounting.Account? ReturnToAccount { get; set; }

        public decimal Amount { get; set; }
        public String? Description { get; set; }



        public void CreateTransaction()
        {
            if (this.Transaction == null)
            {
                Console.WriteLine("Creating LendReturn Transaction");

                this.Transaction = new Accounting.Transaction()
                {
                    Id = this.Id,
                    Date = this.Date,
                    Reference = this.Name,
                    Type = Accounting.Enums.TransactionType.LendReturn,
                    LendReturn = this,
                };
            }
            this.Transaction.ClearLedger();
            this.Transaction.UpdateBalance();
        }

    }
}