using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.API.Accounting.FiscalYear;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Accounting.FiscalYear
{
    public class ExpensesController : FiscalYearBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PeriodAccountsBalances
                .Where(m => m.FiscalYearId == FiscalYearId && m.Account.Type == Node.Models.Accounting.AccountTypes.Expense)
                .Include(m => m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
