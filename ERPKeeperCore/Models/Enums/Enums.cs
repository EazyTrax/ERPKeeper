using System;
using System.Collections.Generic;
using System.Linq;

namespace ERPKeeperCore.Enterprise.Models
{
    public enum ViewPeriod
    {
        Week = 0,
        MonthToDate = 10,
        LastMonth = 20,
        YearToDate = 30,
        LastYear = 40,
        Fiscal = 50,
        All = 99
    }
    public enum ProfileQueryType
    {
        Active = 0,
        InActive = 1,
        All = 2

    }
    public enum Role
    {
        Customer = 1,
        Supplier = 2,
        Employee = 3,
        Investor = 4,
        Member = 5,
        Accountant = 6,
        HR = 7,

    }
    public enum EnumRoleStatus
    {
        Active = 0,
        InActive = 1
    }
    public enum EnumRelationType
    {
        None = 0,
        Employee = 1,
        Partner = 2,
        Owner = 3
    }

    public enum ProfileType
    {
        Organization = 0,
        People = 1
    }
    public enum ProfileStatus
    {
        Active = 0,
        Disable = 1
    }
    public enum ProfileViewType
    {
        Organization = 0,
        People = 1,
        Active = 11,
        InActive = 12
    }
    public enum EnumLanguage
    {
        en = 0,
        th = 1
    }
}
