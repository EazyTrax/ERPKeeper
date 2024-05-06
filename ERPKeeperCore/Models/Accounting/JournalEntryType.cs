
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeperCore.Enterprise.Models.Accounting
{
    [Table("JournalEntryType")]
    public class JournalEntryType
    {
        [Key]
        public Guid Id { get; set; }

        public String Name { get; set; }
        public String? Detail { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public virtual ICollection<JournalEntry> JournalEntries { get; set; }

        public virtual int EntryCount => JournalEntries?.Count() ?? 0;

        public string? Description { get;  set; } = "NA";

        public bool IsSystem { get;  set; }

        public void Update(JournalEntryType journalEntryType)
        {
            Name = journalEntryType.Name;
            Detail = journalEntryType.Detail;
        }
    }
}