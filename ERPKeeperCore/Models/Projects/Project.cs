using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Projects.Enums;
using ERPKeeperCore.Enterprise.Models.Security;
using ERPKeeperCore.Enterprise.Models.Suppliers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERPKeeperCore.Enterprise.Models.Projects
{
    public class ProjectNote
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public string? PreNote { get; set; }


        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }


        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

    }
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Project Parent { get; set; }

        [MaxLength(30)]
        public string? Code { get; set; }

        [MaxLength(128)]
        public string? Name { get; set; }
        public string? Detail { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int AgeInDays
        {
            get
            {
                var endDate = EndDate ?? DateTime.Today;
                return (endDate - StartDate).Days;
            }
        }


        public ProjectType Type { get; set; }

        public Decimal Value { get; set; }

        public virtual ICollection<Project> Childs { get; set; }


        public virtual ICollection<SaleQuote> SaleQuotes { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<PurchaseQuote> PurchaseQuotes { get; set; }

        public virtual ICollection<ProjectNote> ProjectNotes { get; set; }


        public decimal SaleQuotesTotal { get; set; }
        public decimal PurchaseQuotesTotal { get; set; }
        public decimal EstimateProfit => SaleQuotesTotal - PurchaseQuotesTotal;



        public decimal SalesTotal { get; set; }
        public decimal PurchasesTotal { get; set; }
        public decimal Profit => SalesTotal - PurchasesTotal;


        public void ChangeStatus(ProjectStatus close)
        {
            Status = close;

        }

        public void UpdateBalance()
        {
            SaleQuotesTotal = SaleQuotes.Sum(x => x.Total);
            SalesTotal = Sales.Sum(x => x.Total);
            PurchaseQuotesTotal = PurchaseQuotes.Sum(x => x.Total);
            PurchasesTotal = Purchases.Sum(x => x.Total);

        }

    }
}
