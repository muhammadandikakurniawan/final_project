using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class SkillAndList
    {
        public IEnumerable<Candidate_Skill> ListSkill {get;set;}
        public Candidate_Skill suram { get; set; }
    }
}