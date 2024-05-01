using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Products
{

    public class ItemsController : API_Products_BaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .Items
                .Where(i=>i.ItemType != Enterprise.Models.Items.Enums.ItemTypes.Group);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
        [AllowAnonymous]
        public object Export(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .Items
                .Where(i => i.ItemType != Enterprise.Models.Items.Enums.ItemTypes.Group)
                .Include(b=>b.Brand);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Items.Item();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());


            Organization.ErpCOREDBContext.Items.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Items.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            try
            {
                var model = Organization.ErpCOREDBContext.Items.Find(key);
                Organization.ErpCOREDBContext.Items.Remove(model);
                Organization.ErpCOREDBContext.SaveChanges();

            }
            catch (Exception) { }

        }
    }
}
