using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Accounting.Journal
{
    public class TransactionLedgersController : API_Accounting_Transaction_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext
                .TransactionLedgers
                .Where(m => m.TransactionId == TransactionId)
                .Include(m=>m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
