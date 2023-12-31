﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERPKeeper.Node.Models.Profiles
{
    [Table("ERP_Profiles")]

    public class Profile
    {
        [Key]
        public Guid Uid { get; set; }
        public ProfileType ProfileType { get; set; }
        public ProfileStatus Status { get; set; }

        [Column("IsSelfOrganization")]
        public bool IsSelfOrganization { get; set; }

        [Column("IsRDVerify")]
        public bool IsRDVerify { get; set; }
        public Security.Enums.AccessLevel AccessLevel { get; set; }
        public String TitleName { get; set; }
        public String Name { get; set; }
        public String ShotName { get; set; }
        public String DisplayName => $"{this.Name}".Replace("-", "").Trim();

        public String DisplayHeader =>
            this.DisplayName + Environment.NewLine
            + "สาขา " + this.PrimaryAddress?.Title + Environment.NewLine
            + "เลขประจำตัวผู้เสียภาษี " + this.TaxNumber + Environment.NewLine
            + this.PrimaryAddress?.AddressLine + Environment.NewLine
            + this.PhoneNumber;

        [DefaultValue(EnumLanguage.en)]
        public EnumLanguage localizedLanguage { get; set; }

        public byte[] Picture { get; set; }
        public String TaxNumber { get; set; }
        public String Detail { get; set; }
        public String DetailContact { get; set; }
        public String DetailLogin { get; set; }
        public String WebSite { get; set; }
        public String Email { get; set; }
        public String FaceBook { get; set; }
        public String PhoneNumber { get; set; }
        public String Note { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Pin { get; set; }

        public virtual ICollection<ProfileContact> ProfileContacts { get; set; }
        public virtual ICollection<ProfileAddress> Addresses { get; set; }
        public virtual ICollection<ProfileBankAccount> BankAccounts { get; set; }
        public virtual ICollection<HistoryItem> Histories { get; set; }
        public virtual ICollection<ProfileRole> ProfileRoles { get; set; }



        public virtual Equity.Investor Investor { get; set; }
        public virtual Employees.Employee Employee { get; set; }
        public virtual Customers.Customer Customer { get; set; }
        public virtual Suppliers.Supplier Supplier { get; set; }


        public ProfileAddress PrimaryAddress
        {
            get
            {
                if (Addresses == null)
                    return null;

                var primaryAddress = Addresses?.Where(a => a.IsPrimary).FirstOrDefault();

                if (primaryAddress != null)
                    return primaryAddress;

                var defaultBranch = Addresses?.Where(a => a.Number == "00000").FirstOrDefault();
                if (defaultBranch != null)
                    return defaultBranch;

                if (Addresses.Count > 0)
                    return Addresses.FirstOrDefault();

                return new ProfileAddress();
            }
        }


        public Profile()
        {
            this.Uid = Guid.NewGuid();
        }

        internal void Update(Profile profile)
        {
            this.Name = profile.Name ?? "NA";
            this.ShotName = profile.ShotName ?? "NA";
            this.TaxNumber = (profile.TaxNumber)?.ToLower();
            this.PhoneNumber = (profile.PhoneNumber)?.ToLower();
            this.Email = (profile.Email)?.ToLower();
            this.WebSite = (profile.WebSite)?.ToLower();
            this.FaceBook = (profile.FaceBook)?.ToLower();
            this.Pin = profile.Pin?.Trim();
            this.AccessLevel = profile.AccessLevel;

        }
    }
}
