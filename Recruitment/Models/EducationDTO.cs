using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "School/University Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "School / University min 3 char max 50 char")]
        public string EducationName
        {
            get; set;
        }

        [Required(ErrorMessage = "Major Is Required")]
        [RegularExpression("(.){3,30}", ErrorMessage = "Major min 3 char max 30 char")]
        public string Major
        {
            get; set;
        }

        [Required(ErrorMessage = "Degree Is Required")]
        [RegularExpression("(.){2,10}", ErrorMessage = "Degree min 2 char max 10 char")]
        public string Degree
        {
            get; set;
        }

        [Required(ErrorMessage = "GPA Is Required")]
        public float GPA
        {
            get; set;
        }

        [Required(ErrorMessage = "Start Date Is Required")]
        public DateTime TahunMasuk
        {
            get; set;
        }

        [Required(ErrorMessage = "End Date Is Required")]
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