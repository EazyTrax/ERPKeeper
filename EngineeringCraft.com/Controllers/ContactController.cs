using Microsoft.AspNetCore.Mvc;

namespace www.Controllers
{

    public class ContactController : Controller
    {
        [Route("/Contact")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
