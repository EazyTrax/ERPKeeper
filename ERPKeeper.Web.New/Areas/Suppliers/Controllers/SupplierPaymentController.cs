using ERPKeeperCore.Enterprise.Models.Suppliers;
using ERPKeeperCore.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPKeeperCore.Web.Areas.Suppliers.Controllers
{

    [Route("/{CompanyId}/Suppliers/SupplierPayments/{TransactionId:Guid}/{action=index}")]
    public class SupplierPaymentController : _Suppliers_Base_Controller
    {
        public Guid TransactionId => Guid.Parse(RouteData.Values["TransactionId"].ToString());

        public IActionResult Index()
        {
            var SupplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(TransactionId);
            return View(SupplierPayment);
        }


        public IActionResult Export()
        {
            var SupplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(TransactionId);
            return View(SupplierPayment);
        }

        public IActionResult Update()
        {
            var SupplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(TransactionId);

            SupplierPayment.UpdateBalance();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Update(SupplierPayment model)
        {
            var supplierPayment = Organization.ErpCOREDBContext.SupplierPayments.Find(TransactionId);

            supplierPayment.Date = model.Date;
            supplierPayment.Reference = model.Reference;

            supplierPayment.RetentionTypeId = model.RetentionTypeId;
            supplierPayment.PayFrom_AssetAccountId = model.PayFrom_AssetAccountId;

            supplierPayment.AmountBankFee = model.AmountBankFee;
            supplierPayment.AmountDiscount = model.AmountDiscount;
            supplierPayment.Memo = model.Memo;


            supplierPayment.UpdateBalance();
            Organization.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
