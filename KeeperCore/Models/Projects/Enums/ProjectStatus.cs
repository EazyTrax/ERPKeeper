using System;
using System.Collections.Generic;
using System.Linq;

namespace KeeperCore.ERPNode.Models.Tasks.Enums
{
    public enum ProjectStatus
    {
        Active = 0,
        Close = 2,
        Pending = 1,
        Void = 100
    }

    public enum ProjectType
    {
        Normal = 0,
        Maintainance = 1,
        RunRate = 2
    }
}
