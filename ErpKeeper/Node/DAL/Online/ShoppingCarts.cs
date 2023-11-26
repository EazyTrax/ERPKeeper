using ERPKeeper.Node.Models.Items;
using System.Collections.Generic;
using System.Linq;
using System;
using ERPKeeper.Node.Models.Online;

namespace ERPKeeper.Node.DAL.Items
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

        public List<ShoppingCartItem> GetCartItemsByProfile(string sessionId, Guid? profileGuid)
        {
            if (profileGuid == null)
                return erpNodeDBContext.ShoppingCartItems
                     .Where(cartItem => cartItem.SessionId == sessionId)
                     .ToList();
            else
                return erpNodeDBContext.ShoppingCartItems
                    .Where(cartItem => cartItem.SessionId == sessionId || cartItem.ProfileUid == profileGuid)
                    .ToList();
        }

        public void AddCart(string sessionId, Guid? ProfileUid, Guid ItemUid, int Amount)
        {
            //Calculate Discount
            var item = erpNodeDBContext.Items.Find(ItemUid);

            var newcartItem = new ShoppingCartItem()
            {
                Uid = Guid.NewGuid(),
                SessionId = sessionId,
                ProfileUid = ProfileUid,
                ItemGuid = ItemUid,
                UnitPrice = item.UnitPrice,
                Amount = Amount,
                CreateDate = DateTime.Now
            };

            erpNodeDBContext.ShoppingCartItems.Add(newcartItem);
            erpNodeDBContext.SaveChanges();

        }

        public Models.Estimations.SaleQuote SubmitOrder(Guid profileGuid)
        {
            var orderItems = erpNodeDBContext
               .ShoppingCartItems
               .Where(items => items.ProfileUid == profileGuid)
               .ToList();



            if (orderItems.Count > 0)
            {
                var profile = organization.Profiles.Find(profileGuid);
                var salesQuote = organization.SaleEstimates.Create(profile, DateTime.Now);
                salesQuote.Reference = "Online Sale";

                salesQuote.Items = new HashSet<Models.Estimations.QuoteItem>();

                orderItems.ForEach(orderItem =>
                {
                    var item = erpNodeDBContext.Items.Find(orderItem.ItemGuid);
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