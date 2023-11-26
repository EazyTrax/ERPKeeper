using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Documents
{
    [Table("ERP_FileGroups")]
    public class DocumentGroup
    {
        [Key]
        public Guid Id { get; set; }
        public int No { get; set; }
        public DateTime RecordTime { get; set; }
        public String Name { get; set; }
        public Guid? OwnerProfileId { get; set; }
        [ForeignKey("OwnerProfileId")]
        public virtual KeeperCore.ERPNode.Models.Profiles.Profile OwnerProfile { get; set; }


        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual DocumentGroup Parent { get; set; }



        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentGroup> FileGroups { get; set; }


        public DocumentGroup()
        {
            
        }

    }
}