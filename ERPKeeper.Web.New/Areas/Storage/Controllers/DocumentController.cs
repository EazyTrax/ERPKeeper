﻿using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Storage.Controllers
{

    [Route("/{CompanyId}/{area}/Documents/{DocumentId:Guid}/{action}")]
    public class DocumentController : _Storage_BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Download(Guid DocumentId)
        {
            var file = Organization.ErpCOREDBContext.Documents.Find(DocumentId);

            if (file != null)
                return File(file.GetOriginalFile(CompanyId), "application/octet-stream", file.FileName);

            return NotFound();
        }
    }
}