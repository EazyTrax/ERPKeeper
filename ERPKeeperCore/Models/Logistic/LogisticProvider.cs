using System.ComponentModel.DataAnnotations;


namespace ERPKeeperCore.Enterprise.Models.Logistic
{
    public partial class LogisticProvider
    {
        [Key]
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String WebSite { get; set; }



        public virtual ICollection<Shipment> Shipments { get; set; }
    }

}
