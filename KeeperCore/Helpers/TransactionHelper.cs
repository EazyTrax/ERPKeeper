using KeeperCore.ERPNode.Models.Accounting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeeperCore.ERPNode.Models.Helpers
{
    public static class ObjectsHelper
    {
        public static readonly Dictionary<ERPObjectType, string> keyValuePairs = new Dictionary<ERPObjectType, string>()
        {
                {  ERPObjectType.Sale, "SL" },
                {  ERPObjectType.SalesReturn, "SR"},
                {  ERPObjectType.SaleQuote, "SE"},
                {  ERPObjectType.Purchase, "PU"},
                {  ERPObjectType.PurchaseReturn, "PR"},
                {  ERPObjectType.PurchaseQuote, "PE"},
                {  ERPObjectType.EmployeePayment, "EP"},
                {  ERPObjectType.FundTransfer, "FT"},
                {  ERPObjectType.SupplierPayment, "BP"},
                {  ERPObjectType.ReceivePayment, "RP"},
                {  ERPObjectType.CommercialPayment, "CP"},
                {  ERPObjectType.LiabilityPayment, "LP"},
                {  ERPObjectType.TaxPayment, "TP"},
                {  ERPObjectType.Customer, "CUS"},
                {  ERPObjectType.Supplier, "SUP"},
                {  ERPObjectType.Employee, "EMP"},
                {  ERPObjectType.Investor, "INV"},
                {  ERPObjectType.Item, "ITM"},
        };


        public static string TrCode(ERPObjectType type)
        {
            var entry = keyValuePairs.Where(e => e.Key == type)
                .Select(p => (string)p.Value)
                .FirstOrDefault();

            return entry;
        }

        public static ERPObjectType LookUp(string searchType)
        {
            var entry = keyValuePairs.Where(e => e.Value == searchType)
                .Select(p => (ERPObjectType)p.Key)
                .FirstOrDefault();

            return entry;
        }
    }
}
