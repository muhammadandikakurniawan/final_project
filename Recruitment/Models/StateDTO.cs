using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class StateDTO
    {
        string stateId;
        string stateName;
        string stateNext;

        [Required(ErrorMessage = "State Id is Required!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Attention! min 3 & max 20 characters")]
        [DisplayName("State Id")]
        public string StateId { get => stateId; set => stateId = value; }
        [Required(ErrorMessage = "State Name is Required")]
        [StringLength(50, MinimumLength = 3,ErrorMessage = "Attention! min 3 & max 50 characters")]
        [DisplayName("State Name")]
        public string StateName { get => stateName; set => stateName = value; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Attention! min 3 & max 50 characters")]
        [DisplayName("State Next")]
        public string StateNext { get => stateNext; set => stateNext = value; }
    }
}