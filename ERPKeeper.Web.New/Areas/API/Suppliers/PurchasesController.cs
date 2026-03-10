using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Enterprise;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ERPKeeperCore.Enterprise.Models.Suppliers.Enums;

namespace ERPKeeperCore.Web.Areas.API.Profiles.Suppliers
{
    public class PurchasesController : API_Suppliers_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions, PurchaseStatus? status = null)
        {
            var query = Organization.ErpCOREDBContext.Purchases.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status.Value);
            }

            var returnModel = query.ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Insert(string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);

            var model = new ERPKeeperCore.Enterprise.Models.Suppliers.Purchase();


            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);

            enterpriseRepo.Purchases.Create(model);
            enterpriseRepo.SaveChanges();

            var supplier = enterpriseRepo.Suppliers.Find(model.SupplierId);

            if (string.IsNullOrEmpty(model.Reference))
                model.Reference = "N/A";
            if (model.TaxCodeId == null)
                model.TaxCodeId = supplier.DefaultTaxCodeUid;
            if (model.ExpenseAccountId == null)
                model.ExpenseAccountId = supplier.DefaultExpenseAccountId;

            enterpriseRepo.SaveChanges();

            if (supplier.DefaultProductItem != null)
                model.AddItem(supplier.DefaultProductItem);

            enterpriseRepo.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var enterpriseRepo = new EnterpriseRepo(CompanyId, true);
            var model = enterpriseRepo.ErpCOREDBContext.Purchases.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
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
