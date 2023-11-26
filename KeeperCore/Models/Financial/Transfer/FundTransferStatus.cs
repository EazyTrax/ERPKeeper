using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using KeeperCore.ERPNode.Models.ChartOfAccount;

namespace KeeperCore.ERPNode.Models.Financial.Transfer
{
    public enum FundTransferStatus
    {
        Open = 0,
        Void = 2
    }
}