using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Experience Name")]
        [Required(ErrorMessage = "Experience Name Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Experience min 3 char max 50 char")]
        public string ExperienceName
        {
            get; set;
        }

        [DisplayName("Industry")]
        [Required(ErrorMessage = "Industry Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Industry min 3 char max 50 char")]
        public string Industri
        {
            get; set;
        }

        [DisplayName("Position Job")]
        [Required(ErrorMessage = "Position Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Position min 3 char max 50 char")]
        public string Posisi
        {
            get; set;
        }

        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Start Date Is Required")]
        public DateTime StartDate
        {
            get; set;
        }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "End Date Is Required")]
        public DateTime EndDate
        {
            get; set;
        }

        [DisplayName("Job Description")]
        [Required(ErrorMessage = "Job Description Is Required")]
        [RegularExpression("(.){5,200}", ErrorMessage = "Job Description min 5 char max 200 char")]
        public string JobDesc
        {
            get; set;
        }

        [DisplayName("Skill")]
        [Required(ErrorMessage = "Skill Is Required")]
        [RegularExpression("(.){1,200}", ErrorMessage = "Skill min 1 char max 200 char")]
        public string Skill
        {
            get; set;
        }

        [DisplayName("Salary")]
        [Required(ErrorMessage = "Salary Is Required")]
        [RegularExpression("-|[0-9]{1,11}", ErrorMessage = "Salary Max 11 Digit Or -")]
        public int Salary
        {
            get; set;
        }

        [DisplayName("Project Experience")]
        [Required(ErrorMessage = "Project Is Required")]
        [RegularExpression("(.){1,100}", ErrorMessage = "Project min 1 char max 100 char")]
        public string Project
        {
            get; set;
        }

        //[Required(ErrorMessage = "Benefit Harus Diisi")]
        [DisplayName("Benefit")]
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