using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Projects
{
    public class NewTask
    {
        public Guid ProfileGuid { get; set; }
        public String Title { get; set; }
        public Guid ResponsibleUid { get; set; }
        public Guid CreatorUid { get; set; }
        public DateTime? CreatedDate { get;  set; }
    }
}