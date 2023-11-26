using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using ERPKeeper.Web.New.API.Accounting;
using ERPKeeper.Web.New.API.Accounting.JournalEntry;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ERPKeeper.Web.New.API.Accounting.JournalEntry
{
    public class JournalEntryItemsController : JournalEntryBaseController
    {
        public object All(DataSourceLoadOptions loadOptions)
        {
            var returnModel = Organization.ErpNodeDBContext.JournalEntryItems
                .Where(m => m.JournalEntryId == JournalEntryId)
                .Include(m => m.Account);

            return DataSourceLoader.Load(returnModel, loadOptions);
        }

        [HttpPost]
        public IActionResult Insert(string values)
        {
            var model = new ERPKeeper.Node.Models.Accounting.JournalEntryItem();
            JsonConvert.PopulateObject(values, model);
            model.JournalEntryId = JournalEntryId;
            //if (!TryValidateModel(RequirementType))
            //    return BadRequest(ModelState.GetFullErrorMessage());

            Organization.ErpNodeDBContext.JournalEntryItems.Add(model);
            Organization.ErpNodeDBContext.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public IActionResult Update(Guid key, string values)
        {
            var model = Organization.ErpNodeDBContext.JournalEntryItems.First(a => a.JournalEntryItemId == key);
            JsonConvert.PopulateObject(values, model);
            Organization.ErpNodeDBContext.SaveChanges();

            var journalEntry = Organization.ErpNodeDBContext.JournalEntries.Find(JournalEntryId);
            journalEntry.UpdateBalance();
            Organization.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public void Delete(Guid key)
        {
            var model = Organization.ErpNodeDBContext.JournalEntryItems.First(a => a.JournalEntryItemId == key);
            Organization.ErpNodeDBContext.JournalEntryItems.Remove(model);
            Organization.ErpNodeDBContext.SaveChanges();
        }
    }
}
