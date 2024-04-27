using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ERPKeeperCore.Enterprise.Models.Customers;
using ERPKeeperCore.Enterprise.Models.Projects.Enums;

namespace ERPKeeperCore.Enterprise.Models.Projects
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Project Parent { get; set; }

        [MaxLength(30)]
        public String? Code { get; set; }

        [MaxLength(128)]
        public String? Name { get; set; }
        public String? Detail { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int Age => (int)(DateTime.Now - EndDate).TotalDays;

        public ProjectType Type { get; set; }

        public Decimal Value { get; set; }

        public virtual ICollection<Project> Childs { get; set; }
       

        public void ChangeStatus(ProjectStatus close)
        {
            Status = close;
        }

    }
}
