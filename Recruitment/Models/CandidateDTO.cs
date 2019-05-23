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
        [RegularExpression("(.){3,50}", ErrorMessage = "Full Name min 3 char max 50 char")]
        public string NamaLengkap
        {
            get; set;
        }

        [Required(ErrorMessage = "Call Name Is Required")]
        [RegularExpression("(.){3,20}", ErrorMessage = "Call Name min 3 char max 20 char")]
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
        [RegularExpression("(.){3,30}", ErrorMessage = "Place Of Birth min 3 char max 30 char")]
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
        [RegularExpression("(.){3,100}", ErrorMessage = "Full Name min 3 char max 100 char")]
        public string AlamatRumah
        {
            get; set;
        }

        [RegularExpression("(.*){1,100}", ErrorMessage ="If dont have use (-)")]
        public string AlamatOrtu
        {
            get; set;
        }

        [RegularExpression("-|[0-9]{11,12}", ErrorMessage = "Telephone min 11 max 12 Or -")]
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
        [RegularExpression("-|[0-9]{11,14}", ErrorMessage = "Phone Number min 11 max 14 Or -")]
        public string NoHP
        {
            get; set;
        }
        
        [RegularExpression("-|[0-9]{16}", ErrorMessage ="Resident Card Must 16 Char Or -")]
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
        [RegularExpression("-|[0-9]{5}", ErrorMessage = "Zip Code 5 Char")]
        public int KodePos
        {
            get; set;
        }

        [Required(ErrorMessage = "Expected Salary Is Required")]
        [RegularExpression("-|[0-9]{1,11}", ErrorMessage = "Expected Salary Max 11 Digit Or -")]
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
        [RegularExpression("(.){1,100}", ErrorMessage = "If dont have use (-)")]
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

        [RegularExpression("(.){1,25}", ErrorMessage = "If dont have use (-)")]
        public string Referer
        {
            get; set;
        }

        [RegularExpression("-|[0-9]{15}", ErrorMessage = "NPWP Must 15 Char Or -")]
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


        [AllowExtensions(Extensions = "png,jpg,jpeg", ErrorMessage ="Supported Files Only .png | .jpeg | .jpg")]
        public HttpPostedFileBase GambarFile { get => gambarFile; set => gambarFile = value; }

        [AllowExtensions(Extensions = "docx,pdf", ErrorMessage = "Supported Files Only .docx |.pdf")]
        public HttpPostedFileBase CvFile { get => cvFile; set => cvFile = value; }
    }
}