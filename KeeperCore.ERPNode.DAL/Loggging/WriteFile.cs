using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.DAL.Loggging
{
    public static class FileLog
    {
        public static void WriteSQL(string data)
        {

            string path = string.Format(@"c:\Temp\{0}_SQLtrace.txt", DateTime.Now.Second);
            File.AppendAllText(path, data + Environment.NewLine);
        }
        public static void Write(string logType, string data)
        {

            string path = string.Format(@"c:\Temp\{0}_ERP_LOG" + logType + ".txt", DateTime.Now.Minute);

            try
            {
                File.AppendAllText(path, data + Environment.NewLine);
            }
            catch (Exception)
            {

            }

        }

        public static void WriteProfileSQL(string data)
        {

            string path = string.Format(@"c:\Temp\{0}_Profile_SQLtrace.txt", DateTime.Now.Minute);
            File.AppendAllText(path, data + Environment.NewLine);
        }
    }
}