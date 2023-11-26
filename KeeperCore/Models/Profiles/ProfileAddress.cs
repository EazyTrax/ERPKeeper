using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Profiles
{

    [Table("ERP_Profiles_Addresses")]
    public class ProfileAddress
    {
        [Key]
        [Column("Id")]
        public Guid AddressId { get; set; }

        [Column("ProfileId")]
        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profiles.Profile Profile { get; set; }


        [MaxLength(50)]
        public String Title { get; set; }
        public String Number { get; set; }



        public bool IsPrimary { get; set; }

        public String AddressLine { get; set; }

        [MaxLength(100)]
        public String PhoneNumber { get; set; }

        [MaxLength(100)]
        public String FaxNumber { get; set; }

        public DateTime? RecordDate { get; set; }
        public string Name { get; set; }

        public ProfileAddress()
        {
            
        }

        public void MakePrimary()
        {
            IsPrimary = true;
        }

        public void Update(ProfileAddress address)
        {
            this.Title = address.Title;
            this.Number = address.Number;


            this.IsPrimary = address.IsPrimary;
            this.AddressLine = address.AddressLine;
            this.PhoneNumber = address.PhoneNumber;
            this.FaxNumber = address.FaxNumber;

          
        }
    }
}