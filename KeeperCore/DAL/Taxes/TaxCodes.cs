using KeeperCore.ERPNode.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Taxes;
using KeeperCore.ERPNode.Models.ChartOfAccount;
using KeeperCore.ERPNode.Models.Accounting.Enums;

namespace KeeperCore.ERPNode.DAL.Taxes
{
    public class TaxCodes : ERPNodeDalRepository
    {

        public TaxCodes(Organization organization) : base(organization)
        {

        }


        public List<TaxCode> ListAll() => erpNodeDBContext.TaxCodes.ToList();
        public List<TaxCode> GetList(ERPObjectType trType)
        {
            switch (trType)
            {
                case ERPObjectType.Sale:
                case ERPObjectType.PurchaseReturn:
                    return this.ListOutputTax;

                case ERPObjectType.Purchase:
                case ERPObjectType.SalesReturn:
                    return this.ListInputTax;
            }

            throw new NotImplementedException("Transaction Type Not Define");
        }


        public List<TaxCode> ListInputTax => erpNodeDBContext
            .TaxCodes.Where(t => t.TaxDirection == Models.Taxes.Enums.TaxDirection.Input)
            .ToList();

        public List<TaxCode> ListOutputTax => erpNodeDBContext.TaxCodes
            .Where(t => t.TaxDirection == Models.Taxes.Enums.TaxDirection.Output)
            .ToList();

        public TaxCode Find(Guid? TaxCodeId) => (TaxCodeId == null) ? null : erpNodeDBContext.TaxCodes.Find(TaxCodeId);


        public TaxCode CreateNew(string name)
        {
            if (name == null)
                name = "New TaxCode";

            var newTaxCode = new TaxCode()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            erpNodeDBContext.TaxCodes.Add(newTaxCode);
            organization.SaveChanges();
            return newTaxCode;
        }

        public TaxCode GetDefault => erpNodeDBContext.TaxCodes.Where(t => t.isDefault).FirstOrDefault();

        public TaxCode GetDefaultInput => erpNodeDBContext.TaxCodes.Where(t => t.TaxDirection == Models.Taxes.Enums.TaxDirection.Input).FirstOrDefault();
        public TaxCode GetDefaultOuput => erpNodeDBContext.TaxCodes.Where(t => t.TaxDirection == Models.Taxes.Enums.TaxDirection.Output).FirstOrDefault();

        public void Remove(TaxCode taxCode)
        {
            erpNodeDBContext.TaxCodes.Remove(taxCode);
            organization.SaveChanges();
        }
    }
}