using ERPKeeper.Node.DAL;
using ERPKeeperCore.Enterprise;
using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Items.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.CMD
{
    public partial class ERPMigrationTool
    {
        private void Copy_Taxes_TaxCode()
        {
            var oldModels = oldOrganization.ErpNodeDBContext.TaxCodes.ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.TaxCodes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Taxes.TaxCode()
                    {
                        Id = a.Uid,
                        Name = a.Name,
                        Description = a.Description,
                        Rate = a.TaxRate,
                        TaxDirection = (Enterprise.Models.Taxes.Enums.TaxDirection)a.TaxDirection,
                        AssignAccountId = a.AssignAccountGuid,
                        CloseToAccountId = a.CloseToAccountGuid,
                        InputTaxAccountId = a.InputTaxAccountGuid,
                        OutputTaxAccountId = a.OutputTaxAccountGuid,
                        IsDefault = a.isDefault,
                        TaxAccountId = a.TaxAccountGuid,
                        TaxReceivableAccountId = a.TaxReceivableAccountGuid,
                        TaxPayableAccountId = a.TaxPayableAccountGuid,
                    };

                    newOrganization.ErpCOREDBContext.TaxCodes.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void Copy_Taxes_TaxPeriod()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.TaxPeriods
              .Select(x => x.Id)
              .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.TaxPeriods
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();

          

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"PROF:{a.Name}-{a.PeriodStartDate}");

                var exist = newOrganization.ErpCOREDBContext.TaxPeriods.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Taxes.TaxPeriod()
                    {
                        Id = a.Uid,
                        StartDate = a.PeriodStartDate,
                        CloseToAccountId = a.CloseToAccountGuid,
                        FiscalYearId = a.FiscalYearUid,
                        LiabilityPaymentId = a.LiabilityPaymentUid,
                        Memo = a.Memo,
                        No = a.No,
                    };

                    newOrganization.ErpCOREDBContext.TaxPeriods.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void Copy_Taxes_IncomeTaxes()
        {

            var existModelIds = newOrganization.ErpCOREDBContext.IncomeTaxes
                .Select(x => x.Id)
                .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext.IncomeTaxes
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"IncomeTax:{oldModel.Name}");

                var exist = newOrganization.ErpCOREDBContext.IncomeTaxes.FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Taxes.IncomeTax()
                    {
                        Id = oldModel.Uid,
                        FiscalYearId = oldModel.FiscalYearUid,
                        ExpenseAccountId = oldModel.IncomeTaxExpenseAccountGuid,
                        LiabilityAccountId = oldModel.LiabilityAccountGuid,
                        ProfitAmount = oldModel.ProfitAmount,
                        TaxAmount = oldModel.TaxAmount,
                        Date = oldModel.TrDate,
                        Memo = oldModel.Memo ?? "NA",
                        No = oldModel.No,
                    };

                    newOrganization.ErpCOREDBContext.IncomeTaxes.Add(exist);
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }
                else
                {
                    exist.TaxAmount = oldModel.TaxAmount;
                    newOrganization.ErpCOREDBContext.SaveChanges();
                }


            });
        }


    }
}
