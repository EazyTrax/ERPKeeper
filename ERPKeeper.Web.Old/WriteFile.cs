using System;
using System.IO;
using System.Linq;

namespace ERPKeeper.WebFrontEnd
{
    public static class WriteFile
    {
        public static void WriteSQL(string data)
        {

            string path = string.Format(@"c:\Temp\{0}_SQLtrace.txt", DateTime.Now.Second);
            //   File.AppendAllText(path, data);
        }

        public static void WriteProfile(string data)
        {

            string path = string.Format(@"c:\Temp\{0}_Profile.txt", DateTime.Now.Minute);
            File.AppendAllText(path, data + Environment.NewLine);
        }
    }
}