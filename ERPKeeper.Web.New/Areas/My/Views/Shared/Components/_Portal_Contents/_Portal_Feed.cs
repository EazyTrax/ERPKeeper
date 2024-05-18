using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Linq;


namespace ERPKeeperCore.Web.Areas.My.Views.Shared.Components
{
    public class _Portal_Contents : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            //int AuthorizeLearnerId = Guid.Parse(this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //AVM.Learner.Business.Contents businessContents = new AVM.Learner.Business.Contents(AuthorizeLearnerId);
            //businessContents.Sync();

            return View(@"/Areas/Portal/Views/Shared/Components/_Portal_Contents/Default.cshtml");
        }
    }
}
