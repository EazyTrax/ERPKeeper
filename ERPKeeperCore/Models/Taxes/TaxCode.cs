using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Taxes.Enums;

namespace ERPKeeperCore.Enterprise.Models.Taxes
{
    public class TaxCode
    {
        [Key]

        public Guid Id { get; set; }
        public Guid? TaxGroupId { get; set; }
        public String? Name { get; set; }
        public String? Description { get; set; }
        public TaxDirection TaxDirection { get; set; }
        public bool IsDefault { get; set; }
        public bool isRecoverable { get; set; }
        public Decimal Rate { get; set; }

        public Guid? TaxAccountId { get; set; }
        [ForeignKey("TaxAccountId")]
        public virtual Account TaxAccount { get; set; }


        public Guid? OutputTaxAccountId { get; set; }
        [ForeignKey("OutputTaxAccountId")]
        public virtual Account OutputTaxAccount { get; set; }

        public Guid? InputTaxAccountId { get; set; }
        [ForeignKey("InputTaxAccountId")]
        public virtual Account InputTaxAccount { get; set; }


        public Guid? AssignAccountId { get; set; }
        [ForeignKey("AssignAccountId")]
        public virtual Account AssignAccount { get; set; }


        public Guid? CloseToAccountId { get; set; }
        [ForeignKey("CloseToAccountId")]
        public virtual Account CloseToAccount { get; set; }



        public void Refresh()
        {

        }


        //Closing Account
        public Guid? TaxReceivableAccountId { get; set; }

        public Guid? TaxPayableAccountId { get; set; }
        [ForeignKey("TaxPayableAccountId")]
        public virtual Account TaxPayableAccount { get; set; }

        public bool IsReady { get; set; }
        public int CommercialCount { get; set; }



        public String? Code
        { get; set; }

        public TaxCode()
        {

        }

        public void Update(TaxCode taxCode)
        {
            Name = taxCode.Name;
            Code = taxCode.Code;
            TaxDirection = taxCode.TaxDirection;

            Description = taxCode.Description;
            TaxGroupId = taxCode.TaxGroupId;
            Rate = taxCode.Rate;
            IsDefault = taxCode.IsDefault;
            isRecoverable = taxCode.isRecoverable;
            TaxAccountId = taxCode.TaxAccountId;

        }

        public Decimal GetTaxRate(DateTime Date)
        {
            return Rate;
        }

        public Decimal GetTaxBalance(DateTime Date, decimal amount)
        {
            var taxRate = GetTaxRate(Date);
            return decimal.Round(amount * taxRate / 100, 2, MidpointRounding.ToEven);
        }

    }
}
