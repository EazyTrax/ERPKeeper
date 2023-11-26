using KeeperCore.ERPNode.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using KeeperCore.ERPNode.Models.Assets;

namespace KeeperCore.ERPNode.DAL.Assets
{
    public class FixedAssetTypes : ERPNodeDalRepository
    {
        public FixedAssetTypes(Organization organization) : base(organization)
        {

        }

        public List<FixedAssetType> All() => erpNodeDBContext.FixedAssetTypes.ToList();
        public FixedAssetType Find(Guid uid) => erpNodeDBContext.FixedAssetTypes.Find(uid);

        public void Add(FixedAssetType model)
        {
            model.Id = Guid.NewGuid();
            erpNodeDBContext.FixedAssetTypes.Add(model);
        }
    }
}