using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class EducationDTO
    {
        public int EducationId
        {
            get; set;
        }

        public string EducationName
        {
            get; set;
        }

        public string Major
        {
            get; set;
        }

        public string Degree
        {
            get; set;
        }

        public float GPA
        {
            get; set;
        }

        public DateTime TahunMasuk
        {
            get; set;
        }

        public DateTime TahunLulus
        {
            get; set;
        }

        public string CandidateId
        {
            get; set;
        }
    }
}