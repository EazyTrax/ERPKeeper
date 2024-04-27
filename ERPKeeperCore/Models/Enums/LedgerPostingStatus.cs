using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.Enterprise.Models.Enums
{
    public enum LedgerPostStatus
    {
        Editable = 0,
        Supected = 1,
        Locked = 9,
        PreOpening = 10
    }
}
