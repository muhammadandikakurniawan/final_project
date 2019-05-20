using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class ResetViewModel
    {
        public string Username { get; set; }
        [Required(ErrorMessage ="Password Lama Harus Diisi")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password Baru Harus Diisi")]
        public string NewPassword { get; set; }
    }
}