using ERPKeeper.WebFrontEnd.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ERPKeeper.WebFrontEnd.Areas.Menu.Controllers
{
    public class DefaultController : _DefaultNodeController
    {

        //[OutputCache(Duration = 60)]
        public PartialViewResult _Main()
        {
            return PartialView();
        }
    }
}