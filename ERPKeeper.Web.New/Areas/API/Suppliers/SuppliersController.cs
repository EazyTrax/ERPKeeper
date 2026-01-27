using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers
{
    public class SuppliersController : API_Suppliers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {

            var returnModel = Organization
                .ErpCOREDBContext
                .Suppliers
                .Include(x => x.Profile)
                .ToList();



            return DataSourceLoader.Load(returnModel, loadOptions);
        }



        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Suppliers.Find(key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }






        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Suppliers.Find(key);
            Organization.ErpCOREDBContext.Suppliers.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
