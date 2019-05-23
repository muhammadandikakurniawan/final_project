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

        [Required(ErrorMessage = "Company Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Company min 3 char max 50 char")]
        public string ExperienceName
        {
            get; set;
        }

        [Required(ErrorMessage = "Industri Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Industri min 3 char max 50 char")]
        public string Industri
        {
            get; set;
        }

        [Required(ErrorMessage = "Posisi Is Required")]
        public string Posisi
        {
            get; set;
        }

        [Required(ErrorMessage = "Start Date Is Required")]
        public DateTime StartDate
        {
            get; set;
        }

        [Required(ErrorMessage = "End Date Is Required")]
        public DateTime EndDate
        {
            get; set;
        }

        [Required(ErrorMessage = "Job Desc Is Required")]
        [RegularExpression("(.){5,200}", ErrorMessage = "Job Desc min 5 char max 200 char")]
        public string JobDesc
        {
            get; set;
        }

        [Required(ErrorMessage = "Skill Is Required")]
        [RegularExpression("(.){1,200}", ErrorMessage = "Skill min 1 char max 200 char")]
        public string Skill
        {
            get; set;
        }

        [Required(ErrorMessage = "Salary Is Required")]
        [RegularExpression("-|[0-9]{1,11}", ErrorMessage = "Salary Max 11 Digit Or -")]
        public int Salary
        {
            get; set;
        }

        [Required(ErrorMessage = "Project Is Required")]
        [RegularExpression("(.){1,100}", ErrorMessage = "Project min 1 char max 100 char")]
        public string Project
        {
            get; set;
        }

        //[Required(ErrorMessage = "Benefit Harus Diisi")]
        [RegularExpression("(.){1,100}", ErrorMessage = "Benefit min 1 char max 100 char")]
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