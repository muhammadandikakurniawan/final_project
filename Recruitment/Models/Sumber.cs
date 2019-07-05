using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //.Schema
using System.Web.Mvc;
using System.ComponentModel;

namespace Recruitment.Models
{
    public class Sumber
    {
        [Required(ErrorMessage = "Source Id is Required!")]
        [DisplayName("Source Id")]
        public string SumberId { get; set; }
        [Required(ErrorMessage = "Source Name is Required!")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Attention! min 3 & max 50 characters")]
        [DisplayName("Source Name")]
        public string SumberNama { get; set; }
    }
}