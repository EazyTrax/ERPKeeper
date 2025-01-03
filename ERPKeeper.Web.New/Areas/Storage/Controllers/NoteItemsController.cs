using ERPKeeperCore.Enterprise.Models.Storage;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Storage.Controllers
{

    public class NoteItemsController : _Storage_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        // Controller Action Method (ASP.NET Core)
        [HttpPost]
        [Route("/{companyId}/Storage/NoteItems/Create")]
        public IActionResult Create([FromBody] NoteItem noteItem)
        {
            if (noteItem == null || string.IsNullOrEmpty(noteItem.Note))
            {
                return BadRequest("Invalid note data.");
            }

            noteItem.Id = Guid.NewGuid();
            noteItem.Date = DateTime.UtcNow;
            noteItem.MemberId = AuthorizeUserId;

            _dbContext.Add(noteItem);
            _dbContext.SaveChanges();

            return Ok(noteItem);
        }
    }
}
