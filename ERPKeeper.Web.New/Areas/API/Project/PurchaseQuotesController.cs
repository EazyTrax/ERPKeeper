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

namespace ERPKeeperCore.Web.Areas.API.Project
{
    public class PurchaseQuotesController : API_Project_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PurchaseQuotes
                .Where(m=>m.ProjectId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
