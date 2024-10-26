using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/Suppliers/Purchases/{Id:Guid}/{action=index}")]
    public class PurchaseController : _Suppliers_Base_Controller
    {
        public Guid Id => Guid.Parse(RouteData.Values["Id"].ToString());

        public IActionResult Index()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }
        public IActionResult UnPost()
        {
            var model =   Organization.Purchases.Find(Id);
            Organization.Purchases.UnPost(model);
            Organization.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Items()
        {
            var transcation = Organization.Purchases.Find(Id);
            transcation.UpdateItems();

            Organization.SaveChanges();
            return View(transcation);
        }

        [Route("/{CompanyId}/Suppliers/Purchases/{Id:Guid}/Items/Avaliable")]
        public IActionResult Items_Avaliable()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }
        [Route("/{CompanyId}/Suppliers/Purchases/{Id:Guid}/Items/Add")]
        public IActionResult Items_Add([FromQuery] Guid ItemId)
        {
            var transcation = Organization.Purchases.Find(Id);
            var item = Organization.Items.Find(ItemId);
            transcation.AddItem(item);
            Organization.SaveChanges();

            return Redirect($"/{CompanyId}/Suppliers/Purchases/{Id}/Items");
        }

        public IActionResult Payment()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }

        public IActionResult Shipments()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }

        public IActionResult Documents()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }

        public IActionResult Export()
        {
            var transcation = Organization.Purchases.Find(Id);
            return View(transcation);
        }
    }
}
