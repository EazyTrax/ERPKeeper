
using ERPKeeper.Node.Models.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.DAL.Items
{
    public class DataSheets : ERPNodeDalRepository
    {
        public DataSheets(Organization organization) : base(organization)
        {

        }

        public IQueryable<DataSheet> GetQuery()
        {
            return erpNodeDBContext.DataSheets;
        }

        public List<DataSheet> GetListAll()
        {
            return erpNodeDBContext.DataSheets.ToList();
        }

        public DataSheet Find(Guid id) => erpNodeDBContext.DataSheets.Find(id);

        public void Delete(Guid id)
        {
            var dataSheet = erpNodeDBContext.DataSheets.Find(id);

            erpNodeDBContext.DataSheets.Remove(dataSheet);
            organization.SaveChanges();
        }

        public DataSheet CreateNew(DataSheet dataSheet)
        {
            dataSheet.Uid = Guid.NewGuid();
            erpNodeDBContext.DataSheets.Add(dataSheet);
            return dataSheet;
        }
        public DataSheet CreateNew(string name)
        {
            var dataSheet = erpNodeDBContext.DataSheets.Create();
            dataSheet.Uid = Guid.NewGuid();
            dataSheet.Name = name;

            erpNodeDBContext.DataSheets.Add(dataSheet);
            erpNodeDBContext.SaveChanges();
            return dataSheet;
        }

        public DataSheet CreateNew(Item item,MemoryStream memoryStream, HttpPostedFileBase file)
        {
            var dataSheet = new DataSheet()
            {
                Uid = Guid.NewGuid(),
                ItemGuid = item.Uid,
                File = memoryStream.ToArray(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                ContentLength = file.ContentLength
            };

            erpNodeDBContext.DataSheets.Add(dataSheet);

            return dataSheet;
        }

    }
}
