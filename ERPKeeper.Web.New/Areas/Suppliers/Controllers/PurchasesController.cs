using ERPKeeper.Web.New.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeper.Web.New.Areas.Suppliers.Controllers
{

    public class PurchasesController : Base_SuppliersController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
