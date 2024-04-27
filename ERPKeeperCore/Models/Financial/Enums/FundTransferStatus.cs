using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace ERPKeeperCore.Enterprise.Models.Financial.Enums
{
    public enum FundTransferStatus
    {
        Open = 0,
        Void = 2
    }
}