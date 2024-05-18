
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.My.Views.Shared.Components
{
    public class _Portal_Header : ViewComponent
    {
        public IViewComponentResult Invoke(bool showImage = true)
        {
            Guid AuthorizeLearnerId = Guid.Parse(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString());

            return View("/Areas/Portal/Views/Shared/Components/_Portal_Header/Default.cshtml");
        }
    }

}
