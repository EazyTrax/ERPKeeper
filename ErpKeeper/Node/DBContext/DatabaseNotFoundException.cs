using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.DBContext
{
    public class DatabaseNotFoundException : Exception
    {

        public DatabaseNotFoundException()
        {
        }

        public DatabaseNotFoundException(string message)
        : base(message)
        {
        }

        public DatabaseNotFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}