﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Taxes
{

    public class IncomeTaxesController : API_Taxes_BaseController
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .IncomeTaxes;

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Taxes.IncomeTax();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());


            Organization.ErpCOREDBContext.IncomeTaxes.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.IncomeTaxes.Find(key);

            if (!model.IsPosted)
            {
                JsonConvert.PopulateObject(values, model);
                Organization.ErpCOREDBContext.SaveChanges();
                return Ok();
            }
            else
            {
                return Ok("Cannot Edit");
            }


        }

    }
}
