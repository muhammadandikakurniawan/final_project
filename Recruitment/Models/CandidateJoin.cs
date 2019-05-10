using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Recruitment.Models;

namespace Recruitment.Models
{
    public class CandidateJoin
    {
        public CandidateDTO CandidateDetails { get; set; }
        public Sumber SumberDetails { get; set; }
        public StateDTO StateDetails { get; set; }
        public ExperienceDTO Experience { get; set; }
        public EducationDTO Education { get; set; }
    }
}