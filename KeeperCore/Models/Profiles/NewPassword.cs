using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperCore.ERPNode.Models.Profiles
{
    public class NewPassword
    {
        public Guid ProfileId { get; set; }
        public int pin { get; set; }
    }
}
