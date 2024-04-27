using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeperCore.Web.Area.Employments.Views.Shared.Components
{

    public class _Partial_Menu_Employees : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(@"/Areas/Employees/Views/Shared/Components/_Partial_Menu_Employees/Default.cshtml");
        }
    }
}
