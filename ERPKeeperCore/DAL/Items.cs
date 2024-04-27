
using ERPKeeperCore.Enterprise.DAL.Company;
using ERPKeeperCore.Enterprise.DBContext;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Accounting;
using ERPKeeperCore.Enterprise.Models.Items;
using ERPKeeperCore.Enterprise.Models.Items.Enums;

namespace ERPKeeperCore.Enterprise.DAL
{
    public class Items : ERPNodeDalRepository
    {
        public Items(EnterpriseRepo organization) : base(organization)
        {

        }

        public List<Item> GetAll()
        {
            return erpNodeDBContext.Items.ToList();
        }



        public Item? Find(Guid Id) => erpNodeDBContext.Items.Find(Id);

        public object Copy(Item item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}