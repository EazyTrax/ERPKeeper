using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.My.Views.Shared.Components
{
    public class _Portal_Brands_Summary : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View(@"/Area/Portal/Views/Shared/Components/_My_Brands_Summary/Default.cshtml");

        }
    }

}
