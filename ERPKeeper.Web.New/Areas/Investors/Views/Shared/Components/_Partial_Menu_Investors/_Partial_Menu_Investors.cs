using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeperCore.Web.Area.Investors.Views.Shared.Components
{

    public class _Partial_Menu_Investors : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(@"/Areas/Investors/Views/Shared/Components/_Partial_Menu_Investors/Default.cshtml");
        }
    }
}
