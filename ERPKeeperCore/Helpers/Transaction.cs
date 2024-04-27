
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPKeeperCore.Enterprise.Models.Enums;

namespace ERPKeeperCore.Enterprise.Models.Helpers
{
    public static class ObjectsHelper
    {
        public static readonly Dictionary<ObjectType, string> keyValuePairs = new Dictionary<ObjectType, string>()
        {
                {  ObjectType.Sale, "SL" },
                {  ObjectType.SalesReturn, "SR"},
                {  ObjectType.SaleQuote, "SE"},
                {  ObjectType.Purchase, "PU"},
                {  ObjectType.PurchaseReturn, "PR"},
                {  ObjectType.PurchaseQuote, "PE"},
                {  ObjectType.EmployeePayment, "EP"},
                {  ObjectType.FundTransfer, "FT"},
                {  ObjectType.SupplierPayment, "BP"},
                {  ObjectType.ReceivePayment, "RP"},
                {  ObjectType.CommercialPayment, "CP"},
                {  ObjectType.LiabilityPayment, "LP"},
                {  ObjectType.TaxPayment, "TP"},
                {  ObjectType.Customer, "CUS"},
                {  ObjectType.Supplier, "SUP"},
                {  ObjectType.Employee, "EMP"},
                {  ObjectType.Investor, "INV"},
                {  ObjectType.Item, "ITM"},
        };


        public static string TrCode(ObjectType type)
        {
            var entry = keyValuePairs.Where(e => e.Key == type)
                .Select(p => (string)p.Value)
                .FirstOrDefault();

            return entry;
        }

        public static ObjectType LookUp(string searchType)
        {
            var entry = keyValuePairs.Where(e => e.Value == searchType)
                .Select(p => (ObjectType)p.Key)
                .FirstOrDefault();

            return entry;
        }
    }
}
