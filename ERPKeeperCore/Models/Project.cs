using KeeperCore.ERPNode.Models.Enums;
using KeeperCore.ERPNode.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Project Parent { get; set; }



        [MaxLength(30)]
        public String Code { get; set; }

        [MaxLength(30)]
        public String Name { get; set; }
        public Enums.ProjectStatus Status { get; set; }


        public DateTime CreatedDate { get; set; }
        public int Age => (int)((DateTime.Now - CreatedDate).TotalDays);
        public String Detail { get; set; }
        public Enums.ProjectType Type { get; set; }
        public decimal Value { get; set; }

        public virtual ICollection<Project> Childs { get; set; }
        public virtual ICollection<Sale> Commercials { get; set; }

        public void ChangeStatus(ProjectStatus close)
        {
            this.Status = close;
        }

    }
}
