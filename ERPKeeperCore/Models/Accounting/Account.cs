using KeeperCore.ERPNode.Models.Accounting.Enums;
using KeeperCore.ERPNode.Models.Financial;
using KeeperCore.ERPNode.Models.Taxes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Accounting
{

    [Table("ERP_Accounts")]
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Account Parent { get; set; }
        public virtual ICollection<Account> Child { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(30)]
        public string No { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public string CodeName { get; set; }

        public string Code => No;

        public bool IsLiquidity { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public void AddChild(Account accountItem)
        {
            accountItem.ParentId = Id;
        }

        [DefaultValue(false)]
        public bool IsFolder { get; set; }


        public AccountTypes Type { get; set; }
        public AccountSubTypes? SubType { get; set; }
        public bool IsFocus { get; set; } = false;


        public BankAccountType BankAccountType { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountBankName { get; set; }
        public string BankAccountBranch { get; set; }
        public bool IsCashEquivalent { get; set; }

        public LedgerPostStatus PostStatus { get; set; }



        #region OpeningBalance

        [Column(TypeName = "Money")]
        public decimal? OpeningBalance { get; set; }
        public decimal OpeningDebitBalance { get; set; }
        public decimal OpeningCreditBalance { get; set; }




        #endregion

        [DisplayFormat(DataFormatString = "N2")]
        public decimal CurrentBalance
        {
            get
            {
                switch (Type)
                {
                    case AccountTypes.Asset:
                    case AccountTypes.Expense:
                        return Debit - Credit;

                    case AccountTypes.Liability:
                    case AccountTypes.Capital:
                    case AccountTypes.Income:
                    default:
                        return Credit - Debit;
                }
            }
        }

        public DateTime? RecordedDate { get; set; }


        [Column(TypeName = "Money")]
        [DisplayFormat(DataFormatString = "N2")]
        [DefaultValue(0)]
        public decimal Credit { get; set; }

        [Column(TypeName = "Money")]
        [DisplayFormat(DataFormatString = "N2")]
        [DefaultValue(0)]
        public decimal Debit { get; set; }


        public decimal? ProfitMultiplyAmount { get; set; }
        public string EnglishName { get; set; }
        public int Order { get; set; }



        public virtual ICollection<TaxCode> TaxCodeAssignAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeCloseToAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeTaxAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeOutputTaxAccounts { get; set; }
        public virtual ICollection<TaxCode> TaxCodeInputTaxAccounts { get; set; }



        public virtual ICollection<Loan> Loan_DebitTo_AssetAccounts { get; set; }
        public virtual ICollection<Loan> Loan_CreditTo_LiabilityAccounts { get; set; }
        public virtual ICollection<Lend> Lend_CreditFrom_AssetAccounts { get; set; }
        public virtual ICollection<Lend> Lend_DebitTo_AssetAccounts { get; set; }
        public virtual ICollection<FundTransfer> FundTransfer_WithDrawAccounts { get; set; }
        public virtual ICollection<FundTransfer> FundTransfer_DepositAccounts { get; set; }
        public virtual ICollection<FundTransfer> FundTransfer_BankFeeAccounts { get; set; }


        public virtual ICollection<FiscalYear> FiscalYears_CloseToAccounts { get; set; }

        public virtual ICollection<Items.Item> Items_PurchaseAccount { get; set; }
        public virtual ICollection<Items.Item> Items_IncomeAccount { get; set; }

        public virtual ICollection<CapitalActivity> CapitalActivity_EquityAccounts { get; set; }
        public virtual ICollection<CapitalActivity> CapitalActivity_AssetAccounts { get; set; }


        public virtual ICollection<GeneralPayment> GeneralPayment_OptionalAssetAccounts { get; set; }
        public virtual ICollection<GeneralPayment> GeneralPayment_AssetAccounts { get; set; }
        public virtual ICollection<GeneralPayment> GeneralPayment_LiabilityAccounts { get; set; }
        public virtual ICollection<GeneralPayment> GeneralPayment_ReceivableAccounts { get; set; }

        public virtual ICollection<LendPayment> LendPayment_LendAccounts { get; internal set; }
        public virtual ICollection<LoanPayment> LoanPayment_LoanAccounts { get; internal set; }


        public Account()
        {

        }

        public Account(AccountTypes type, AccountSubTypes subType, bool folder, string AccountNo, string accountName, Account parent)
        {

            Type = type;
            SubType = subType;
            IsFolder = folder;
            No = AccountNo;
            Name = accountName;
        }

        public void ClearBalance()
        {
            Debit = 0;
            Credit = 0;
            RecordedDate = DateTime.Now;
        }

    }
}