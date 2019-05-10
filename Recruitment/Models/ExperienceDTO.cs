using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class ExperienceDTO
    {
        public int ExperienceId
        {
            get; set;
        }

        public string ExperienceName
        {
            get; set;
        }

        public string Industri
        {
            get; set;
        }

        public string Posisi
        {
            get; set;
        }

        public DateTime StartDate
        {
            get; set;
        }

        public DateTime EndDate
        {
            get; set;
        }

        public string JobDesc
        {
            get; set;
        }

        public string Skill
        {
            get; set;
        }

        public int Salary
        {
            get; set;
        }

        public string Project
        {
            get; set;
        }

        public string Benefit
        {
            get; set;
        }

        public string CandidateId
        {
            get; set;
        }
    }
}