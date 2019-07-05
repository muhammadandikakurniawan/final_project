using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //.Schema
using System.Web.Mvc;
using System.ComponentModel;

namespace Recruitment.Models
{
    public class MenuModels
    {

        public string MenuId { get; set; }
        [Required(ErrorMessage = "Menu Name is Required!")]
        [RegularExpression("(.){3,20}", ErrorMessage = "Attention! min 3 & max 20 characters")]
        [DisplayName("Menu Name")]
        public string MenuName { get; set; }
        [Required(ErrorMessage = "Role Id is Required!")]
        //[RegularExpression("(.){3,50}", ErrorMessage = "Attention! min 3 & max 50 characters")]
        [DisplayName("Role Id")]
        public string RoleId { get; set; }
        [DisplayName("Action Name")]
        [RegularExpression("(.){3,20}", ErrorMessage = "Attention! min 3 & max 20 characters")]
        public string Action { get; set; }
        [DisplayName("Controller Name")]
        [RegularExpression("(.){3,20}", ErrorMessage = "Attention! min 3 & max 20 characters")]
        public string Controller { get; set; }


    }

}