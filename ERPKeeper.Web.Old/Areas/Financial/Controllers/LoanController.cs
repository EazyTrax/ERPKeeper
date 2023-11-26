using ERPKeeper.Node.Models.Financial.Loans;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("Loan")]
    [Route("{id}/{action=Home}")]
    public class LoanController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.Loans.Find(id));
        public ActionResult Ledger(Guid id) => RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });

        public ActionResult New()
        {
            var transaction = Organization.Loans.Find(Guid.Empty);

            var loan = new Loan()
            {
                TrnDate = DateTime.Now,
                Uid = Guid.NewGuid(),
            };

            return View("Home", loan);
        }

        [HttpPost]
        public ActionResult Save(Loan loan)
        {
            Organization.Loans.Save(loan);

            return RedirectToAction("Home", new { id = loan.Uid });
        }




        public ActionResult Delete(Guid id)
        {
            Organization.Loans.Delete(id);
            return RedirectToAction("Home", "Loans");
        }

        public ActionResult PostLedger()
        {
            Organization.Loans.PostLedger();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult UnPost()
        {
            Organization.Loans.UnPost();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}