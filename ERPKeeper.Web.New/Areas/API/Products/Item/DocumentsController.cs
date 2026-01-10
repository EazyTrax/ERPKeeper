using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.New.API.Products.Item;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Products.Item
{
    [Route("/API/Products/Items/{ItemId:Guid}/{controller}/{action=Index}")]

    public class DocumentsController : API_Products_Item_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Documents
                .Where(r => r.TransactionId == ItemId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


    
        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Documents.First(a => a.Id == key);

            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.Documents.First(a => a.Id == key);
            Organization.ErpCOREDBContext.Documents.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
