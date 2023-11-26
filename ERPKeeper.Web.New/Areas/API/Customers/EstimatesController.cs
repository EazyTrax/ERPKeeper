using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeper.Node.Models.Estimations.Enums;
using ERPKeeper.Node.Models.Accounting.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Customers
{
    public class EstimatesController : Base_CustomersController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.Estimates
                .Where(x => x.TransactionType == ERPObjectType.SaleQuote)
                .Where(x => !(x.Status == QuoteStatus.Ordered || x.Status == QuoteStatus.Close))
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

        //[HttpPost]
        //public void Delete(Guid key)
        //{
        //    var model = Organization.erpNodeDBContext.SaleQuotes.First(a => a.Uid == key);
        //    Organization.erpNodeDBContext.SaleQuotes.Remove(model);
        //    Organization.erpNodeDBContext.SaveChanges();
        //}
    }
}
