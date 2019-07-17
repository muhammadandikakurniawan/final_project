using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class CandidateInterviewDTO
    {
        public string Name { set; get; }
        public string AppliedPosition { set; get; }
        public string SuitablePosition { set; get; }
        public string Source { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string PraSelectionPIC { set; get; }
        public string CallerPIC { set; get; }
        public string CallerNotes { set; get; }
        public string Availablity { set; get; }
        public string ExpectedSalary { set; get; }
        public string SkillsSet { set; get; }
        public string InterviewNotes { set; get; }
        public string InterviewDate { set; get; }
        public string State { set; get; }
    }
}