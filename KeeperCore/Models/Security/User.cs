using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using KeeperCore.ERPNode.Models.Profiles;

namespace KeeperCore.ERPNode.Models.Security
{
    [Table("ERP_Security_Permissions")]

    public class User
    {
        public User() { }

        [Column("Id")]
        public Guid Id { get; set; }
        public virtual String Name { get; set; }
        public Enums.AccessLevel AccessLevel { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
