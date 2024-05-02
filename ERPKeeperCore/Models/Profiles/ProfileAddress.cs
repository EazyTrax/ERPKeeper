using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models.Profiles
{

    public class ProfileAddress
    {
        [Key]

        public Guid Id { get; set; }

        [Column("ProfileId")]
        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }


        [MaxLength(50)]
        public String? Title { get; set; }
        public String? Number { get; set; }



        public bool IsPrimary { get; set; }

        public String? AddressLine { get; set; }

        [MaxLength(100)]
        public String? PhoneNumber { get; set; }

        [MaxLength(100)]
        public String? FaxNumber { get; set; }

        public DateTime? RecordDate { get; set; }
        public String? Name { get; set; }

        public ProfileAddress()
        {

        }

        public void MakePrimary()
        {
            IsPrimary = true;
        }

        public void Update(ProfileAddress address)
        {
            Title = address.Title;
            Number = address.Number;


            IsPrimary = address.IsPrimary;
            AddressLine = address.AddressLine;
            PhoneNumber = address.PhoneNumber;
            FaxNumber = address.FaxNumber;


        }
    }
}