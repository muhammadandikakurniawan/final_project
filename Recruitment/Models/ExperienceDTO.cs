using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Company Harus Diisi")]
        public string ExperienceName
        {
            get; set;
        }

        [Required(ErrorMessage = "Industri Harus Diisi")]
        public string Industri
        {
            get; set;
        }

        [Required(ErrorMessage = "Posisi Harus Diisi")]
        public string Posisi
        {
            get; set;
        }

        [Required(ErrorMessage = "Start Date Harus Diisi")]
        public DateTime StartDate
        {
            get; set;
        }

        [Required(ErrorMessage = "End Date Harus Diisi")]
        public DateTime EndDate
        {
            get; set;
        }

        [Required(ErrorMessage = "Job Desc Harus Diisi")]
        public string JobDesc
        {
            get; set;
        }

        //[Required(ErrorMessage = "Skill Harus Diisi")]
        public string Skill
        {
            get; set;
        }

        [Required(ErrorMessage = "Salary Harus Diisi")]
        public int Salary
        {
            get; set;
        }

        [Required(ErrorMessage = "Project Harus Diisi")]
        public string Project
        {
            get; set;
        }

        //[Required(ErrorMessage = "Benefit Harus Diisi")]
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