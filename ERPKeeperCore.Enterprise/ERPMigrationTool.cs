using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        private string enterpriseDB;
        public EnterpriseRepo newOrganization;
        public Organization oldOrganization;

        public ERPMigrationTool(string enterpriseDB)
        {
            this.enterpriseDB = enterpriseDB;

            Console.WriteLine($"###{enterpriseDB}########################################################");
            newOrganization = new ERPKeeperCore.Enterprise.EnterpriseRepo(enterpriseDB, true);
            newOrganization.ErpCOREDBContext.Database.Migrate();
            Console.WriteLine(newOrganization.ErpCOREDBContext.Transactions.Count());
            oldOrganization = new ERPKeeper.Node.DAL.Organization(enterpriseDB, true);
            Console.WriteLine("###########################################################");
        }


        public void Migrate()
        {
            this.Copy_Accounting();
            this.Copy_Profiles();
            this.Copy_Financial();
            this.Copy_Customers();
            this.Copy_Suppliers();
            this.Copy_Employees();
            this.Copy_Items();
            this.Copy_Taxes();
            this.Copy_Assets();
            this.Copy_Projects();
        }

      

      

      

        


        public void Copy_Projects()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.Projects.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Projects.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Projects.Project()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        StartDate = a.CreatedDate,
                        EndDate = a.CreatedDate.AddYears(1),
                    };
                    newOrganization.ErpCOREDBContext.Projects.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }


    }
}
