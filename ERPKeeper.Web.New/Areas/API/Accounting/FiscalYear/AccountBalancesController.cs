﻿using System;
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
                .Include(m => m.Account)
                .ToList();

            returnModel = returnModel
                .Where(m => m.OpeningCredit != 0 || m.OpeningDebit != 0 || m.ClosedCredit != 0 || m.ClosedDebit != 0 || m.Credit != 0 || m.Debit != 0 || m.ClosingCredit != 0 || m.ClosingDebit != 0)
                .ToList();




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

                    CurrentDebit = line.TotalDebit,
                    CurrentCredit = line.TotalCredit,

                    ClosingDebit = line.ClosingDebit,
                    ClosingCredit = line.ClosingCredit,

                    ClosedDebit = line.ClosedDebit,
                    ClosedCrdit = line.ClosedCredit





                });

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
