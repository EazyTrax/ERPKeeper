using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeper.Web.New.Views.Components
{
    public class _App_Menu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
