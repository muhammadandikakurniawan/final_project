using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class Users
    {
        [Required(ErrorMessage = "Username Harus Diisi")]
        [StringLength(15)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Full Name Harus Diisi")]
        [StringLength(15)]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Password Harus Diisi")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role Id Harus Diisi")]
        public string Roleid { get; set; }
    }
}