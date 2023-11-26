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
using ERPKeeper.Node.Models.Accounting;

namespace ERPKeeper.Web.New.API.Accounting.FiscalYear
{
    public class ProfitAndLostController : FiscalYearBaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.PeriodAccountsBalances
                .Where(m => m.FiscalYearUid == FiscalYearId)
                .Where(m => m.Account.Type == AccountTypes.Expense || m.Account.Type == AccountTypes.Income)
                .Include(m => m.Account)
                .ToList()
                .Select(m => new
                {
                    m.AccountGuid,
                    m.Account.Name,
                    m.Account.Code,
                    type = m.Account.Type.ToString(),
                    subType = m.Account.SubEnumType.ToString(),
                    m.Debit,
                    m.Credit
                })
                .OrderBy(m => m.Code)
                .ToList();



            return DataSourceLoader.Load(returnModel, loadOptions);
        }



    }
}