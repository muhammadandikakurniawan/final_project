﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class Candidate_Skill
    {
        
        public long SkillId { get; set; }
        public string CandidateId { get; set; }
        public string NameSkill { get; set; }
        
    }
}