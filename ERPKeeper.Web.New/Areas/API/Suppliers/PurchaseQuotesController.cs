using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERPKeeperCore.Enterprise;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers
{
    public class PurchaseQuotesController : API_Suppliers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PurchaseQuotes
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);
            var model = new ERPKeeperCore.Enterprise.Models.Suppliers.PurchaseQuote();
            JsonConvert.PopulateObject(values, model);

            enterpriseRepo.PurchaseQuotes.CreateDraft(model);
            enterpriseRepo.SaveChanges();

            return Ok();
        }



        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.PurchaseQuotes.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        //[HttpPost]
        //public void Delete(Guid key)
        //{
        //    var model = Organization.erpNodeDBContext.PurchaseQuotes.First(a => a.Id == key);
        //    Organization.erpNodeDBContext.PurchaseQuotes.Remove(model);
        //    Organization.erpNodeDBContext.SaveChanges();
        //}
    }
}
