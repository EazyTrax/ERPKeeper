using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial.Transfer;
using KeeperCore.ERPNode.Models.Taxes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.ChartOfAccount
{

    [Table("ERP_Accounts")]
    public class AccountItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual AccountItem Parent { get; set; }
        public virtual ICollection<AccountItem> Child { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(30)]
        public String No { get; set; }

        [MaxLength(255)]
        public String Name { get; set; }

        public String CodeName { get; set; }

        public string Code => this.No;

        public bool IsLiquidity { get; set; }

        [MaxLength(500)]
        public String Description { get; set; }

        public void AddChild(AccountItem accountItem)
        {
            accountItem.ParentId = this.Id;
        }

        [DefaultValue(false)]
        public bool IsFolder { get; set; }


        public AccountTypes Type { get; set; }
        public AccountSubTypes? SubType { get; set; }
        public bool IsFocus { get; set; } = false;


        public BankAccountType BankAccountType { get; set; }
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

        public DateTime? RecordedDate { get; set; }


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



        public virtual ICollection<TaxCode> TaxCodeAssignAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeCloseToAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeTaxAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeOutputTaxAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeInputTaxAccounts { get; set; }



        public virtual ICollection<Models.Financial.Loans.Loan> Loan_DebitTo_AssetAccounts { get; set; }
        public virtual ICollection<Models.Financial.Loans.Loan> Loan_CreditTo_LiabilityAccounts { get; set; }
        public virtual ICollection<Models.Financial.Lends.Lend> Lend_CreditFrom_AssetAccounts { get; set; }
        public virtual ICollection<Models.Financial.Lends.Lend> Lend_DebitTo_AssetAccounts { get; set; }
        public virtual ICollection<Models.Financial.Transfer.FundTransfer> FundTransfer_WithDrawAccounts { get; set; }
        public virtual ICollection<Models.Financial.Transfer.FundTransfer> FundTransfer_DepositAccounts { get; set; }
        public virtual ICollection<Models.Financial.Transfer.FundTransfer> FundTransfer_BankFeeAccounts { get; set; }


        public virtual ICollection<Models.Accounting.FiscalYear> FiscalYears_CloseToAccounts { get; set; }

        public virtual ICollection<Models.Items.Item> Items_PurchaseAccount { get; set; }
        public virtual ICollection<Models.Items.Item> Items_IncomeAccount { get; set; }

        public virtual ICollection<Models.Equity.CapitalActivity> CapitalActivity_EquityAccounts { get; set; }
        public virtual ICollection<Models.Equity.CapitalActivity> CapitalActivity_AssetAccounts { get; set; }


        public virtual ICollection<Models.Financial.Payments.GeneralPayment> GeneralPayment_OptionalAssetAccounts { get; set; }
        public virtual ICollection<Models.Financial.Payments.GeneralPayment> GeneralPayment_AssetAccounts { get; set; }
        public virtual ICollection<Models.Financial.Payments.GeneralPayment> GeneralPayment_LiabilityAccounts { get; set; }
        public virtual ICollection<Models.Financial.Payments.GeneralPayment> GeneralPayment_ReceivableAccounts { get; set; }

        public virtual ICollection<Models.Financial.Lends.LendPayment> LendPayment_LendAccounts { get; internal set; }
        public virtual ICollection<Models.Financial.Loans.LoanPayment> LoanPayment_LoanAccounts { get; internal set; }


        public AccountItem()
        {

        }

        public AccountItem(AccountTypes type, AccountSubTypes subType, bool folder, string AccountNo, string accountName, AccountItem parent)
        {

            this.Type = type;
            this.SubType = subType;
            this.IsFolder = folder;
            this.No = AccountNo;
            this.Name = accountName;
        }

        public void ClearBalance()
        {
            this.Debit = 0;
            this.Credit = 0;
            this.RecordedDate = DateTime.Now;
        }

    }
}