using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class Sumber
    {
        [Required]
        public string SumberId { get; set; }
        [Required]
        public string SumberNama { get; set; }
    }
}