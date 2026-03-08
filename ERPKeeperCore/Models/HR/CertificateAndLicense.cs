using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using ERPKeeperCore.Enterprise.Models.Employees;

namespace ERPKeeperCore.Enterprise.Models.Employees
{
    public class CertificateAndLicense
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string CodeName { get; set; }
        public string IssuingOrganization { get; set; }

        public virtual ICollection<EmployeeCertificateAndLicense> EmployeeCertificateAndLicenses { get; set; } = new List<EmployeeCertificateAndLicense>();

    }
}
