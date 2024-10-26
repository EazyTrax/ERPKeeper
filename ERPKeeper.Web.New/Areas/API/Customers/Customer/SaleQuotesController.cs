using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Web.API.Profiles.Profile;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers.Customer
{
    public class SaleQuotesController : _API_Customers_Customer_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                .Where(s => s.Status == Enterprise.Models.Customers.Enums.SaleQuoteStatus.Draft)
                .Where(r => r.CustomerId == ProfileId)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);
            var model = new ERPKeeperCore.Enterprise.Models.Customers.SaleQuote();
            JsonConvert.PopulateObject(values, model);

            enterpriseRepo.SaleQuotes.CreateDraft(model, ProfileId);
            enterpriseRepo.SaveChanges();

            return Ok();
        }
       


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.SaleQuotes.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.SaleQuotes.First(a => a.Id == key);
            Organization.ErpCOREDBContext.SaleQuotes.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
