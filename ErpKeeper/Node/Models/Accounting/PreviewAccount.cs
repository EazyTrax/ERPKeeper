﻿using ERPKeeper.Node.Models.Financial.Transfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeper.Node.Models.Accounting
{


    [Table("ERPchartOfAccountDal_PreviewAccount")]
    public class PreviewAccount
    {
        [Key]
        public Guid PreviewAccountGuid { get; set; }


        public Guid AccountGuid { get; set; }
        [ForeignKey("AccountGuid")]
        public virtual AccountItem Account { get; set; }


        [Column("OwnerProfileGuid")]
        public Guid OwnerProfileGuid { get; set; }
        [ForeignKey("OwnerProfileGuid")]
        public Models.Profiles.Profile Profile { get; set; }


        public PreviewAccount()
        {

        }


        public PreviewAccount(Guid AccountGuid, Guid profileUid)
        {
            this.PreviewAccountGuid = Guid.NewGuid();
            this.AccountGuid = AccountGuid;
            this.OwnerProfileGuid = profileUid;
        }


    }
}