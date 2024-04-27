using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Suppliers.Supplyer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Suppliers.Suppliers.Supplyer
{
    public class PurchasesController : Base_API_Suppliers_Supplier_Controller
    {

        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Purchases
                .Where(r => r.SupplierId == ProfileId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Suppliers.Purchase();
            JsonConvert.PopulateObject(values, model);

            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            model.SupplierId = ProfileId;
            Organization.ErpCOREDBContext.Purchases.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Purchases.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Purchases.First(a => a.Id == key);
            Organization.ErpCOREDBContext.Purchases.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
