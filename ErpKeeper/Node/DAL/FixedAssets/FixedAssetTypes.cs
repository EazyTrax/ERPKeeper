using ERPKeeper.Node.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.Models.Assets;

namespace ERPKeeper.Node.DAL.Assets
{
    public class FixedAssetTypes : ERPNodeDalRepository
    {
        public FixedAssetTypes(Organization organization) : base(organization)
        {

        }

        public List<FixedAssetType> All() => erpNodeDBContext.AssetTypes.ToList();
        public FixedAssetType Find(Guid uid) => erpNodeDBContext.AssetTypes.Find(uid);

        public void Add(FixedAssetType model)
        {
            model.Uid = Guid.NewGuid();
            erpNodeDBContext.AssetTypes.Add(model);
        }

        public void Refresh()
        {
            All().ForEach(x => x.Refresh());
            erpNodeDBContext.SaveChanges();
        }
    }
}