using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeperCore.Web.API.Accounting;
using ERPKeeperCore.Web.API.Accounting.JournalEntry;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeperCore.Web.API.Accounting.JournalEntry
{
    public class ItemsController : JournalEntryBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpCOREDBContext.JournalEntryItems
                .Where(m => m.JournalEntryId == JournalEntryId)
                .Include(m => m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeperCore.Enterprise.Models.Accounting.JournalEntryItem();
            JsonConvert.PopulateObject(values, model);
            model.JournalEntryId = JournalEntryId;
            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpCOREDBContext.JournalEntryItems.Add(model);
            Organization.ErpCOREDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpCOREDBContext.JournalEntryItems.Find(key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpCOREDBContext.SaveChanges();

            var journalEntry = Organization.ErpCOREDBContext.JournalEntries.Find(JournalEntryId);
            journalEntry.UpdateBalance();
            Organization.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpCOREDBContext.JournalEntryItems.Find(key);
            Organization.ErpCOREDBContext.JournalEntryItems.Remove(model);
            Organization.ErpCOREDBContext.SaveChanges();
        }
    }
}
