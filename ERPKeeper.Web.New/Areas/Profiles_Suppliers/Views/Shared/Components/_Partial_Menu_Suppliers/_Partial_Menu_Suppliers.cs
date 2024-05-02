using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeperCore.Web.Area.Suppliers.Views.Shared.Components
{

    public class _Partial_Menu_Suppliers : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(@"/Areas/Suppliers/Views/Shared/Components/_Partial_Menu_Suppliers/Default.cshtml");
        }
    }
}
