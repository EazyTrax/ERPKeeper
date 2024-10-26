using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers
{
    public class SalesController : API_Profiles_Customers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.Sales
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);
            var model = new ERPKeeperCore.Enterprise.Models.Customers.Sale();
            JsonConvert.PopulateObject(values, model);

            enterpriseRepo.Sales.CreateDraft(model);
            enterpriseRepo.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.Sales.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

    }
}
