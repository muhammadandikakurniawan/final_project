using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //.Schema
using System.Web.Mvc;
using System.ComponentModel;

namespace Recruitment.Models
{
    public class PositionPoco
    {
        [DisplayName("Position Id")]
        public string IdPosisi { get; set; }
        [Required(ErrorMessage = "Position Name is Required!")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Attention! min 3 & max 50 characters")]
        [DisplayName("Position Name")]
        public string Nama { get; set; }
    }
}