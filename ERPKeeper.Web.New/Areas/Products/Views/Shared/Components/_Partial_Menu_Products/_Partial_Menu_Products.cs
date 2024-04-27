using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeperCore.Web.Area.Products.Views.Shared.Components
{
    public class _Partial_Menu_Products : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(@"/Areas/Products/Views/Shared/Components/_Partial_Menu_Products/Default.cshtml");
        }
    }
}
