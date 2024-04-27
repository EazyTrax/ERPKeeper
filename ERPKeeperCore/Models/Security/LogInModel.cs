
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.Enterprise.Models.Security
{
    [NotMapped]
    public class LogInModel
    {

        [Required(ErrorMessage = "Please enter an email address")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Please enter an password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Persistent { get; set; }
        public string AppId { get; set; }
        public string ReturnUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
