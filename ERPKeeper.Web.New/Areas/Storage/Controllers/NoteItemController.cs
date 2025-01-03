using ERPKeeperCore.Enterprise.Models.Storage;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Storage.Controllers
{

    [Route("/{CompanyId}/{area}/NoteItems/{NoteItemId:Guid}/{action}")]
    public class NoteItemController : _Storage_BaseController
    {
   

        public Guid ProjectId => Guid.Parse(RouteData.Values["ProjectId"].ToString());
        public Guid NoteItemId => Guid.Parse(RouteData.Values["NoteItemId"].ToString());

        public IActionResult Delete()
        {
            var projectNote = Organization.ErpCOREDBContext.NoteItems.Find(NoteItemId);

            if (projectNote != null)
            {
                Organization.ErpCOREDBContext.NoteItems.Remove(projectNote);
                Organization.SaveChanges();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Update([FromBody] NoteItem model)
        {
            if (model == null || model.Id == Guid.Empty)
                return BadRequest("Invalid data.");

            var projectNote = Organization.ErpCOREDBContext.NoteItems
                .FirstOrDefault(pn => pn.Id == model.Id);

            if (projectNote != null)
            {
                projectNote.Note = model.Note;
                projectNote.PreNote = projectNote.Note; // Optional: Save the previous note content
                Organization.ErpCOREDBContext.SaveChanges();

                return Ok();
            }

            return NotFound("Note not found.");
        }



    }
}