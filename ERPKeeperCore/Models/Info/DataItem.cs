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
        public String OrganizationName { get; set; }

        public int No { get; set; }

        public String Address { get; set; }
        public String PhoneNumber { get; set; }
        public string BranchName { get; set; }
    }
}