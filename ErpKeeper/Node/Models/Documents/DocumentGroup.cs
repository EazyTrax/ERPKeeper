using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Documents
{


    [Table("ERP_FileGroups")]
    public class DocumentGroup
    {
        [Key]
        public Guid Uid { get; set; }

        public int No { get; set; }

        public DateTime RecordTime { get; set; }


        public String Name { get; set; }



        public Guid? OwnerProfileGuid { get; set; }
        [ForeignKey("OwnerProfileGuid")]
        public virtual ERPKeeper.Node.Models.Profiles.Profile OwnerProfile { get; set; }


        public Guid? ParentGuid { get; set; }
        [ForeignKey("ParentGuid")]
        public virtual DocumentGroup Parent { get; set; }



        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<DocumentGroup> FileGroups { get; set; }


        public DocumentGroup()
        {
            Uid = Guid.NewGuid();
        }

    }
}