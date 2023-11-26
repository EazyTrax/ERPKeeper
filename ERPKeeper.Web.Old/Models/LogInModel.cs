using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace ERPKeeper.WebFrontEnd.Models
{
    public class LogInModel
    {

        [Required(ErrorMessage = "Please enter Email")]
        public String Email { get; set; }



        [Required(ErrorMessage = "Please enter password.")]
        [DataType(DataType.Password)]
        public String Password { get; set; }



        [Required]
        public bool Persistent { get; set; }


        public string AppId { get; set; }
        public string ReturnUrl { get; set; }
    }
}