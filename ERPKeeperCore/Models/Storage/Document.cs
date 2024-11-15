using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Profiles;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Storage
{
    public partial class Document
    {
        [Key]
        public Guid Id { get; set; }
        public TransactionType Type { get; set; }
        public String Note { get; set; }
        public String? Description { get; set; }
        public String? FileName { get; set; }
        public String? FileExtension { get; set; }
        public String? ContentType { get; set; }
        public int Size { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid? TransactionId { get; set; }

        public Guid? ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile? Profile { get; set; }



        public String FileSizeString => GetReadableFileSize(Size);

        public string AgeAgo
        {
            get
            {
                var now = DateTime.Now;
                var timeSpan = now - CreatedDate;

                if (timeSpan.TotalDays < 1)
                    return "Less than a day";

                var years = (int)(timeSpan.TotalDays / 365);
                var remainingDaysAfterYears = (int)(timeSpan.TotalDays % 365);

                var months = remainingDaysAfterYears / 30;
                var days = remainingDaysAfterYears % 30;

                var ageAgo = "";

                if (years > 0)
                    ageAgo += $"{years}Y ";
                if (months > 0)
                    ageAgo += $"{months}M ";
                if (days > 0)
                    ageAgo += $"{days}D";

                return ageAgo.Trim();
            }
        }



        public string GetReadableFileSize(long fileSize)
        {
            if (fileSize >= 1024L * 1024L * 1024L)
                return $"{(fileSize / (1024.0 * 1024.0 * 1024.0)):0.##}G";
            else if (fileSize >= 1024L * 1024L)
                return $"{(fileSize / (1024.0 * 1024.0)):0.##}M";
            else if (fileSize >= 1024L)
                return $"{(fileSize / 1024.0):0.##}K";
            else
                return $"{fileSize}B";
        }

        public byte[]? GetOriginalFile(string companyId, Guid makerId)
        {
            string filePath = $"C:\\ERP\\Comapny\\{companyId}\\Files\\{Id}";
            if (System.IO.File.Exists(filePath))
                return System.IO.File.ReadAllBytes(filePath);
            else
                return null;
        }

        public string GetFilePath(string companyId, Guid makerId)
        {
            string filePath = $"C:\\ERP\\Comapny\\{companyId}\\Files\\{Id}";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            return filePath;
        }

        public void AddContent(string companyId, byte[] content)
        {
            string filePath = $"C:\\ERP\\Comapny\\{companyId}\\Files\\{Id}";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            System.IO.File.WriteAllBytes(filePath, content);
        }

        public void RemovePhysicalFile(string companyId, Guid makerId)
        {
            string filePath = $"C:\\ERP\\Comapny\\{companyId}\\Files\\{Id}";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
