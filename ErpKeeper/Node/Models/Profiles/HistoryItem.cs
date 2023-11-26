


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPKeeper.Node.Models.Profiles
{

    [Table("ERP_Profiles_HistoryItems")]
    public class HistoryItem
    {

        public HistoryItem()
        {
            RecordDate = DateTime.Now;
        }


        [Key]
        public Guid Uid { get; set; }
        public DateTime RecordDate { get; set; }



        [Column("HistoryRelatedProfileUid")]
        public Guid? ProfileGuid { get; set; }
        [ForeignKey("ProfileGuid")]
        public virtual Profiles.Profile Profile { get; set; }



        public String Title { get; set; }
        public String Detail { get; set; }
        public Guid KeyUid { get; set; }


        [Column("Url")]
        public string Url { get; set; }
        public string Type { get; set; }
    }
}