﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.New.API.Financials.RetentionType;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.New.API.Financials.RetentionGroup
{
    public class SupplierPaymentsController : API_Financials_RetentionGroup_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization
                .ErpCOREDBContext.SupplierPayments
                .Where(m => m.RetentionGroupId == RetentionGroupId)
                .Include(a => a.RetentionType)
                .Include(a=>a.Purchase);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.SupplierPayments.Find(key);

            JsonConvert.PopulateObject(values, model);

            Organization.ErpCOREDBContext.SaveChanges();
            Organization.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.SupplierPayments.Find(key);
            model.RetentionGroupId = null;

            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}