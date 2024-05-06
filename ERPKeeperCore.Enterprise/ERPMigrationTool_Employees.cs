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
        private void CopyEmployeePositions()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.EmployeePositions
              .Select(x => x.Id)
              .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .EmployeePositions
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();


            oldModels.ForEach(a =>
            {
                Console.WriteLine($"EmployeePositions:{a.Uid}-{a.Title}");

                var exist = newOrganization.ErpCOREDBContext.EmployeePositions.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Employees.EmployeePosition()
                    {
                        Id = a.Uid,
                        Description = a.Description ?? "NA",
                        Title = a.Title,
                    };
                    newOrganization.ErpCOREDBContext.EmployeePositions.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }

        private void Copy_Employees_PaymentTypes()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.EmployeePaymentTypes
              .Select(x => x.Id)
              .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .EmployeePaymentTypes
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();


            oldModels.ForEach(a =>
            {
                Console.WriteLine($"EmployeePositions:{a.Uid}-{a.Name}");

                var exist = newOrganization.ErpCOREDBContext.EmployeePaymentTypes.FirstOrDefault(x => x.Id == a.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Employees.EmployeePaymentType()
                    {
                        Id = a.Uid,
                        Description = a.Description ?? "NA",
                        Name = a.Name,
                        ExpenseAccountId = a.AccountGuid,
                        PayDirection = (Enterprise.Models.Employees.Enums.PayDirection)a.PayDirection,
                        IsActive = a.IsActive,

                    };
                    newOrganization.ErpCOREDBContext.EmployeePaymentTypes.Add(exist);
                }
                else
                {
                    exist.ExpenseAccountId = a.AccountGuid;
                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyEmployees()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.Employees
               .Select(x => x.Id)
               .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .Employees
                .Where(i => !existModelIds.Contains(i.ProfileUid))
                .ToList();

            oldModels.ForEach(a =>
            {
                Console.WriteLine($"Employees:{a.ProfileUid}-{a.Profile.Name}");

                var exist = newOrganization.ErpCOREDBContext.Employees.FirstOrDefault(x => x.Id == a.ProfileUid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Employees.Employee()
                    {
                        Id = a.ProfileUid,
                        EmployeePositionId = a.PositionGuid,
                        Status = (Enterprise.Models.Employees.Enums.EmployeeStatus)a.Status,
                    };
                    newOrganization.ErpCOREDBContext.Employees.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyEmployeePaymentPeriods()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.EmployeePaymentPeriods
               .Select(x => x.Id)
               .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .EmployeePaymentPeriods
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"EmployeePaymentPeriods:{oldModel.Name}-{oldModel.Name}");
                var exist = newOrganization.ErpCOREDBContext.EmployeePaymentPeriods.FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Employees.EmployeePaymentPeriod()
                    {
                        Id = oldModel.Uid,
                        Date = oldModel.TrnDate,
                        Description = oldModel.Description ?? "NA",
                        Name = oldModel.Name,
                        PayFromAccountId = oldModel.PayFromAccountGuid,
                        TotalDeduction = oldModel.TotalDeduction,
                        TotalEarning = oldModel.TotalEarning
                    };

                    newOrganization.ErpCOREDBContext.EmployeePaymentPeriods.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }
        private void CopyEmployeePayments()
        {
            var existModelIds = newOrganization.ErpCOREDBContext.EmployeePayments
               .Select(x => x.Id)
               .ToList();

            var oldModels = oldOrganization.ErpNodeDBContext
                .EmployeePayments
                .Where(i => !existModelIds.Contains(i.Uid))
                .ToList();

            oldModels.ForEach(oldModel =>
            {
                Console.WriteLine($"EmployeePayments:{oldModel.Name}");
                var exist = newOrganization.ErpCOREDBContext.EmployeePayments.FirstOrDefault(x => x.Id == oldModel.Uid);

                if (exist == null)
                {
                    exist = new ERPKeeperCore.Enterprise.Models.Employees.EmployeePayment()
                    {
                        Id = oldModel.Uid,
                        EmployeeId = oldModel.EmployeeUid,
                        EmployeePaymentPeriodId = oldModel.EmployeePaymentPeriodUid,
                        Name = oldModel.Name,
                        PayFromAccountId = oldModel.PayFromAccountGuid,
                        TotalEarning = oldModel.TotalEarning,
                        TotalDeduction = oldModel.TotalDeduction,
                    };
                    newOrganization.ErpCOREDBContext.EmployeePayments.Add(exist);
                }
                else
                {

                }

                newOrganization.ErpCOREDBContext.SaveChanges();
            });
        }

    }
}
