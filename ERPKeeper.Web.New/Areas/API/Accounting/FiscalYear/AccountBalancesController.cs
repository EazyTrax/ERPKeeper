using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeper.Web.New.API.Accounting;
using ERPKeeper.Web.New.API.Accounting.FiscalYear;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Accounting.FiscalYear
{
    public class AccountBalancesController : FiscalYearBaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.PeriodAccountsBalances
                .Where(m => m.FiscalYearUid == FiscalYearId)
                .Include(m => m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }






    }
}
