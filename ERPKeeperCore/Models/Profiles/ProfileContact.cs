using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeperCore.Enterprise.Models.Profiles
{
    public class ProfileContact
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public String? Name { get; set; }
        public String? MobilePhone { get; set; }
        public String? OfficePhone { get; set; }
        public String? Email { get; set; }
        public String? Note { get; set; }
        public String? ShippingNote { get; set; }

        public ProfileContact()
        {

        }
    }
}