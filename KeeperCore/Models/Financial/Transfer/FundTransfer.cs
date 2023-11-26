using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Transactions;

namespace KeeperCore.ERPNode.Models.Financial.Transfer
{

    [Table("ERP_Finance_Transfer")]
    public class FundTransfer
    {
        [Key]
        public Guid Id { get; set; }

        public Models.Accounting.Enums.ERPObjectType TransactionType = Accounting.Enums.ERPObjectType.FundTransfer;
        public string Reference { get; set; }

        
        public DateTime TrnDate { get; set; }
        public FundTransferStatus Status { get; set; }

        public Guid? FiscalYearId { get; set; }
        [ForeignKey("FiscalYearId")]
        public virtual Accounting.FiscalYear FiscalYear { get; set; }



        public string Name =>
                string.Format("{0}-{1}/{2}", DocumentCode, DocumentGroup, No.ToString().PadLeft(3, '0'));
        public string DocumentGroup => this.TrnDate.ToString("yyMM");
        public string DocumentCode => Helpers.ObjectsHelper.TrCode(this.TransactionType);

        public int No { get; set; }



        public Decimal AmountwithDraw { get; set; }
        public Decimal AmountFee { get; set; }
        public Decimal AmountDeposit
        {
            get
            {
                return this.AmountwithDraw - this.AmountFee;
            }
        }



        /// <summary>
        /// Chart Of Account Detail
        /// </summary>
        public Guid? WithDrawAccountId { get; set; }
        [ForeignKey("WithDrawAccountId")]
        public virtual AccountItem WithDrawAccount { get; set; }


        public Guid? DepositAccountId { get; set; }
        [ForeignKey("DepositAccountId")]
        public virtual AccountItem DepositAccount { get; set; }



        public Guid? BankFeeAccountId { get; set; }
        [ForeignKey("BankFeeAccountId")]
        public virtual AccountItem BankFeeAccount { get; set; }




        public String Memo { get; set; }
        public LedgerPostStatus PostStatus { get; set; }

    }
}