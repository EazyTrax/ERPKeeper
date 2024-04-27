using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeperCore.Web.Area.Customers.Views.Shared.Components
{

    public class _Partial_Menu_Customers : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(@"/Areas/Customers/Views/Shared/Components/_Partial_Menu_Customers/Default.cshtml");
        }
    }
}
