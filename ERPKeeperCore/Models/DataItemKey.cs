﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models
{

    public enum DataItemKey
    {
        SetupComplete = 1,
        DefaultAccountCreated = 2,
        DefaultSystemAccountAssign = 3,
        FirstDate = 1011,
        FirstOpeningDate = 100,
        TaxId = 110,
        OrganizationName = 120,
        OrganizationHeader = 121,
        Title = 130,
        WebSite = 140,
        DefaultAddress = 150,
        SalesCountDefault = 1000,
        SalesCountPartialPaid = 1002,
        SalesCountPrepare = 1004,
        SalesCountOverDue = 1006,
        SalesCountThisMonth = 1008,
        SalesCountLastMonth = 1010,
        ItemsCount = 1101,
        ItemInventoryCount = 1102,
        ItemServiceCount = 1103,
        ItemNonInventoryCount = 1104,
        ItemAssetCount = 1105,
        AssetCount = 1501,
        AssetActiveCount = 1502,
        AssetInActiveCount = 1503,
        EmployeesCount = 1801,
        EmployeesActiveCount = 1802,
        EmployeeInActiveCount = 1803,
    }
}