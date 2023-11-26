using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Taxes
{

    public class IncomeTaxesController : IncomeTaxes_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext
                .IncomeTaxes;

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeper.Node.Models.Taxes.IncomeTax();
            JsonConvert.PopulateObject(values, model);
       
            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());


            Organization.ErpNodeDBContext.IncomeTaxes.Add(model);
            Organization.ErpNodeDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpNodeDBContext.IncomeTaxes.Find(key);

            if (model.PostStatus == Node.Models.Accounting.Enums.LedgerPostStatus.Editable)
            {
                JsonConvert.PopulateObject(values, model);
                Organization.ErpNodeDBContext.SaveChanges();
                return Ok();
            }
            else
            {
                return Ok("Cannot Edit");
            } 

            
        }

    }
}
