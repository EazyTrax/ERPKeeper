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

namespace ERPKeeperCore.Web.Areas.API.Project
{
    public class SaleQuotesController : API_Project_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.SaleQuotes
                .Where(m => m.ProjectId == Id)
                .ToList();

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
