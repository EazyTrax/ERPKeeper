using Microsoft.EntityFrameworkCore;

namespace KeeperCore.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var erpDb = new KeeperCore.ERPNode.DBContext.ERPCoreDbContext("Test01");

            erpDb.Database.EnsureDeleted();
            erpDb.Database.EnsureCreated();

            Console.WriteLine("Completed!");
        }
    }
}