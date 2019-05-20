using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class CandidateDTO
    {
        private HttpPostedFileBase gambarFile;
        private HttpPostedFileBase cvFile;
        
        public string CandidateId
        {
            get; set;
        }

        [Required(ErrorMessage = "Full Name Is Required")]
        public string NamaLengkap
        {
            get; set;
        }

        [Required(ErrorMessage = "Call Name Is Required")]
        public string NamaPanggilan
        {
            get; set;
        }

        [Required(ErrorMessage = "Gender Is Required")]
        public string JenisKelamin
        {
            get; set;
        }

        [Required(ErrorMessage = "Place Of Birth Is Required")]
        public string TempatLahir
        {
            get; set;
        }

        [Required(ErrorMessage = "Date Of Birth Is Required")]
        public DateTime TanggalLahir
        {
            get; set;
        }

        [Required(ErrorMessage = "Religion Is Required")]
        public string Agama
        {
            get; set;
        }

        [Required(ErrorMessage = "Marital Status Is Required")]
        public string StatusPerkawinan
        {
            get; set;
        }

        [Required(ErrorMessage = "Address Is Required")]
        public string AlamatRumah
        {
            get; set;
        }

        [RegularExpression("-|[A-Z]|[0-9]", ErrorMessage ="Address Must Char or number or - ")]
        public string AlamatOrtu
        {
            get; set;
        }


        public string TelpRumah
        {
            get; set;
        }

        [Required(ErrorMessage = "Email Is Required")]
        public string Email
        {
            get; set;
        }

        [Required(ErrorMessage = "Phone Number Is Required")]
        public string NoHP
        {
            get; set;
        }

        [RegularExpression("-|[0-9]", ErrorMessage ="Resident Card Must Number Or -")]
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

        [Required(ErrorMessage = "Zip Code Is Required")]
        public int KodePos
        {
            get; set;
        }

        [Required(ErrorMessage = "Expected Salary Is Required")]
        public int ExpectedSalary
        {
            get; set;
        }

        [Required(ErrorMessage = "Position Is Required")]
        public string JudulPosisi
        {
            get; set;
        }

        [Required(ErrorMessage = "Notes Is Required")]
        public string Catatan
        {
            get; set;
        }


        public string StateId
        {
            get; set;
        }

        [Required(ErrorMessage = "Source Is Required")]
        public string SourceId
        {
            get; set;
        }

        public string Referer
        {
            get; set;
        }

        [RegularExpression("-|[0-9]", ErrorMessage = "NPWP Must Number Or -")]
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

        public HttpPostedFileBase GambarFile { get => gambarFile; set => gambarFile = value; }
        public HttpPostedFileBase CvFile { get => cvFile; set => cvFile = value; }
    }
}