using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



namespace ERPKeeperCore.Enterprise.Models.Accounting.Enums
{

    public enum FiscalYearStatus
    {
        Open = 0,
        Close = 1
    }
}