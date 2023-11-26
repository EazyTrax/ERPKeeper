using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ERPKeeper.Node.Models.Departments
{

    [Table("ERP_Departments")]
    public class Department
    {
        [Key]
        public Guid DepartmentGuid { get; set; }
        public String Name { get; set; }




        public int EmployeeCount { get; set; }

        public void UpdateEmplyeeCount()
        {
            throw new NotImplementedException();
        }

   

        
    }
}
