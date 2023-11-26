using Microsoft.AspNetCore.Mvc;

namespace www.Controllers
{

    public class HomeController : Controller
    {
        [Route("/")]
        [Route("/Home")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
