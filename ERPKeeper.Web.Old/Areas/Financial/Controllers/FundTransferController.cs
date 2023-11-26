using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("FundTransfer")]
    [Route("{id}/{action=Home}")]
    public class FundTransferController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id)
        {
            var _Transfer = Organization.FundTransfers.Find(id);
            return View(_Transfer);
        }
        public ActionResult Ledger(Guid id)
        {
            return RedirectToAction("Home", "Ledgers", new { Area = "Accounting", id = id });
        }

        public ActionResult Documents(Guid id)
        {
            var _Transfer = Organization.FundTransfers.Find(id);
            return View(_Transfer);
        }

   

        [HttpPost]
        public ActionResult Save(ERPKeeper.Node.Models.Financial.Transfer.FundTransfer _Transfer)
        {
            Organization.FundTransfers.Save(_Transfer);
            return RedirectToAction("Home", new { id = _Transfer.Uid });
        }


        public ActionResult UnPost(Guid id)
        {
            var Transfer = Organization.FundTransfers.Find(id);
            Organization.FundTransfers.UnPost(Transfer);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        public ActionResult Void(Guid id)
        {
            var fundTransfer = Organization.FundTransfers.Find(id);
            Organization.FundTransfers.UnPost(fundTransfer);
            fundTransfer.Status = ERPKeeper.Node.Models.Financial.Transfer.FundTransferStatus.Void;

            Organization.SaveChanges();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }


        public ActionResult Delete(Guid id)
        {
            var result = Organization.FundTransfers.Delete(id);

            if (result)
                return RedirectToAction("Home", "FundTransfers");
            else
                return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        public ActionResult Copy(Guid id)
        {
            var fundTransfer = Organization.FundTransfers.Find(id);
            var cloneFundTransfer = Organization.FundTransfers.Copy(fundTransfer, DateTime.Now);
            return RedirectToAction("Home", "FundTransfer", new { id = cloneFundTransfer.Uid });
        }


        public ActionResult Export(Guid id)
        {
            var transfers = Organization.FundTransfers.GetAll();

            var Report = new Reports.FundTransfer()
            {
                DataSource = transfers,
                Name = id.ToString("D")
            };

            Report.Parameters["CompanyName"].Value = Organization.DataItems.OrganizationName;

            return View(Report);
        }


    }
}