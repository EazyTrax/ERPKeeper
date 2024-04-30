using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.New.API.Financials.FundTransfer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.New.API.Financials.FundTransfer
{
    public class ItemsController : API_Financials_FundTransfer_BaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.FundTransferItems
                .Where(m => m.FundTransferId == FundTransferId)
                .Include(m => m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Financial.FundTransferItem();
            JsonConvert.PopulateObject(values, model);
            model.FundTransferId = FundTransferId;

            Organization.ErpCOREDBContext.FundTransferItems.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.FundTransferItems.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();

            var journalEntry = Organization.ErpCOREDBContext.JournalEntries.Find(FundTransferId);
            journalEntry.UpdateBalance();
            Organization.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.FundTransferItems.Find(key);
            Organization.ErpCOREDBContext.FundTransferItems.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
