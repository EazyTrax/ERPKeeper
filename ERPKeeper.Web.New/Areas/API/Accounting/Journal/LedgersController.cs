using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeper.Web.New.API.Accounting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Accounting.Journal
{
    public class LedgersController : JournalBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.Ledgers
                .Where(m => m.TransactionLedgerUid == JournalId)
                .Include(m=>m.accountItem)
                .Include(m=>m.TransactionLedger);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }
    }
}
