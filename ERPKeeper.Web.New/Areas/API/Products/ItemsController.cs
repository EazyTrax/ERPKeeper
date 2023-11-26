using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Products
{

    public class ItemsController : ProductsBaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext
                .Items
                .Where(i=>i.ItemType != Node.Models.Items.Enums.ItemTypes.Group);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
        [AllowAnonymous]
        public object Export(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext
                .Items
                .Where(i => i.ItemType != Node.Models.Items.Enums.ItemTypes.Group)
                .Include(b=>b.Brand);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeper.Node.Models.Items.Item();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());


            Organization.ErpNodeDBContext.Items.Add(model);
            Organization.ErpNodeDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpNodeDBContext.Items.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpNodeDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            try
            {
                var model = Organization.ErpNodeDBContext.Items.Find(key);
                Organization.ErpNodeDBContext.Items.Remove(model);
                Organization.ErpNodeDBContext.SaveChanges();

            }
            catch (Exception) { }

        }
    }
}
