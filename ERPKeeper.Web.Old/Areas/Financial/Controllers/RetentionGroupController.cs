using ERPKeeper.Node.Models.Financial.Payments;
using ERPKeeper.Node.Models.Financial.Payments.Enums;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERPKeeper.WebFrontEnd.Areas.Financial.Controllers
{
    [RouteArea("Financial")]
    [RoutePrefix("RetentionGroup")]
    [Route("{id}/{action=Home}")]

    public class RetentionGroupController : WebFrontEnd.Controllers._DefaultNodeController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }

        public ActionResult Home(Guid id) => View(Organization.RetentionGroups.Find(id));
        public PartialViewResult PartialGridView(Guid id)
        {
            var payments = Organization.RetentionGroups.Find(id).GeneralPayments.ToList();
            return PartialView(payments);
        }
        public ActionResult Download(Guid id)
        {
            var RetentionGroup = Organization.RetentionGroups.Find(id);

            int i = 1;
            string retention = string.Empty;
            RetentionGroup
                .GeneralPayments
                .OrderBy(s => s.TrnDate)
                .ToList()
                .ForEach(s => retention = retention + s.GetThaiRDReport(i++) + Environment.NewLine);

            return File(System.Text.Encoding.UTF8.GetBytes(retention),
                 "text/plain",
                  $"{RetentionGroup.Id}_{DateTime.Now.ToShortDateString()}.txt");
        }

        public ActionResult Export(Guid id)
        {
            var RetentionGroup = Organization.RetentionGroups.Find(id);
            var retentions = RetentionGroup.GeneralPayments.ToList();

            var report = new ERPKeeper.Node.Reports.Financial.RetentionGroup()
            {
                DataSource = retentions,
                Name = "Retentions"
            };

            report.Parameters["Title_Line1"].Value = $"ประจำเดือน {RetentionGroup.TrnDate.Month}/{RetentionGroup.TrnDate.Year}";
            report.Parameters["Title_Line1"].Visible = false;

            report.Parameters["Title_Line3"].Value = $"{Organization.DataItems.OrganizationName} ";
            report.Parameters["Title_Line3"].Visible = false;

            report.Parameters["Title_Line4"].Value = $"เลขประจำผู้เสียภาษีอากร {Organization.DataItems.TaxID}";
            report.Parameters["Title_Line4"].Visible = false;

            ViewBag.Report = report;
            return View("Export", RetentionGroup);
        }

    }
}