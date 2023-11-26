using ERPKeeper.Node.Models.Datum;
using ERPKeeper.Node.Models.Profiles;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Profiles.Controllers
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can implement base type constructors")]
    public class ProfileController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

        }



        public ActionResult New() => View();

        public ActionResult Documents(Guid id)
        {
            ViewBag.id = id;

            var profile = Organization.Profiles.Get(id);
            return View(profile);
        }

        public ActionResult History(Guid id) => View(Organization.Profiles.Get(id));
        public ActionResult Icon(Guid id) => View(Organization.Profiles.Get(id));

        [HttpPost]
        public ActionResult UploadIcon(Guid id)
        {
            var profile = Organization.Profiles.Get(id);
            var image = System.Web.Helpers.WebImage.GetImageFromRequest();

            if (image != null)
            {
                profile.Picture = image.GetBytes();
                Organization.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult GetImage(Guid id)
        {
            var profile = Organization.Profiles.Get(id);
            System.Web.Helpers.WebImage image = new System.Web.Helpers.WebImage(profile.Picture);
            return File(image.GetBytes(), "image/" + image.ImageFormat, image.FileName);
        }

        public PartialViewResult _HistorySummary(Guid id) => PartialView(Organization.Profiles.ListHistoryEvent(id));

        public ActionResult Home(Guid id)
        {
            ViewBag.id = id;
            var profile = Organization.Profiles.Get(id);

            if (profile != null)
                return View(profile);
            else
                return RedirectToAction("Home", "Profiles");

        }

        public ActionResult Addresses(Guid id)
        {
            var profile = Organization.Profiles.Get(id);
            if (profile != null)
                return View(profile);
            else
                return RedirectToAction("Home", "Profiles");
        }

        public ActionResult UpdateRd(Guid id)
        {
            var profile = Organization.Profiles.Get(id);
            Organization.Profiles.SyncWithRd(profile);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult UpdateRdAddresses(Guid id)
        {
            Organization.Profiles.UpdateRdAddresses(id);
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }




        public ActionResult BankAccounts(Guid id)
        {
            var profile = Organization.Profiles.Get(id);
            if (profile != null)
                return View(profile);
            else
                return RedirectToAction("Home", "Profiles");
        }


        public ActionResult Commercials(Guid id)
        {
            var profile = Organization.Profiles.Get(id);
            if (profile != null)
                return View(profile);
            else
                return RedirectToAction("Home", "Profiles");
        }





        public ActionResult Export(Guid id)
        {
            var profile = Organization.Profiles.GetQuery().Where(p => p.Uid == id).ToList();
            var Report = new Reports.LetterFront()
            {
                DataSource = profile,
                Name = id.ToString("D")
            };
            return View(Report);
        }

        public ActionResult Save(Profile model)
        {
            model = Organization.Profiles.Save(model);

            if (model.IsSelfOrganization)
            {
                Organization.DataItems.Set(DataItemKey.OrganizationName, model.Name);
                Organization.DataItems.Set(DataItemKey.TaxId, model.TaxNumber);
                Organization.SaveChanges();
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        public ActionResult Delete(Guid id)
        {
            var profile = Organization.Profiles.Get(id);
            Organization.Profiles.Remove(profile);
            return RedirectToAction("Home", "Profiles");
        }
        public ActionResult ChangePin(Guid id) => View(Organization.Profiles.Get(id));

        [HttpPost]
        public ActionResult ChangePin(Profile profile)
        {
            profile = Organization.Profiles.ChangePin(profile);
            return RedirectToAction("Home", new { id = profile.Uid });
        }
        public PartialViewResult _CallbackPanel() => PartialView("~/Views/Shared/_CallbackPanelPartial.cshtml");

        public ActionResult Tasks(Guid id) => View(Organization.Profiles.Find(id));
        public PartialViewResult PartialGridViewTasks(Guid id) => PartialView(Organization.Tasks.ListByResponsible(id));
    }
}




