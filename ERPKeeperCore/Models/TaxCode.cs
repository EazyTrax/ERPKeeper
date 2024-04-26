using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models
{
    public class TaxCode
    {
        [Key]
        
        public Guid Id { get; set; }
        public Guid? TaxGroupId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Enums.TaxDirection TaxDirection { get; set; }
        public bool isDefault { get; set; }
        public bool isRecoverable { get; set; }

        public Decimal TaxRate { get; set; }

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

      

        public String Code
        { get; set; }

        public TaxCode()
        {
           
        }

        public void Update(TaxCode taxCode)
        {
            this.Name = taxCode.Name;
            this.Code = taxCode.Code;
            this.TaxDirection = taxCode.TaxDirection;

            this.Description = taxCode.Description;
            this.TaxGroupId = taxCode.TaxGroupId;
            this.TaxRate = taxCode.TaxRate;
            this.isDefault = taxCode.isDefault;
            this.isRecoverable = taxCode.isRecoverable;
            this.TaxAccountId = taxCode.TaxAccountId;
            
        }

        public Decimal GetTaxRate(DateTime Date)
        {
            return TaxRate;
        }

        public Decimal GetTaxBalance(DateTime Date, Decimal amount)
        {
            var taxRate = this.GetTaxRate(Date);
            return decimal.Round(amount * taxRate / (decimal)100, 2, MidpointRounding.ToEven);
        }

    }
}
