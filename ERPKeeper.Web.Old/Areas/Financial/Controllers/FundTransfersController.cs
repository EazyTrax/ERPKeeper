using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    public class FundTransfersController : WebFrontEnd.Controllers._DefaultNodeController
    {


        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home() => View();

        public ActionResult New(Guid? transferFromAccountGuid)
        {
            var transfer = new ERPKeeper.Node.Models.Financial.Transfer.FundTransfer()
            {
                TrnDate = DateTime.Now,
                Uid = Guid.NewGuid(),
                Status = ERPKeeper.Node.Models.Financial.Transfer.FundTransferStatus.Open,
                WithDrawAccountGuid = transferFromAccountGuid
            };
            Organization.FundTransfers.Add(transfer);

            return RedirectToAction("Home", "FundTransfer", new {  id = transfer.Uid});
        }



        [ValidateInput(false)]
        public PartialViewResult PartialGridView()
        {
            return PartialView("PartialGridView", Organization.FundTransfers.GetAll());
        }


        public ActionResult ReOrder()
        {
            Organization.FundTransfers.ReOrder();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



    }
}