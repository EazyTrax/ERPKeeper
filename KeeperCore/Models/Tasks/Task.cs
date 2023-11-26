using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using KeeperCore.ERPNode.Models.Security;
using KeeperCore.ERPNode.Models.Tasks.Enums;

namespace KeeperCore.ERPNode.Models.Tasks
{
    [Table("ERP_Tasks")]
    public class WorkTask
    {

        [Key]
        public Guid Id { get; set; }
        public int No { get; set; }
        public String Name => $"TSK-{No}";
        public Enums.WorkTaskStatus Status { get; set; }
        public String Title { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CloseDate { get; set; }


        public Guid? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Tasks.Project Project { get; set; }


        public int Age
        {
            get
            {
                if (DueDate != null)
                    return (int)(((DateTime)DueDate).Date - DateTime.Today.Date).TotalDays;
                else
                    return 0;
            }
        }

        public String Detail { get; set; }
        public void SetStatus(WorkTaskStatus status) => this.Status = status;

        public Guid? ResponsibleUserId { get; set; }
        [ForeignKey("ResponsibleUserId")]
        public virtual User ResponsibleMember { get; set; }



        public void ChangeStatus(WorkTaskStatus status)
        {
            this.Status = status;
        }

    }
}
