using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperCore.ERPNode.Helpers
{
    public static class Badge
    {
        public static string Info(string content) =>
            string.Format(" <span class=\"badge badge-info\"> {0}</span> ", content);
        public static string Info(int content) =>
            string.Format(" <span class=\"badge badge-info\"> {0}</span> ", content.ToString("N0"));


        public static string Success(string content) =>
             string.Format(" <span class=\"badge badge-success\"> {0}</span> ", content);
        public static string Success(int content) =>
            string.Format(" <span class=\"badge badge-success\"> {0}</span> ", content.ToString("N0"));


        public static string Dark(string content) =>
            string.Format(" <span class=\"badge badge-dark\"> {0}</span> ", content);
        public static string Dark(int content) => Dark(content.ToString("N0"));




        public static string Danger(string content) =>
            string.Format(" <span class=\"badge badge-danger\"> {0}</span> ", content);
        public static string Danger(int content) =>
            string.Format(" <span class=\"badge badge-danger\"> {0}</span> ", content.ToString("N0"));

        internal static string Primary(string content) =>
            string.Format(" <span class=\"badge badge-primary\"> {0}</span> ", content);
        public static string Primary(int content) =>
            string.Format(" <span class=\"badge badge-primary\"> {0}</span> ", content.ToString("N0"));

        public static string Warning(string content) =>
            string.Format(" <span class=\"badge badge-warning\"> {0}</span> ", content);
        public static string Warning(int content) =>
            string.Format(" <span class=\"badge badge-warning\"> {0}</span> ", content.ToString("N0"));


        public static string GetBadge(int content)
        {
            if (content > 60)
                return KeeperCore.ERPNode.Helpers.Badge.Danger(content);
            if (content > 30)
                return KeeperCore.ERPNode.Helpers.Badge.Warning(content);
            else 
                return KeeperCore.ERPNode.Helpers.Badge.Dark(content);
        }
    }
}
