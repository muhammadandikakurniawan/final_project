using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class SkillModels
    {
        [Required(ErrorMessage = "Skill Id is Required")]
        public long SkillId { get; set; }

        [Required(ErrorMessage = "Skill Name is Required")]
        public string SkillName { get; set; }
    }
}