using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Tasks
{
    public class NewTask
    {
        public Guid ProfileId { get; set; }
        public String Title { get; set; }
        public Guid ResponsibleUserId { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime? CreatedDate { get;  set; }
    }
}