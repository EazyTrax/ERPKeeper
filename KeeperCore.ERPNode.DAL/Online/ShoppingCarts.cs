using KeeperCore.ERPNode.Models.Items;
using System.Collections.Generic;
using System.Linq;
using System;
using KeeperCore.ERPNode.Models.Online;

namespace KeeperCore.ERPNode.DAL.Items
{
    public class ShoppingCarts : ERPNodeDalRepository
    {
        public ShoppingCarts(Organization organization) : base(organization)
        {

        }


        public List<ShoppingCartItem> AllCartItems
        {
            get { return erpNodeDBContext.ShoppingCartItems.ToList(); }
        }

        public List<ShoppingCartItem> GetCartItemsByProfile(string sessionId, Guid? profileId)
        {
            if (profileId == null)
                return erpNodeDBContext.ShoppingCartItems
                     .Where(cartItem => cartItem.SessionId == sessionId)
                     .ToList();
            else
                return erpNodeDBContext.ShoppingCartItems
                    .Where(cartItem => cartItem.SessionId == sessionId || cartItem.ProfileId == profileId)
                    .ToList();
        }

        public void AddCart(string sessionId, Guid? ProfileId, Guid ItemId, int Amount)
        {
            //Calculate Discount
            var item = erpNodeDBContext.Items.Find(ItemId);

            var newcartItem = new ShoppingCartItem()
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                ProfileId = ProfileId,
                ItemId = ItemId,
                UnitPrice = item.SalePrice,
                Amount = Amount,
                CreateDate = DateTime.Now
            };

            erpNodeDBContext.ShoppingCartItems.Add(newcartItem);
            erpNodeDBContext.SaveChanges();

        }

        public Models.Estimations.SaleQuote SubmitOrder(Guid profileId)
        {
            var orderItems = erpNodeDBContext
               .ShoppingCartItems
               .Where(items => items.ProfileId == profileId)
               .ToList();



            if (orderItems.Count > 0)
            {
                var profile = organization.Profiles.Find(profileId);
                var salesQuote = organization.SaleEstimates.Create(profile, DateTime.Now);
                salesQuote.Reference = "Online Sale";

                salesQuote.Items = new HashSet<Models.Estimations.QuoteItem>();

                orderItems.ForEach(orderItem =>
                {
                    var item = erpNodeDBContext.Items.Find(orderItem.ItemId);
                    salesQuote.AddItem(item, orderItem.Amount);
                });

                salesQuote.Calculate();
                erpNodeDBContext.SaveChanges();


                erpNodeDBContext.ShoppingCartItems.RemoveRange(orderItems);
                erpNodeDBContext.SaveChanges();

                return salesQuote;
            }
            return null;
        }



    }
}