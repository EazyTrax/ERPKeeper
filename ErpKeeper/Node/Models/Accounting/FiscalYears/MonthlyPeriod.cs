using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ERPKeeper.Node.Models.Items;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.FiscalYears;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.Models.Accounting
{
    [Table("ERP_Monthly_Periods")]
    public class MonthlyPeriod
    {
        [Key]
        public Guid Uid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => this.StartDate.AddMonths(1).AddDays(-1);
        public Guid FiscalUid { get; set; }
        [ForeignKey("FiscalUid")]
        public virtual FiscalYear FiscalYear { get; set; }
        public string Name => $"{this.EndDate.Year.ToString()}-{this.EndDate.Month.ToString()}";
    }
}