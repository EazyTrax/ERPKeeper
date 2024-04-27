using ERPKeeperCore.Enterprise.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.DAL.Company
{
    public class DataItems : ERPNodeDalRepository
    {

        public DataItems(EnterpriseRepo organization) : base(organization)
        {
           
        }
        public String? OrganizationName => Get(DataItemKey.OrganizationName);
        public String? OrganizationHeader => Get(DataItemKey.OrganizationHeader);
        public String? TaxID => Get(Models.DataItemKey.TaxId);
        public DateTime FirstDate
        {
            get
            {
                var firstDateString = this.Get(Models.DataItemKey.FirstDate);

                if (firstDateString != null)
                    return DateTime.Parse(firstDateString, System.Globalization.CultureInfo.InvariantCulture);
                else
                    return new DateTime(DateTime.Now.Year, 1, 1);
            }

            set
            {
                Set(Models.DataItemKey.FirstDate, value.ToString(System.Globalization.CultureInfo.InvariantCulture));
                erpNodeDBContext.SaveChanges();
            }
        }

        public List<DataItem> GetAll()
        {
            return erpNodeDBContext.DataItems.ToList();
        }

        public DataItem Get(Guid Id) => erpNodeDBContext.DataItems.Find(Id);
        public String? Get(DataItemKey key) => erpNodeDBContext.DataItems.Where(dt => dt.Key == key).FirstOrDefault()?.Value;

        public void Set(Models.DataItemKey key, string value)
        {
            var dataItem = erpNodeDBContext.DataItems.Where(dt => dt.Key == key).FirstOrDefault();
            if (dataItem != null)
            {
                dataItem.Value = value;
            }
            else
            {
                dataItem = new Models.DataItem()
                {
                    Id = Guid.NewGuid(),
                    Key = key,
                    Value = value
                };
                erpNodeDBContext.DataItems.Add(dataItem);
            }
            erpNodeDBContext.SaveChanges();
        }
    }
}