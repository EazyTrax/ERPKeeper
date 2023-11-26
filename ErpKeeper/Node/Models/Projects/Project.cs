using ERPKeeper.Node.Models.Projects.Enums;
using ERPKeeper.Node.Models.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Projects
{
    [Table("ERP_Projects")]
    public class Project
    {
        [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Uid { get; set; }

        public Guid? ParentGuid { get; set; }
        [ForeignKey("ParentGuid")]
        public virtual Project Parent { get; set; }

        public virtual ICollection<Project> Childs { get; set; }
        public virtual ICollection<Material> Materials { get; set; }

        public void ChangeStatus(ProjectStatus close)
        {
            this.Status = close;
        }

        public virtual ICollection<Transactions.Commercial> Commercials { get; set; }
        public virtual ICollection<Estimations.Quote> Quotes { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }

        [MaxLength(30)]
        public String Code { get; set; }
        public String Name { get; set; }
        public Enums.ProjectStatus Status { get; set; }

        [Column("TrnDate")]
        public DateTime CreatedDate { get; set; }
        public int Age => (int)((DateTime.Now - CreatedDate).TotalDays);
        public String Detail { get; set; }
        public Enums.ProjectType Type { get; set; }
        public decimal Value { get; set; }


        public Guid? CustomerProfileGuid { get; set; }
        [ForeignKey("CustomerProfileGuid")]
        public virtual Profiles.Profile Customer { get; set; }






        public virtual void Close()
        {
            this.Status = Enums.ProjectStatus.Close;
        }

        public virtual void Open()
        {
            this.Status = Enums.ProjectStatus.Active;
        }


    }
}
