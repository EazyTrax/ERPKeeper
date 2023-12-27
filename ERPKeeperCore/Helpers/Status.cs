
using KeeperCore.ERPNode.Models.Accounting.Enums;
using System;

namespace KeeperCore.ERPNode.Helpers
{
    public static class Status
    {
        public static string StatusImage(object status)
        {
            if (status != null)
                return String.Format("<img src ='/Content/Icon/Status/{0}.png' /> ", status.ToString());
            else
                return " ";
        }


        public static string ItemTypeImageUrl(object status)
        {
            if (status != null)
                return String.Format("/Content/Icon/ItemTypes/{0}.png", status.ToString());
            else
                return " ";
        }

        public static string ItemTypeImage(object status)
        {
            if (status != null)
                return String.Format("<img src = '/Content/Icon/ItemTypes/{0}.png' height='15' />", status.ToString());
            else
                return " ";
        }



   
        public static string LedgerStatus(object isPostLedger)
        {
            if ((LedgerPostStatus)(isPostLedger ?? LedgerPostStatus.Editable) == LedgerPostStatus.Locked)
                return String.Format("<i class=\"fas fa-fingerprint text-danger\"></i>");
            else
                return String.Format("<i class=\"fas fa-fingerprint text-light\"></i>");
        }
    }
}