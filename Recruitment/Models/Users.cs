using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Username is Required!")]
        //[StringLength(15)]
        [RegularExpression("(.){3,20}", ErrorMessage = "Attention! min 3 & max 20 characters")]        
        public string Username { get; set; }
        [Required(ErrorMessage = "Full Name is Required!")]
        //[StringLength(15)]
        [RegularExpression("(.){3,50}", ErrorMessage = "Attention! min 3 & max 50 characters")]
        [DisplayName("Full Name")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Password is Required!")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Attention! min 3 & max 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role Id is Required!")]
        [DisplayName("Role Id")]
        public string Roleid { get; set; }

        public int UserId { get; set; }
    }
}