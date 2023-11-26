using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ERPKeeper.Node.Models.Profiles;

namespace ERPKeeper.Node.Models.Security
{
    [Table("ERP_Security_Permissions")]

    public class Member
    {
        public Member() { }

        [Column("Uid")]
        public Guid Uid { get; set; }

        [Key]
        public Guid ProfileUid { get; set; }
        [ForeignKey("ProfileUid")]
        public virtual Profile Profile { get; set; }
        public virtual String Name => this.Profile.DisplayName;
        public Enums.AccessLevel AccessLevel { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
