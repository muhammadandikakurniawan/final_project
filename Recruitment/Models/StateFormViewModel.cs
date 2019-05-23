using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Models
{
    public class StateFormViewModel
    {
        List<StateDTO> stateDTOs;
        [Required]
        StateDTO formState;

        public List<StateDTO> StateDTOs { get => stateDTOs; set => stateDTOs = value; }
        public StateDTO FormState { get => formState; set => formState = value; }

        public List<SelectListItem> StateListItems()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            result.Add(new SelectListItem { Text = "None", Value = null });
            try
            {
                foreach (StateDTO stateDTO in stateDTOs)
                {
                    result.Add(new SelectListItem { Text = stateDTO.StateId + "(" + stateDTO.StateName + ")", Value = stateDTO.StateId });
                }
            }
            catch (NullReferenceException e)
            {

            }

            return result;
        }
    }
}