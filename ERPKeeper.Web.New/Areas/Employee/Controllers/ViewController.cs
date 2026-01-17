using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;


namespace ERPKeeperCore.Web.Areas.Employee.Controllers
{
    public class ViewController : _EmployeeBaseController
    {
        [Route("/{Area}/{Controller}/{mode}")]
        public IActionResult SetMode(string mode)
        {
            Response.Cookies.Delete("ViewMode");

            if (mode == "Grid")
                Response.Cookies.Append("ViewMode", "Grid", new CookieOptions { MaxAge = TimeSpan.FromDays(15) });
            if (mode == "Tile")
                Response.Cookies.Append("ViewMode", "Tile", new CookieOptions { MaxAge = TimeSpan.FromDays(15) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}