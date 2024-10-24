using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        public void Copy_Profiles()
        {
            Console.WriteLine("> Copy_Profiles");
            Copy_Profiles_Profiles();
            Copy_Profiles_ProfileAddresss();
             
        }

        private void Copy_Profiles_Profiles()
        {
            Console.WriteLine("> Copy_Profiles_Profiles");
            var existModelIds = newOrganization.ErpCOREDBContext.Profiles
             .Select(x => x.Id)
             .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.Profiles
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.Profiles.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Profiles.Profile()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Detail = a.Detail,
                        TaxNumber = a.TaxNumber,
                        Note = a.Note,

                    };
                    newOrganization.ErpCOREDBContext.Profiles.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }


            });
        }

        private void Copy_Profiles_ProfileAddresss()
        { 
                Console.WriteLine("> Copy_Profiles_ProfileAddresss");
            var existModelIds = newOrganization.ErpCOREDBContext.ProfileAddresses
             .Select(x => x.Id)
             .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.ProfileAddresses
                .Where(i => !existModelIds.Contains(i.AddressGuid))
                .ToList();


            oldModels.ForEach(a =>
            {
                Console.WriteLine($"ProfileAddresss:{a.AddressGuid}-{a.Number}");

                var exist = newOrganization.ErpCOREDBContext.ProfileAddresses.FirstOrDefault(x => x.Id == a.AddressGuid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Profiles.ProfileAddress()
                    {
                        Id = a.AddressGuid,
                        IsPrimary = a.IsPrimary,
                        Number = a.Number,
                        AddressLine = a.AddressLine,
                        FaxNumber = a.FaxNumber,
                        ProfileId = a.ProfileGuid,
                        PhoneNumber = a.PhoneNumber,
                        RecordDate = a.RecordDate,
                        Title = a.Title,
                    };

                    newOrganization.ErpCOREDBContext.ProfileAddresses.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }



    }
}
