using System;
using System.Collections.Generic;
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

        [Required(ErrorMessage = "Required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Input is too long")]
        public string StateId { get => stateId; set => stateId = value; }
        [Required(ErrorMessage = "Required")]
        [StringLength(50, ErrorMessage = "Input is too long")]
        public string StateName { get => stateName; set => stateName = value; }
        [StringLength(500, ErrorMessage = "Input is too long")]
        public string StateNext { get => stateNext; set => stateNext = value; }
    }
}