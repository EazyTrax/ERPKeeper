using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Profiles
{


    [Table("ERP_Profile_Roles")]
    public class ProfileRole
    {
        [Key]
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public DateTime? Created { get; set; }

        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profiles.Profile Profile { get; set; }

    }
}