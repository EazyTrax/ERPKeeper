using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Profiles.Profile
{
    public class RolesController : _ProfileBaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.ProfileRoles
                .Where(r => r.ProfileId == ProfileId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var profile = Organization.Profiles.Find(ProfileId);
            var model = new ERPKeeperCore.Enterprise.Models.Profiles.ProfileRole();
            JsonConvert.PopulateObject(values, model);

            var existRole = Organization.ErpCOREDBContext
                .ProfileRoles
                .Where(r => r.ProfileId == ProfileId && r.Role == model.Role)
                .FirstOrDefault();

            if (existRole == null)
            {
                model.Id = Guid.NewGuid();
                model.Created = DateTime.Now;
                model.ProfileId = ProfileId;
                Organization.ErpCOREDBContext.ProfileRoles.Add(model);
                Organization.ErpCOREDBContext.SaveChanges();


                switch (model.Role)
                {
                    case Node.Models.Profiles.Role.Employee:
                        Organization.Employees.Create(profile);
                        break;
                    case Node.Models.Profiles.Role.Customer:
                        Organization.Customers.Create(profile);
                        break;
                    case Node.Models.Profiles.Role.Supplier:
                        Organization.Suppliers.Create(profile);
                        break;
                    case Node.Models.Profiles.Role.Investor:
                        Organization.Investors.Create(profile);
                        break;
                }
            }


            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.ProfileRoles.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.ProfileRoles.First(a => a.Id == key);
            Organization.ErpCOREDBContext.ProfileRoles.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
