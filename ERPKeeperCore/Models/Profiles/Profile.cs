using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERPKeeperCore.Enterprise.Models.Profiles
{

    public class Profile
    {
        [Key]
        public Guid Id { get; set; }
        public ProfileType ProfileType { get; set; }
        public ProfileStatus Status { get; set; }


        public bool IsSelfOrganization { get; set; } = false;
        public bool IsRDVerify { get; set; }
        public String? TitleName { get; set; }
        public String? Name { get; set; }

        public String? ShotName { get; set; }
        public String? DisplayName => $"{Name}".Replace("-", "").Trim();


        public String? TaxNumber { get; set; }
        public String? Detail { get; set; }
        public String? DetailContact { get; set; }
        public String? DetailLogin { get; set; }
        public String? WebSite { get; set; }
        public String? Email { get; set; }
        public String? FaceBook { get; set; }
        public String? PhoneNumber { get; set; }
        public String? Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public String? Pin { get; set; }

        public virtual ICollection<ProfileContact> Contacts { get; set; } = new List<ProfileContact>();
        public virtual ICollection<ProfileAddress> Addresses { get; set; } = new List<ProfileAddress>();
        public virtual ICollection<ProfileBankAccount> BankAccounts { get; set; } = new List<ProfileBankAccount>();
        public virtual ICollection<ProfileRole> ProfileRoles { get; set; } = new List<ProfileRole>();



        public virtual Customers.Customer Customer { get; set; }
        public virtual Suppliers.Supplier Supplier { get; set; }
        public virtual Employees.Employee Employee { get; set; }
        public virtual Investors.Investor Investor { get; set; }
        public string? Password { get;  set; }

        public Profile()
        {

        }

        internal void Update(Profile profile)
        {
            Name = profile.Name ?? "NA";
            ShotName = profile.ShotName ?? "NA";
            TaxNumber = profile.TaxNumber?.ToLower();
            PhoneNumber = profile.PhoneNumber?.ToLower();
            Email = profile.Email?.ToLower();
            WebSite = profile.WebSite?.ToLower();
            FaceBook = profile.FaceBook?.ToLower();
            Pin = profile.Pin?.Trim();

        }
    }
}
