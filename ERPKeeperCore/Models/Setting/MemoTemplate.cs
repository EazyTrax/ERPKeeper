using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models.Setting
{
    public class MemoTemplate
    {
        [Key]
        public Guid Id { get; set; }
      
        public String Name { get; set; }

        public MemoTemplate()
        {

        }

    }
}