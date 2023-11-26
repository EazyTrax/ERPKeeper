using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Financial.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeper.Node.Models.Accounting
{

    [Table("ERP_Accounts")]
    public class AccountItem
    {
        [Key]
        public Guid Uid { get; set; }
        [MaxLength(30)]
        public String No { get; set; }

        [MaxLength(255)]
        public String Name { get; set; }
        public String CodeName { get; set; }
        public string Code => this.No;

        public bool IsLiquidity { get; set; }

        [MaxLength(500)]
        public String Description { get; set; }
        public AccountTypes Type { get; set; }

        [Column("AccountSubType")]
        public AccountSubTypes? SubEnumType { get; set; }

        public AccountReportGroup ReportGroup { get; set; }


        public String SpanBadge
        {

            get
            {
                switch (this.Type)
                {
                    case AccountTypes.Asset:
                        return "<span class=\"badge badge-success\">A</span>";
                    case AccountTypes.Liability:
                        return "<span class=\"badge badge-warning\">L</span>";
                    case AccountTypes.Capital:
                        return "<span class=\"badge badge-primary\">C</span>";
                    case AccountTypes.Income:
                        return "<span class=\"badge badge-info\">I</span>";
                    case AccountTypes.Expense:
                        return "<span class=\"badge badge-danger\">E</span>";
                    default:
                        return "";
                }
            }
        }

        public String SpanCodeBadge
        {
            get
            {
                string returnBadge = "";
                switch (this.Type)
                {
                    case AccountTypes.Asset:
                        returnBadge = "<span class=\"badge\"> {0}</span>";
                        break;
                    case AccountTypes.Liability:
                        returnBadge = "<span class=\"badge\"> {0}</span>";
                        break;
                    case AccountTypes.Capital:
                        returnBadge = "<span class=\"badge\"> {0}</span>";
                        break;
                    case AccountTypes.Income:
                        returnBadge = "<span class=\"badge\"> {0}</span>";
                        break;
                    case AccountTypes.Expense:
                        returnBadge = "<span class=\"badge\"> {0}</span>";
                        break;
                }

                return string.Format(returnBadge, this.Code);

            }
        }





        [DefaultValue(false)]
        [Column("IsPreviewDisplay")]
        public bool IsFocus { get; set; }


        // public BankAccountType BankAccountType { get; set; }
        public String BankAccountNumber { get; set; }

        public String BankAccountBankName { get; set; }

        public String BankAccountBranch { get; set; }

        public bool IsCashEquivalent { get; set; }

        public LedgerPostStatus PostStatus { get; set; }



        #region OpeningBalance

        [Column(TypeName = "Money")]
        public Decimal? OpeningBalance { get; set; }
        public Decimal OpeningDebitBalance { get; set; }
        public Decimal OpeningCreditBalance { get; set; }




        #endregion


        [DisplayFormat(DataFormatString = "N2")]
        public Decimal CurrentBalance
        {
            get
            {
                switch (Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return (this.Debit) - (Credit);

                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                    default:
                        return (Credit) - (this.Debit);
                }
            }
        }

        public Decimal TotalDebit { get { return Math.Max(Debit - Credit, 0); } }
        public Decimal TotalCredit { get { return Math.Max(Credit - Debit, 0); } }


        [Column("BalanceRecordDate")]
        public DateTime? CurrentBalanceRecordDate { get; set; }


        [Column(TypeName = "Money")]
        [DisplayFormat(DataFormatString = "N2")]
        [DefaultValue(0)]
        public Decimal Credit { get; set; }

        [Column(TypeName = "Money")]
        [DisplayFormat(DataFormatString = "N2")]
        [DefaultValue(0)]
        public Decimal Debit { get; set; }


        public Decimal? ProfitMultiplyAmount { get; set; }
        public string EnglishName { get; set; }
        public int Order { get; set; }

        public AccountItem()
        {
            this.Uid = Guid.NewGuid();
        }

        public AccountItem(AccountTypes type, AccountSubTypes subType, string AccountNo, string accountName, AccountItem parent)
        {
            this.Uid = Guid.NewGuid();
            this.Type = type;
            this.SubEnumType = subType;
            this.No = AccountNo;
            this.Name = accountName;
        }

        public void ClearBalance()
        {
            this.Debit = 0;
            this.Credit = 0;
            this.CurrentBalanceRecordDate = DateTime.Now;
        }

    }
}