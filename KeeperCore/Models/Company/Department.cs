using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KeeperCore.ERPNode.Models.Departments
{

    [Table("ERP_Departments")]
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }
        public String Name { get; set; }

        public Guid? ResponsibleProfileId { get; set; }
        [ForeignKey("ResponsibleProfileId")]
        public virtual Profiles.Profile ResponsibleProfile { get; set; }


        public int EmployeeCount { get; set; }

        public void UpdateEmplyeeCount()
        {
            throw new NotImplementedException();
        }

   

        
    }
}
