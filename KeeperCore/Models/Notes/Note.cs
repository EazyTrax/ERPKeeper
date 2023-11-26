using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Notes
{
    [Table("ERP_Notes")]
    public class Note
    {
        [Key]
        public Guid Id { get; set; }

        public String Content { get; set; }

        public DateTime CreatedDate { get; set; }


        public Guid CreateByProfileId { get; set; }
        [ForeignKey("CreateByProfileId")]
        public Profiles.Profile CreateByProfile { get; set; }



        public Guid AssignToProfileId { get; set; }
        [ForeignKey("AssignToProfileId")]
        public Profiles.Profile AssignToProfile { get; set; }

    }




}
