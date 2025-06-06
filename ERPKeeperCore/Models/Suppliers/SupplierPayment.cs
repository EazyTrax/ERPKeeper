﻿using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using ERPKeeperCore.Enterprise.Models.Accounting;


namespace ERPKeeperCore.Enterprise.Models.Suppliers
{
    public class SupplierPayment
    {
        [Key]
        public Guid Id { get; set; }
        public PaymentStatus Status { get; set; }

        public bool IsPosted { get; set; }
        public Guid? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public virtual Accounting.Transaction? Transaction { get; set; }


        public Guid PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual Purchase? Purchase { get; set; }


        public String? Reference { get; set; }
        public String? Memo { get; set; }
        public int No { get; set; }
        public String? Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;

        public Decimal Amount { get; set; }
        public Decimal AmountExcludeTax { get; set; }
        public Decimal AmountDiscount { get; set; }
        public Decimal AmountAfterDiscount => Amount - AmountDiscount;

        public Decimal AmountRetention { get; set; }
        public Decimal AmountAfterDiscountAndRetention => (Amount - AmountDiscount) - AmountRetention;

        public Decimal AmountBankFee { get; set; }




        //Cr.
        public Guid? PayFrom_AssetAccountId { get; set; }
        [ForeignKey("PayFrom_AssetAccountId")]
        public virtual Accounting.Account? AssetAccount_PayFrom { get; set; }


        // Dr.
        public Guid? LiablityAccount_SupplierPayableId { get; set; }
        [ForeignKey("LiablityAccount_SupplierPayableId")]
        public virtual Accounting.Account? LiablityAccount_SupplierPayable { get; set; }


        public Guid? RetentionTypeId { get; set; }
        [ForeignKey("RetentionTypeId")]
        public virtual Financial.RetentionType? RetentionType { get; set; }

        public Guid? RetentionGroupId { get; set; }
        [ForeignKey("RetentionGroupId")]
        public virtual Financial.RetentionPeriod? RetentionGroup { get; set; }





        public Guid? IncomeAccount_DiscountTakenId { get; set; }
        [ForeignKey("IncomeAccount_DiscountTakenId")]
        public virtual Account? IncomeAccount_DiscountTaken { get; set; }


        public Guid? ExpenseAccount_BankFeeId { get; set; }
        [ForeignKey("ExpenseAccount_BankFeeId")]
        public virtual Account? ExpenseAccount_BankFee { get; set; }



        public void CreateTransaction()
        {
            if (this.Transaction == null)
            {
                Console.WriteLine("Creating Transaction");
                this.Transaction = new Accounting.Transaction()
                {
                    Id = this.Id,
                    Date = this.Date,
                    Reference = this.Name,
                    Type = Accounting.Enums.TransactionType.SupplierPayment,
                    SupplierPayment = this,
                };
            }
        }


        public string GetThaiRDReport(int order)
        {
            string date = this.Date.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
            string profile = this.Purchase?.Supplier?.Profile?.Name ?? "NA";
            string taxId = this.Purchase?.Supplier?.Profile?.TaxNumber ?? "NA";

            string address = this.Purchase?.Supplier?.Profile?.Addresses?.FirstOrDefault()?.AddressLine ?? "NA";
            if (address != null && address.Length > 30)
            {
                address = address.Substring(0, 28);
                address = address.Replace(Environment.NewLine, "");
            }

            string branchNo = this.Purchase?.Supplier?.Profile?.Addresses?.FirstOrDefault()?.Number ?? "NA";


            string retentionStr = "";
            retentionStr += $"{this.RetentionType?.Name}";
            retentionStr += $"|{order}";
            retentionStr += $"|{taxId}";
            retentionStr += $"|{branchNo}";
            retentionStr += $"|{this.Purchase?.Supplier?.Profile?.Name ?? "NA"}";//5
            retentionStr += $"|{address}";
            retentionStr += $"|{date}";
            retentionStr += $"|{this.RetentionType?.PaymentType}";
            retentionStr += $"|{this.RetentionType?.Rate}";
            retentionStr += $"|{this.Purchase.LinesTotalAfterDiscount}";
            retentionStr += $"|{this.AmountRetention}";//15
            retentionStr += $"|1";
            return retentionStr;
        }



        public void Post_Ledgers()
        {
            Console.WriteLine($">Post SupplierPayments:{this.Name} From {this.Purchase.Name}");

            if (this.Transaction == null)
                return;

            if (this.AssetAccount_PayFrom == null)
            {
                Console.WriteLine($">> Fail >> No pay to Account");
                return;
            }

            this.Transaction.ClearLedger();
            this.Transaction.Date = this.Date;
            this.Transaction.Reference = this.Reference;
            this.Transaction.PostedDate = DateTime.Today;



            // Dr. 
            this.Transaction.AddDebit(this.LiablityAccount_SupplierPayable, this.Amount);

            // Cr.
            if (this.AmountDiscount > 0)
                this.Transaction.AddCredit(this.IncomeAccount_DiscountTaken, this.AmountDiscount);

            if (this.RetentionTypeId != null)
                this.Transaction.AddCredit(this.RetentionType.RetentionTo_LiabilityAccount, this.AmountRetention);

            this.Transaction.AddCredit(this.AssetAccount_PayFrom, this.AmountAfterDiscountAndRetention);


            if (this.AmountBankFee > 0)
            {
                //Dr.
                this.Transaction.AddDebit(this.ExpenseAccount_BankFee, this.AmountBankFee);
                //Cr.
                this.Transaction.AddCredit(this.AssetAccount_PayFrom, this.AmountBankFee);
            }



            IsPosted = this.Transaction.UpdateBalance();
        }


        public void UnPostLedger()
        {
            Console.WriteLine($">UnPost  SP:{this.Name}");

            if (Transaction == null)
            {
                this.IsPosted = false;
                return;
            }
            else
            {
                this.Transaction.ClearLedger();
                this.Transaction.Date = this.Date;
                this.Transaction.Reference = this.Reference;
                this.Transaction.Name = this.Name;
                this.IsPosted = false;
            }
        }
        public void UpdateBalance()
        {
            this.Amount = this.Purchase.Total;
            this.AmountRetention = this.RetentionType?.GetRetentionAmount(this.Purchase.LinesTotalAfterDiscount) ?? 0;
            this.Name = $"CRP-{Date.Year}{Date.Month}-{this.No.ToString()}";
        }



    }
}
