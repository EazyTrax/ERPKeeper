using Microsoft.AspNetCore.Mvc;

namespace www.Controllers
{

    public class FinancialController : Controller
    {
        [Route("/Financial")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
