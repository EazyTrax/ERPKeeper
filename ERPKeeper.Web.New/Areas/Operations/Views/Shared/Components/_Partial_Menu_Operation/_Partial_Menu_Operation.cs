using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

namespace ERPKeeper.Web.New.Area.Operations.Views.Shared.Components
{

    public class _Partial_Menu_Operation : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(@"/Areas/Operations/Views/Shared/Components/_Partial_Menu_Operation/Default.cshtml");
        }
    }
}
