using ERPKeeper.Node.DAL;
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace ERPKeeper.Cmd
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] Enterprises = new string[] { "tec", "bit" };

            foreach (var enterpriseDB in Enterprises)
            {
                Console.WriteLine("###########################################################");
                Console.WriteLine(enterpriseDB);
                //UnPost(enterpriseDB);
               // Post(enterpriseDB);
                BasicProcess(enterpriseDB);
                //UpdateTransactionLedger(enterpriseDB);
                Console.WriteLine(">{0} Complete", DateTime.Now.ToLongTimeString());
                Console.WriteLine("###########################################################" + Environment.NewLine + Environment.NewLine);
            }

            Console.WriteLine("###########################################################");
            Console.WriteLine("END");
            Console.WriteLine("###########################################################");
            Console.ReadLine();
        }


        public static void CloneItems()
        {
            var tec = new Node.DAL.Organization("tec");
            var sourceItems = tec.ErpNodeDBContext.Items
                .Where(i => i.BrandGuid != null)
                .Where(i => i.Brand.Name == "Azsic")
                .ToList();


            var bit = new Node.DAL.Organization("bit");
            sourceItems.ForEach(i =>
            {
                var bitItem = bit.Items.FindByPartNumber(i.PartNumber);
                if (bitItem == null)
                {
                    bitItem = new Node.Models.Items.Item()
                    {
                        Uid = i.Uid,
                        PartNumber = i.PartNumber,
                        Description = i.Description,
                        PurchasePrice = i.PurchasePrice,
                        UnitPrice = i.UnitPrice,
                        ItemType = Node.Models.Items.Enums.ItemTypes.Inventory,
                        Brand = bit.ItemBrands.Find("Azsic"),
                        IncomeAccountUid = bit.SystemAccounts.Income.Uid,
                        PurchaseAccountUid = bit.SystemAccounts.COSG.Uid,
                    };
                    Console.WriteLine(i.PartNumber);

                    bit.ErpNodeDBContext.Items.Add(bitItem);

                }
                else
                {
                    bitItem.ItemType = Node.Models.Items.Enums.ItemTypes.Inventory;
                }

                bit.ErpNodeDBContext.SaveChanges();

            });


        }

        private static void BasicProcess(string profileDB)
        {
            using (var organization = new Node.DAL.Organization(profileDB, true))
            {
                organization.Items.GetAll();
                organization.SaveChanges();
            }
        }

        private static void UpdateTransactionLedger(string profileDB)
        {
            var organization = new Node.DAL.Organization(profileDB, true);
            organization.ErpNodeDBContext.TransactionLedgers.ToList()
                  .ForEach(s =>
                  {
                      var TotalDebit = s.Ledgers?.Sum(l => l.Debit) ?? 0;
                      var TotalCredit = s.Ledgers?.Sum(l => l.Credit) ?? 0;

                      if (TotalDebit != TotalCredit)
                      // if (s.TotalCredit != TotalCredit || s.TotalDebit != TotalDebit)
                      {
                          Console.WriteLine($"{s.TransactionName}:{s.TransactionNo}: {s.TransactionType}  / {s.TotalDebit} {s.TotalCredit} / {s.TotalDebit - s.TotalCredit}");


                          //foreach (var ledger in s.Ledgers)
                          //{
                          //    Console.WriteLine($"{ledger.Balance} { Math.Round(ledger.Balance, 2, MidpointRounding.ToEven)}");
                          //}

                          //TotalDebit = Math.Round(TotalDebit, 2, MidpointRounding.ToEven);
                          //TotalCredit = Math.Round(TotalCredit, 2, MidpointRounding.ToEven);

                          //Console.WriteLine($"{TotalDebit} {TotalCredit} / {TotalDebit - TotalCredit}");


                      }


                  });
            //  organization.erpNodeDBContext.SaveChanges();
        }


        private static void Post(string profileDB)
        {
            using (var organization = new Node.DAL.Organization(profileDB, true))
            {
                organization.LedgersDal.PostLedgers();
                organization.ChartOfAccount.UpdateBalance();
               // organization.ChartOfAccount.GenerateHistoryBalance();
            }
        }
        private static void UnPost(string profileDB)
        {
            var organization = new Node.DAL.Organization(profileDB, true);
            organization.LedgersDal.UnPost();
            organization.ChartOfAccount.UpdateBalance();
        }
    }
}