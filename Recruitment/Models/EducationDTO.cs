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
        public string EducationName
        {
            get; set;
        }

        [Required(ErrorMessage = "Major Is Required")]
        public string Major
        {
            get; set;
        }

        [Required(ErrorMessage = "Degree Harus Diisi")]
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