using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.API.Accounting.FiscalYear;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;

namespace ERPKeeperCore.Web.API.Accounting.FiscalYear
{
    public class ProfitAndLostController : FiscalYearBaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.FiscalYearAccountBalances
                .Where(m => m.FiscalYearId == FiscalYearId)
                .Where(m => m.Account.Type == AccountTypes.Expense || m.Account.Type == AccountTypes.Income)
                .Include(m => m.Account)
                .ToList()
                .Select(m => new
                {
                    m.AccountId,
                    m.Account.Name,
                    m.Account.Code,
                    type = m.Account.Type.ToString(),
                    subType = m.Account.SubType.ToString(),
                    m.Debit,
                    m.Credit
                })
                .OrderBy(m => m.Code)
                .ToList();



            return DataSourceLoader.Load(returnModel, loadOptions);
        }



    }
}