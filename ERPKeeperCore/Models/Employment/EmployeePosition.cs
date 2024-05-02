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

    public class EmployeePosition
    {
        [Key]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public int Requried { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        public virtual int EmployeeCount => this.Employees?.Count() ?? 0;

        public void Update(EmployeePosition position)
        {
            this.Title = position.Title;
            this.Description = position.Description;
        }
    }
}
