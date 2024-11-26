using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers
{
    public class PurchasesController : API_Suppliers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .Purchases
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);
            var model = new ERPKeeperCore.Enterprise.Models.Suppliers.Purchase();
            JsonConvert.PopulateObject(values, model);

            enterpriseRepo.Purchases.Creat(model);
            enterpriseRepo.SaveChanges();

            var supplier = enterpriseRepo.Suppliers.Find(model.SupplierId);

            if (string.IsNullOrEmpty(model.Reference))
                model.Reference = "N/A";
            if (model.TaxCodeId == null)
                model.TaxCodeId = supplier.DefaultTaxCodeUid;
            if (model.ExpenseAccountId == null)
                model.ExpenseAccountId = supplier.DefaultExpenseAccountId;

            enterpriseRepo.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);
            var model = enterpriseRepo.ErpCOREDBContext.Purchases.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model);
            model.UpdateBalance();

            enterpriseRepo.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        //[HttpPost]
        //public void Delete(Guid key)
        //{
        //    var model = Organization.erpNodeDBContext.Purchases.First(a => a.Id == key);
        //    Organization.erpNodeDBContext.Purchases.Remove(model);
        //    Organization.erpNodeDBContext.SaveChanges();
        //}
    }
}
