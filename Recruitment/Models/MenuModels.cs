using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //.Schema
using System.Web.Mvc;

namespace Recruitment.Models
{
    public class MenuModels
    {

        public string MenuId { get; set; }
        [Required(ErrorMessage = "Menu Name is Required!")]
        public string MenuName { get; set; }
        [Required(ErrorMessage = "Role Id is Required!")]
        public string RoleId { get; set; }
        
        public string Action { get; set; }
        
        public string Controller { get; set; }


    }

}