﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeper.Web.New.Areas.Products.Views.Shared.Components
{
    public class _Items_Groups : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
       
            return View();
        }
    }
}
