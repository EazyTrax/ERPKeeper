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
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Customers
{
    public class SaleQuotesController : API_Profiles_Customers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [Route("/API/{CompanyId}/Customers/{controller}/{action}/{status}")]
        public object List(SaleQuoteStatus status, DataSourceLoadOptions loadOptions)
        {
            if (status == SaleQuoteStatus.Open)
            {
                var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                 .Where(m => m.Status == SaleQuoteStatus.Open || m.Status == SaleQuoteStatus.Order)
                 .ToList();

                return DataSourceLoader.Load(returnModel, loadOptions);

            }
            else
            {
                var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                                 .Where(m => m.Status == status)
                                 .ToList();
                return DataSourceLoader.Load(returnModel, loadOptions);
            }
        }

        public object Quotes(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                .Where(m =>
                    m.Status == SaleQuoteStatus.Open ||
                    m.Status == SaleQuoteStatus.Open ||
                    m.Status == SaleQuoteStatus.Void ||
                    m.Status == SaleQuoteStatus.Delete
                )
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        public object Orders(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                .Where(m =>
                    m.Status == SaleQuoteStatus.Order ||
                    m.Status == SaleQuoteStatus.Close)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);
            var model = new ERPKeeperCore.Enterprise.Models.Customers.SaleQuote();
            JsonConvert.PopulateObject(values, model);

            enterpriseRepo.SaleQuotes.CreateDraft(model);
            enterpriseRepo.SaveChanges();

            return Ok();
        }



        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {



            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);

            var model = enterpriseRepo.ErpCOREDBContext.SaleQuotes.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);

            if (model.TaxCodeId == null)
                model.TaxCodeId = enterpriseRepo.TaxCodes.GetDefault(Enterprise.Models.Taxes.Enums.TaxDirection.Output).Id;


            model.UpdateBalance();

            enterpriseRepo.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


    }
}
