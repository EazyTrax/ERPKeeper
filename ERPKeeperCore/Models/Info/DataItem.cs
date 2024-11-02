using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ERPKeeperCore.Enterprise.Models.Enums;

namespace ERPKeeperCore.Enterprise.Models.Info
{
    public class Branch
    {
        [Key]

        public Guid Id { get; set; }
        public string? OrganizationName { get; set; }
        public string? TaxNumber { get; set; }
        public string? WebSite { get; set; }
        public int No { get; set; }
        public string? Number { get; set; }

        public string? Address { get; set; }
        public string? AddressLine2 { get; set; }

        public string? PhoneNumber { get; set; }
        public string? BranchName { get; set; }
    }
}