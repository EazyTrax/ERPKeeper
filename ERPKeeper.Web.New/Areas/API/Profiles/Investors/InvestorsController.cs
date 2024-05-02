using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ERPKeeperCore.Web.Areas.API.Profiles.Investors
{
    public class InvestorsController : API_Profiles_Investors_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {

            var returnModel = Organization
                .ErpCOREDBContext
                .Investors
                .Include(x => x.Profile)
                .ToList();



            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new Enterprise.Models.Investors.Investor();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpCOREDBContext.Investors.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Investors.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Investors.Find(key);
            Organization.ErpCOREDBContext.Investors.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
