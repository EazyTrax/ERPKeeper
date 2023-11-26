using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Profiles.Profile
{
    public class AddressesController : _ProfileBaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.ProfileAddresses
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
            var model = new ERPKeeper.Node.Models.Profiles.ProfileAddress();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            model.ProfileGuid = ProfileId;
            Organization.ErpNodeDBContext.ProfileAddresses.Add(model);
            Organization.ErpNodeDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpNodeDBContext.ProfileAddresses.First(a => a.AddressGuid == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpNodeDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpNodeDBContext.ProfileAddresses.First(a => a.AddressGuid == key);
            Organization.ErpNodeDBContext.ProfileAddresses.Remove(model);
            Organization.ErpNodeDBContext.SaveChanges();
        }
    }
}
