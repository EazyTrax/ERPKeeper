using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.Areas.API.Profiles.HR;
using ERPKeeperCore.Web.Areas.API.HR.EmployeePaymentType;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.Areas.API.HR.EmployeePaymentType
{
    public class EmployeePaymentItemsController : _API_Employees_PaymentType_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .EmployeePaymentItems
                .Where(et => et.EmployeePaymentTypeId == Id)
                .Include(a => a.EmployeePaymentType)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.EmployeePaymentItems.First(a => a.Id == key);
            JsonConvert.PopulateObject(values, model, DefaultAPIJsonSerializerSettings);
            Organization.ErpCOREDBContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.EmployeePaymentItems
                .Include(x => x.EmployeePayment)
                .First(a => a.Id == key);

            if (model.EmployeePayment.IsPosted == false)
                Organization.ErpCOREDBContext.EmployeePaymentItems.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
