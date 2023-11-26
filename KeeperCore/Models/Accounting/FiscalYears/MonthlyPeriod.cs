using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using KeeperCore.ERPNode.Models.Items;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Accounting.FiscalYears;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.Models.Accounting
{
    [Table("ERP_Monthly_Periods")]
    public class MonthlyPeriod
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate => this.StartDate.AddMonths(1).AddDays(-1);
        public Guid FiscalId { get; set; }
        [ForeignKey("FiscalId")]
        public virtual FiscalYear FiscalYear { get; set; }
        public string Name => $"{this.EndDate.Year.ToString()}-{this.EndDate.Month.ToString()}";
    }
}