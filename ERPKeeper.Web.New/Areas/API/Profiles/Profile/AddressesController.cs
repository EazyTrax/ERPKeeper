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
    public class AddressesController : _ProfileBaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.ProfileAddresses
                .Where(r => r.ProfileGuid == ProfileId)
                 //.Select(p => new
                 //{
                 //    p.AddressGuid,
                 //    p.Number,
                 //    p.Title,
                 //    p.AddressLine,
                 //    p.PhoneNumber,
                 //})
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Profiles.ProfileAddress();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            model.ProfileGuid = ProfileId;
            Organization.ErpCOREDBContext.ProfileAddresses.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.ProfileAddresses.First(a => a.AddressGuid == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.ProfileAddresses.First(a => a.AddressGuid == key);
            Organization.ErpCOREDBContext.ProfileAddresses.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
