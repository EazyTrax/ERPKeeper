using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Customers.Customers.Customer
{
    public class EstimatesController : BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.SaleEstimates
                .Where(r => r.ProfileGuid == ProfileId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeper.Node.Models.Estimations.SaleQuote();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            model.ProfileGuid = ProfileId;
            Organization.ErpNodeDBContext.SaleEstimates.Add(model);
            Organization.ErpNodeDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpNodeDBContext.SaleEstimates.First(a => a.Uid == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpNodeDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpNodeDBContext.SaleEstimates.First(a => a.Uid == key);
            Organization.ErpNodeDBContext.SaleEstimates.Remove(model);
            Organization.ErpNodeDBContext.SaveChanges();
        }
    }
}
