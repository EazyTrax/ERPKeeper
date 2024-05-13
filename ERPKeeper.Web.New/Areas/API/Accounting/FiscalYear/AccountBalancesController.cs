using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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
    public class AccountBalancesController : FiscalYearBaseController
    {
        [AllowAnonymous]
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.FiscalYearAccountBalances
                .Where(m => m.FiscalYearId == FiscalYearId)
                .Include(m => m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [AllowAnonymous]
        public object Export(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.FiscalYearAccountBalances
                .Where(m => m.FiscalYearId == FiscalYearId)
                .Include(m => m.Account)
                .Select(line => new
                {
                    AccName = line.Account.Name,
                    AccCode = line.Account.Code,

                    AccType = line.Account.Type.ToString(),
                    AccSubType = line.Account.SubType.ToString(),

                    OpeningDebit = line.OpeningDebit,
                    OpeningCredit = line.OpeningCredit,

                    CurrentDebit = line.Debit,
                    CurrentCredit = line.Credit,

                    ClosingDebit = line.ClosingDebit,
                    ClosingCredit = line.ClosingCredit,

                    ClosedDebit = line.ClosedDebit,
                    ClosedCrdit = line.ClosedCredit





                });

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
