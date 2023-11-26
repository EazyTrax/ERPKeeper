using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ERPKeeper.Web.New.API.Profiles.Profile
{
    [Route("/API/Profiles/{ProfileId:Guid}/{controller}/{action=Index}")]
    public class _ProfileBaseController : APIBaseController
    {
 
        public Guid ProfileId => Guid.Parse(RouteData.Values["ProfileId"].ToString());


    }
}
