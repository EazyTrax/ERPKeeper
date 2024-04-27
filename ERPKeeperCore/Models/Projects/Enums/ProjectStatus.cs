using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models.Projects.Enums
{
    public enum ProjectStatus
    {
        Open = 0,
        Close = 2,
        Draft = 1,
        Void = 100
    }

    public enum ProjectType
    {
        Normal = 0,
        Maintainance = 1,
        RunRate = 2
    }
}
