using ERPKeeper.Node.Models.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Taxes;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Accounting.Enums;

namespace ERPKeeper.Node.DAL.Taxes
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

        public TaxCode Find(Guid? TaxCodeGuid) => (TaxCodeGuid == null) ? null : erpNodeDBContext.TaxCodes.Find(TaxCodeGuid);


        public TaxCode CreateNew(string name)
        {
            if (name == null)
                name = "New TaxCode";

            var newTaxCode = new TaxCode()
            {
                Uid = Guid.NewGuid(),
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