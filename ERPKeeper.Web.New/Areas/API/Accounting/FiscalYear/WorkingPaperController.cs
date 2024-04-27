



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

namespace ERPKeeperCore.Web.API.Accounting.FiscalYear
{
    public class WorkingPaperController : FiscalYearBaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.PeriodAccountsBalances
                .Where(m => m.FiscalYearUid == FiscalYearId)
                .Include(m => m.Account)
                .ToList()
                .Select(m => new
                {
                    m.AccountGuid,
                    m.Account.Name,
                    m.Account.Code,
                    type = m.Account.Type.ToString(),
                    subType = m.Account.SubEnumType.ToString(),
                    m.OpeningDebit,
                    m.OpeningCredit,
                    m.Debit,
                    m.Credit,
                    m.ClosingDebit,
                    m.ClosingCredit,
                    m.ClosedDebit,
                    m.ClosedCredit



                })
                .OrderBy(m => m.Code)
                .ToList();



            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
