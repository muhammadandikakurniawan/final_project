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

        [Required(ErrorMessage = "Nama Lengkap Harus Diisi")]
        public string NamaLengkap
        {
            get; set;
        }

        [Required(ErrorMessage = "Nama Panggilan Harus Diisi")]
        public string NamaPanggilan
        {
            get; set;
        }

        [Required(ErrorMessage = "Jenis Kelamin Harus Diisi")]
        public string JenisKelamin
        {
            get; set;
        }

        [Required(ErrorMessage = "Tempat Lahir Harus Diisi")]
        public string TempatLahir
        {
            get; set;
        }

        [Required(ErrorMessage = "Tanggal Lahir Harus Diisi")]
        public DateTime TanggalLahir
        {
            get; set;
        }

        [Required(ErrorMessage = "Agama Harus Diisi")]
        public string Agama
        {
            get; set;
        }

        [Required(ErrorMessage = "Status Harus Diisi")]
        public string StatusPerkawinan
        {
            get; set;
        }

        [Required(ErrorMessage = "Alamat Harus Diisi")]
        public string AlamatRumah
        {
            get; set;
        }

        [Required(ErrorMessage = "Alamat Orang Tua Harus Diisi")]
        public string AlamatOrtu
        {
            get; set;
        }

        [Required(ErrorMessage = "Telephone Harus Diisi")]
        public string TelpRumah
        {
            get; set;
        }

        [Required(ErrorMessage = "Email Harus Diisi")]
        public string Email
        {
            get; set;
        }

        [Required(ErrorMessage = "No Hp Harus Diisi")]
        public string NoHP
        {
            get; set;
        }

        [Required(ErrorMessage = "KTP Harus Diisi")]
        [RegularExpression(@"([0-9]{16})", ErrorMessage = "No KTP tidak valid")]
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

        [Required(ErrorMessage = "Kode Pos Harus Diisi")]
        public int KodePos
        {
            get; set;
        }

        [Required(ErrorMessage = "Expected Salary Harus Diisi")]
        public int ExpectedSalary
        {
            get; set;
        }

        [Required(ErrorMessage = "Posisi Harus Diisi")]
        public string JudulPosisi
        {
            get; set;
        }

        [Required(ErrorMessage = "Catatan Harus Diisi")]
        public string Catatan
        {
            get; set;
        }

        //[Required(ErrorMessage = "State Harus Diisi")]
        public string StateId
        {
            get; set;
        }

        [Required(ErrorMessage = "Source Harus Diisi")]
        public string SourceId
        {
            get; set;
        }

        public string Referer
        {
            get; set;
        }

        [Required(ErrorMessage = "NPWP Harus Diisi")]
        //[RegularExpression(@"([0-9]{15})", ErrorMessage = "NPWP tidak valid")]
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