using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);


            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);


            model.TaxCodeId = enterpriseRepo.TaxCodes.GetDefault(Enterprise.Models.Taxes.Enums.TaxDirection.Output).Id;
            model.IncomeAccountId = enterpriseRepo.SystemAccounts.Income.Id;

            enterpriseRepo.Sales.CreateNew(model);
            enterpriseRepo.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
           
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);

            var model = enterpriseRepo.ErpCOREDBContext.Sales.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            if (model.TaxCodeId == null)
                model.TaxCodeId = enterpriseRepo.TaxCodes.GetDefault(Enterprise.Models.Taxes.Enums.TaxDirection.Output).Id;

            if (model.IncomeAccountId == null)
                model.IncomeAccountId = enterpriseRepo.SystemAccounts.Income.Id;

            model.UpdateBalance();

            enterpriseRepo.ErpCOREDBContext.SaveChanges();

            return Ok();
        }
    }
}
