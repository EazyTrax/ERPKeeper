using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Documents
{
    public enum DocumentStatus
    {
        UnProcess = 0,
        Processed = 1,
        Suspected = 2,
        Delete = 100
    }





    [Table("ERP_Files")]
    public class Document
    {
        [Key]
        public Guid Id { get; set; }
        public DocumentStatus Status { get; set; }
        public int No { get; set; }
        public String Name { get; set; }
        public bool IsFolder { get; set; }

        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Document Parent { get; set; }


        public DateTime? DocumentDate { get; set; }
        public DateTime RecordTime { get; set; }

        public int Age => (DateTime.Now - RecordTime).Days;

        
        public Guid? ReferenceId { get; set; }
        public string ObjectName { get;  set; }

        public byte[] Content { get; set; }
        public Accounting.Enums.ERPObjectType TransactionType { get; set; }



   
        public String FileName { get; set; }
        public String FileExtension { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
        public string Comment { get; set; }
        public bool IsAssign { get; set; }


        public Guid? FileGroupId { get; set; }
        [ForeignKey("FileGroupId")]
        public virtual DocumentGroup FileGroup { get; set; }


        public string Uploader { get; set; }

        public string Memo { get; set; }
        public string Creator { get; set; }
        public string TypeString { get; set; }
        public int PageCount { get; set; }
        public decimal TotalValue { get; set; }

        public Document()
        {
            
        }
    }
}