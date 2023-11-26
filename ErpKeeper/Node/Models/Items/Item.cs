using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Items
{
    [Table("ERP_Items")]
    public partial class Item
    {
        [Key]
        [Column("Uid")]
        public Guid Uid { get; set; }
        public ItemStatus Status { get; set; }

        [Index]
        public Enums.ItemTypes ItemType { get; set; }
        public int Level { get; set; }


        public int TotalCount { get; set; }
        public String SpanBadge
        {
            get
            {
                switch (this.ItemType)
                {
                    case Enums.ItemTypes.Asset:
                        return ERPKeeper.Helpers.Badge.Success("Ass");
                    case Enums.ItemTypes.Inventory:
                        return ERPKeeper.Helpers.Badge.Warning("Inv");
                    case Enums.ItemTypes.NonInventory:
                        return ERPKeeper.Helpers.Badge.Primary("Niv");
                    case Enums.ItemTypes.Expense:
                        return ERPKeeper.Helpers.Badge.Info("Exp");
                    case Enums.ItemTypes.Service:
                        return ERPKeeper.Helpers.Badge.Danger("Svr");
                    default:
                        return "";
                }
            }
        }

        public void UpdateFromNewERP(Item model)
        {
            //General
            this.PartNumber = model.PartNumber;
            this.UPC = model.UPC;
            this.Description = model.Description;
            this.Status = model.Status;
            this.BrandGuid = model.BrandGuid;
            this.Specification = model.Specification;

            //Sales
            this.UnitPrice = model.UnitPrice;
            this.IncomeAccountUid = model.IncomeAccountUid;

            //Purchase
            this.PurchaseAccountUid = model.PurchaseAccountUid;
        }

        [MaxLength(30)]
        public String PartNumber { get; set; }

        [Column("UPC")]
        public String UPC { get; set; }

        public String Description { get; set; }
        public bool OnlineSale { get; set; }
        public int CommercialAmount { get; set; }


        public string GroupName { get; set; }


        [Column("SalesPrice", TypeName = "Money")]
        public Decimal UnitPrice { get; set; }
        public Decimal PurchasePrice { get; set; }
        public Guid? PriceRangeUid { get; set; }
        [ForeignKey("PriceRangeUid")]
        public virtual PriceRange PriceRange { get; set; }

        public virtual string HarmonizedCode { get; set; }



        public virtual ICollection<Transactions.CommercialItem> CommercialItems { get; set; }
        public virtual ICollection<Estimations.QuoteItem> QuoteItems { get; set; }
        public virtual ICollection<ItemImage> Images { get; set; }
        public virtual ICollection<ItemParameter> ItemParameters { get; set; }

        public List<ItemParameter> GetParametersList()
        {
            List<ItemParameter> parameters = new List<ItemParameter>();

            if (this.ItemParameters != null)
                parameters.AddRange(this.ItemParameters.ToList());

            if (this.ParentUid != null && this.Parent.ItemParameters != null)
                parameters.AddRange(this.Parent.GetParametersList());

            return parameters;
        }


        public Guid? GroupItemUid { get; set; }
        [ForeignKey("GroupItemUid")]
        public virtual GroupItem GroupItem { get; set; }



        public virtual ItemImage DefaultImage => Images?.FirstOrDefault();
        public string Specification { get; set; }
        public Guid? BrandGuid { get; set; }
        [ForeignKey("BrandGuid")]
        public virtual Brand Brand { get; set; }
        public String BrandName => Brand?.Name;

        public Guid? DefaultSupplyierId { get; set; }
        [ForeignKey("DefaultSupplyierId")]
        public virtual Profiles.Profile DefaultSupplyier { get; set; }

        [DisplayName("Purchase Account")]
        [Column("PurchaseAccountUid")]
        public Guid? PurchaseAccountUid { get; set; }
        [ForeignKey("PurchaseAccountUid")]
        public virtual AccountItem PurchaseAccount { get; set; }


        [DisplayName("Income Account")]
        [Column("IncomeAccountUid")]
        public Guid? IncomeAccountUid { get; set; }
        [ForeignKey("IncomeAccountUid")]
        public virtual AccountItem IncomeAccount { get; set; }


        public virtual AccountItem GetPurchaseAccount
        {
            get
            {
                if (this.ItemType == Enums.ItemTypes.Asset)
                    return this.FixedAssetType?.AssetAccount;
                else
                    return this.PurchaseAccount;
            }
        }

        public DateTime? StockCalculateDate { get; set; }

        [Column("stockValue")]
        public decimal StockValue { get; set; }
        public int StockAmount { get; set; }


        public decimal OpeningPurchaseCost { get; set; }
        public int OpeningPurchaseAmount { get; set; }


        public string QuickFinderKey { get; set; }
        public string QuickFinderValue { get; set; }

        public string GetQuickFinderValue => QuickFinderValue ?? this.Brand?.Name;
        public DateTime? CreatedDate { get; set; }
        public void AddImage(byte[] toPutInDb)
        {
            var itemImage = new ERPKeeper.Node.Models.Items.ItemImage()
            {
                Uid = Guid.NewGuid(),
                ItemGuid = Uid,
                Image = toPutInDb
            };

            this.Images.Add(itemImage);
        }
        public void AddItemParameter(ItemParameterType type, string value)
        {
            if (this.ItemParameters == null)
                return;

            var existParameter = this.ItemParameters
                .Where(ip => ip.ParameterTypeUid == type.Uid)
                .FirstOrDefault();

            if (existParameter != null)
                throw new Exception("Exist Parameter");

            var parameter = new ItemParameter()
            {
                Uid = Guid.NewGuid(),
                ParameterTypeUid = type.Uid,
                ParameterType = type,
                Value = value
            };

            this.ItemParameters.Add(parameter);

        }
        public void AssignLevel(int CurrentLevel)
        {
            CurrentLevel++;
            this.Level = CurrentLevel;

            this.Child.Where(c => c.ItemType == Enums.ItemTypes.Group)
                .ToList()
                .ForEach(g =>
                {
                    if (g.Level == 0)
                    {
                        //       Console.WriteLine("Dig inside " + CurrentLevel);
                        g.AssignLevel(CurrentLevel);
                    }
                    else
                    {
                        Console.WriteLine("Loop Found");
                        g.ParentUid = null;
                        g.Parent = null;
                    }
                });

        }
    }
}