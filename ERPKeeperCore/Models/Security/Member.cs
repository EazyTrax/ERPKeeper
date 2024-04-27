using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ERPKeeperCore.Enterprise.Models.Security.Enums;


namespace ERPKeeperCore.Enterprise.Models.Security
{

    public class Member
    {
        [Key]
        public Guid Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String Email { get; set; }
        public virtual String Password { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
