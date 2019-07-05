using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class CallModelProses
    {

        public string CandidateId
        {
            get; set;
        }
        [Required(ErrorMessage = "Full Name Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Full Name min 3 char max 50 char")]
        public string NamaLengkap
        {
            get; set;
        }
        [Required(ErrorMessage = "Nick Name Is Required")]
        [RegularExpression("(.){3,20}", ErrorMessage = "Nick Name min 3 char max 20 char")]
        public string NamaPanggilan
        {
            get; set;
        }

        public string JenisKelamin
        {
            get; set;
        }
        [Required(ErrorMessage = "Place of Birth Is Required")]
        [RegularExpression("(.){3,30}", ErrorMessage = "Place of Birth min 3 char max 30 char")]
        public string TempatLahir
        {
            get; set;
        }

        public DateTime TanggalLahir
        {
            get; set;
        }
        [Required(ErrorMessage = "Religion Is Required")]
        [RegularExpression("(.){3,25}", ErrorMessage = "Religion min 3 char max 25 char")]
        public string Agama
        {
            get; set;
        }
        [Required(ErrorMessage = "Marital Status Is Required")]
        [RegularExpression("(.){3,11}", ErrorMessage = "Marital Status min 3 char max 11 char")]
        public string StatusPerkawinan
        {
            get; set;
        }
        [Required(ErrorMessage = "Address Is Required")]
        [RegularExpression("(.){3,100}", ErrorMessage = "Address min 3 char max 100 char")]
        public string AlamatRumah
        {
            get; set;
        }
        [Required(ErrorMessage = "Parent's Address Is Required")]
        [RegularExpression("(.){3,100}", ErrorMessage = "Parent's Address min 3 char max 100 char")]
        public string AlamatOrtu
        {
            get; set;
        }
        [Required(ErrorMessage = "Telephone Numbers Is Required")]
        [RegularExpression("(.){3,15}", ErrorMessage = "Telephone Numbers min 3 char max 15 char")]
        public string TelpRumah
        {
            get; set;
        }
        [Required(ErrorMessage = "Email Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Email min 3 char max 50 char")]
        public string Email
        {
            get; set;
        }
        [Required(ErrorMessage = "Phone Numbers Is Required")]
        [RegularExpression("(.){3,15}", ErrorMessage = "Phone Numbers min 3 char max 15 char")]
        public string NoHP
        {
            get; set;
        }
        [Required(ErrorMessage = "ID Card Numbers Is Required")]
        [RegularExpression("(.){3,16}", ErrorMessage = "ID Card Numbers min 3 char max 16 char")]
        public string NoKTP
        {
            get; set;
        }

        public int IsDeleted
        {
            get; set;
        }

        public string Foto
        {
            get; set;
        }

        public string UserCreate
        {
            get; set;
        }

        public DateTime DateTimeCreate
        {
            get; set;
        }

        public string UserUpdate
        {
            get; set;
        }

        public DateTime DateTimeUpdate
        {
            get; set;
        }
        [Required(ErrorMessage = "Postal Code Is Required")]
        [RegularExpression("(.){3,15}", ErrorMessage = "Postal Code min 3 char max 15 char")]
        public int KodePos
        {
            get; set;
        }
        [Required(ErrorMessage = "Expected Salary Is Required")]
        [RegularExpression("(.){3,15}", ErrorMessage = "Expected Salary min 3 char max 15 char")]
        public int ExpectedSalary
        {
            get; set;
        }
        [Required(ErrorMessage = "Position's Title Is Required")]
        [RegularExpression("(.){3,50}", ErrorMessage = "Position's Title min 3 char max 50 char")]
        public string JudulPosisi
        {
            get; set;
        }

        public string Catatan
        {
            get; set;
        }

        public string StateId
        {
            get; set;
        }

        public string SourceId
        {
            get; set;
        }
        [Required(ErrorMessage = "Referer Is Required")]
        [RegularExpression("(.){3,25}", ErrorMessage = "Referer min 3 char max 25 char")]
        public string Referer
        {
            get; set;
        }
        [Required(ErrorMessage = "NPWP Is Required")]
        [RegularExpression("(.){3,15}", ErrorMessage = "NPWP min 3 char max 15 char")]
        public string NPWP
        {
            get; set;
        }

        public string CV
        {
            get; set;
        }

        public DateTime AvailableJoin
        {
            get; set;
        }

        public int EducationId
        {
            get; set;
        }
        [Required(ErrorMessage = "NPWP Is Required")]
        [RegularExpression("(.){3,15}", ErrorMessage = "NPWP min 3 char max 15 char")]
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

        public string CandidateIdinEdu
        {
            get; set;
        }

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

        public string CandidateIdinExp
        {
            get; set;
        }
    }
}