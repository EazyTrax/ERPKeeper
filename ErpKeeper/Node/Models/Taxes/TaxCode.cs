using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Taxes
{
    [Table("ERP_Taxes_TaxCode")]
    public class TaxCode
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }
        public Guid? TaxGroupUid { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Enums.TaxDirection TaxDirection { get; set; }
        public bool isDefault
        { get; set; }
        public bool isRecoverable { get; set; }

        [Column("TaxRate")]
        public Decimal TaxRate { get; set; }



        public Guid? TaxAccountGuid { get; set; }
        [ForeignKey("TaxAccountGuid")]
        public virtual Accounting.AccountItem TaxAccount { get; set; }

   
        public Guid? OutputTaxAccountGuid { get; set; }
        public Guid? InputTaxAccountGuid { get; set; }
        public Guid? AssignAccountGuid { get; set; }
        public Guid? CloseToAccountGuid { get; set; }

        public virtual ICollection<Transactions.Commercial> Commercials { get; set; }

        public void Refresh()
        {
            this.CommercialCount = this.Commercials?.Count() ?? 0;
        }
        //Closing Account
        public Guid? TaxReceivableAccountGuid { get; set; }

        public Guid? TaxPayableAccountGuid { get; set; }
        [ForeignKey("TaxPayableAccountGuid")]
        public virtual Accounting.AccountItem TaxPayableAccount { get; set; }

        public bool IsReady { get; set; }
        public int CommercialCount { get; set; }

      

        public String Code
        { get; set; }

        public TaxCode()
        {
            this.Uid = Guid.NewGuid();
        }

        public void Update(TaxCode taxCode)
        {
            this.Name = taxCode.Name;
            this.Code = taxCode.Code;
            this.TaxDirection = taxCode.TaxDirection;

            this.Description = taxCode.Description;
            this.TaxGroupUid = taxCode.TaxGroupUid;
            this.TaxRate = taxCode.TaxRate;
            this.isDefault = taxCode.isDefault;
            this.isRecoverable = taxCode.isRecoverable;
            this.TaxAccountGuid = taxCode.TaxAccountGuid;
            
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
