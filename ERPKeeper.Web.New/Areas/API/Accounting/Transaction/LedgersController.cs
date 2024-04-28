using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Accounting.Journal
{
    public class LedgersController : API_Accounting_Transaction_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .TransactionLedgers
                .Where(m => m.TransactionId == TransactionId);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
