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

        [Required]
        public string StateId { get => stateId; set => stateId = value; }
        [Required]
        public string StateName { get => stateName; set => stateName = value; }
        public string StateNext { get => stateNext; set => stateNext = value; }
    }
}