using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models
{

    [Table("ERP_Locations")]
    public class Location
    {
        [Key]
        public Guid Uid { get; set; }
        public String Name { get; set; }
        public LocationType Type { get; set; }
        public Guid? ParentUid { get; set; }
        [ForeignKey("ParentUid")]
        public virtual Location Parent { get; set; }
        public virtual ICollection<Location> Child { get; set; }
    }

    public enum LocationType
    {
        Land = 1,
        Building = 10,
        Floor = 20,
        Room = 30,
        Container = 40,
        Locker = 50
    }

}