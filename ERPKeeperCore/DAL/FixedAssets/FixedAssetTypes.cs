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

        public List<AssetType> All() => erpNodeDBContext.FixedAssetTypes.ToList();
        public AssetType Find(Guid uid) => erpNodeDBContext.FixedAssetTypes.Find(uid);

        public void Add(AssetType model)
        {
            model.Id = Guid.NewGuid();
            erpNodeDBContext.FixedAssetTypes.Add(model);
        }
    }
}