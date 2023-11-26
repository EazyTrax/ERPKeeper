namespace ERPKeeper.Node.DBContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    internal sealed class ConfigurationERPDbContext : DbMigrationsConfiguration<ERPKeeper.Node.DBContext.ERPDbContext>
    {
        public ConfigurationERPDbContext()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Node.DBContext.ERPDbContext context)
        {

        }
      

    }
}
