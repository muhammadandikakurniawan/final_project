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

        [Required(ErrorMessage = "School/University Harus Diisi")]
        public string EducationName
        {
            get; set;
        }

        [Required(ErrorMessage = "Major Harus Diisi")]
        public string Major
        {
            get; set;
        }

        [Required(ErrorMessage = "Degree Harus Diisi")]
        public string Degree
        {
            get; set;
        }

        [Required(ErrorMessage = "GPA Harus Diisi")]
        public float GPA
        {
            get; set;
        }

        [Required(ErrorMessage = "Tahun Masuk Harus Diisi")]
        public DateTime TahunMasuk
        {
            get; set;
        }

        [Required(ErrorMessage = "Tahun Lulus Harus Diisi")]
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