using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeeperCore.ERPNode.Models
{

    [Table("ERP_Profiles_BankAccount")]
    public class ProfileBankAccount
    {
        [Key]
        
        public Guid BankAccountId { get; set; }

        [Column("ProfileId")] 
        public Guid? ProfileId { get; set; }

        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }


        public String Name { get; set; }
        public String Number { get; set; }

        public String BankName { get; set; }
        public Guid? BankProfileId { get; set; }
        public void Update(ProfileBankAccount bankAccount)
        {
            this.Name = bankAccount.Name;
            this.BankName = bankAccount.BankName;
            this.Number = bankAccount.Number;
        }

        // [ForeignKey("BankProfileId")]
        // public virtual Profile BankProfile { get; set; }

        public ProfileBankAccount()
        {
        
        }
    }
}