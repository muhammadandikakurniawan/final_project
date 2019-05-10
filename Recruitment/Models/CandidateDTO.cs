using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruitment.Models
{
    public class CandidateDTO
    {
        private HttpPostedFileBase gambarFile;

        public string CandidateId
        {
            get; set;
        }

        public string NamaLengkap
        {
            get; set;
        }

        public string NamaPanggilan
        {
            get; set;
        }

        public string JenisKelamin
        {
            get; set;
        }

        public string TempatLahir
        {
            get; set;
        }

        public DateTime TanggalLahir
        {
            get; set;
        }

        public string Agama
        {
            get; set;
        }

        public string StatusPerkawinan
        {
            get; set;
        }

        public string AlamatRumah
        {
            get; set;
        }

        public string AlamatOrtu
        {
            get; set;
        }

        public string TelpRumah
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string NoHP
        {
            get; set;
        }

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

        public int KodePos
        {
            get; set;
        }

        public int ExpectedSalary
        {
            get; set;
        }

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

        public string Referer
        {
            get; set;
        }

        public int NPWP
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
    }
}