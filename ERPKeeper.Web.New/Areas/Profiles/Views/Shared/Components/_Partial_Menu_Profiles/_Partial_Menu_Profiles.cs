using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeper.Web.New.Area.Profiles.Views.Shared.Components
{

    public class _Partial_Menu_Profiles : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(@"/Areas/Profiles/Views/Shared/Components/_Partial_Menu_Profiles/Default.cshtml");
        }
    }
}
