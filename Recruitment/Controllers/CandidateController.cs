using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using PagedList;

namespace Recruitment.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        public ActionResult Index(string searchString, string searchState, string searchPosition, int? page)
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<CandidateDTO> candidates = db.CANDIDATEs.Select(e => new CandidateDTO
                {
                    CandidateId = e.CANDIDATE_ID,
                    NamaLengkap = e.NAMA_LENGKAP,
                    NamaPanggilan = e.NAMA_PANGGILAN,
                    JenisKelamin = e.KD_JENIS_KELAMIN,
                    TempatLahir = e.TEMPAT_LAHIR,
                    TanggalLahir = e.TANGGAL_LAHIR,
                    Agama = e.AGAMA,
                    StatusPerkawinan = e.STATUS_PERKAWINAN,
                    AlamatRumah = e.ALAMAT_RUMAH,
                    AlamatOrtu = e.ALAMAT_ORTU,
                    TelpRumah = e.TELP_RUMAH,
                    Email = e.EMAIL,
                    NoHP = e.NOHP,
                    NoKTP = e.NO_KTP,
                    IsDeleted = e.IS_DELETED,
                    Foto = e.FOTO,
                    UserCreate = e.USER_CREATE,
                    DateTimeCreate = e.DATETIME_CREATE,
                    UserUpdate = e.USER_UPDATE,
                    DateTimeUpdate = e.DATETIME_UPDATE,
                    KodePos = e.KODE_POS,
                    ExpectedSalary = e.EXPECTED_SALARY,
                    JudulPosisi = e.JUDUL_POSISI,
                    Catatan = e.CATATAN,
                    StateId = e.STATE_ID,
                    SourceId = e.SOURCE_ID,
                    Referer = e.REFERER,
                    NPWP = e.NPWP,
                    CV = e.CV,
                    AvailableJoin = (DateTime)e.AVAIABLE_JOIN
                }).ToList();
                List<Sumber> sumbers = db.SOURCEs.Select(e => new Sumber
                {
                    SumberId = e.SOURCE_ID,
                    SumberNama = e.SOURCE_NAME
                }).ToList();

                List<StateDTO> states = db.STATEs.Select(e => new StateDTO
                {
                    StateId = e.STATE_ID,
                    StateName = e.STATE_NAME,
                    StateNext = e.STATE_NEXT
                }).ToList();

                var join = from c in candidates
                           join s in sumbers on c.SourceId equals s.SumberId into table1
                           from s in table1.DefaultIfEmpty()
                           join st in states on c.StateId equals st.StateId into table2
                           from st in table2.DefaultIfEmpty()
                           select new CandidateJoin { CandidateDetails = c, SumberDetails = s, StateDetails = st };
                List<string> position = db.POSITIONs.Select(e => e.POSITION_NAME).ToList();
                position.Add("");
                TempData["position"] = position;
                List<string> state = db.STATEs.Select(e => e.STATE_NAME).ToList();
                state.Add("");
                TempData["state"] = state;
                if (!string.IsNullOrEmpty(searchString))
                {
                    join = join.Where(x => x.CandidateDetails.NamaLengkap.ToLower().Contains(searchString.ToLower()) || x.CandidateDetails.JudulPosisi.ToLower().Contains(searchString.ToLower())
                    || x.SumberDetails.SumberNama.ToLower().Contains(searchString.ToLower()) || x.CandidateDetails.NoHP.ToString().Contains(searchString.ToLower()) || x.CandidateDetails.Email.ToLower().Contains(searchString.ToLower()) ||
                    x.CandidateDetails.UserCreate.ToLower().Contains(searchString.ToLower()) || x.CandidateDetails.DateTimeCreate.ToString().Contains(searchString.ToLower()) || x.StateDetails.StateName.ToLower().Contains(searchString.ToLower()) ||
                    x.CandidateDetails.Catatan.ToLower().Contains(searchString.ToLower())
                    );
                    page = 1;
                }
                

                if (!string.IsNullOrEmpty(searchPosition))
                {
                    join = join.Where(x => x.CandidateDetails.JudulPosisi == searchPosition);
                    page = 1;
                }
                else if (!string.IsNullOrEmpty(searchState))
                {
                    join = join.Where(x => x.StateDetails.StateName== searchState);
                    page = 1;
                }
                else if (!string.IsNullOrEmpty(searchPosition) && !string.IsNullOrEmpty(searchState))
                {
                    join = join.Where(x => x.CandidateDetails.JudulPosisi == searchPosition && x.StateDetails.StateName == searchState);
                    page = 1;
                }

                if (page == null)
                {
                    page = 1;
                }
                int pageSize = 2; 
                int pageNumber = (page ?? 1);

                return View("Index", join.ToPagedList(pageNumber,pageSize));
            }
            
        }

        [ActionName("add")]
        public ActionResult AddingCandidate()
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<CandidateDTO> candidates = db.CANDIDATEs.Select(m => 
                new CandidateDTO {
                    CandidateId = m.CANDIDATE_ID }
                ).ToList();
                string last = candidates.Select(m => m.CandidateId).LastOrDefault();
                int count = 0;
                if (candidates.Count != 0)
                {
                    count = int.Parse(last.Substring(last.Length - 5)) + 1;
                }
                string id = "CA" + String.Format("{0:D5}", count);
                List<string> max = new List<string>();
                max.Add(id);
                TempData["maxid"] = max;
                
                return View("FormCandidate");
            }
                
        }

        [ActionName("Insert")]
        public ActionResult NewCandidate(CandidateDTO newCandidate)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Source");
            //}
            //List<CandidateDTO> candidates = ListCandidate();
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                //string last = candidates.Select(m => m.CandidateId).LastOrDefault();
                //int count = 0;
                //if (candidates.Count != 0)
                //{
                //    count = int.Parse(last.Substring(last.Length - 5)) + 1;
                //}
                //string id = "CA" + String.Format("{0:D5}", count);
                CANDIDATE candidate = new CANDIDATE
                {
                    CANDIDATE_ID = newCandidate.CandidateId,
                    NAMA_LENGKAP = newCandidate.NamaLengkap,
                    NAMA_PANGGILAN = newCandidate.NamaPanggilan,
                    KD_JENIS_KELAMIN = newCandidate.JenisKelamin,
                    TEMPAT_LAHIR = newCandidate.TempatLahir,
                    TANGGAL_LAHIR = newCandidate.TanggalLahir,
                    AGAMA = newCandidate.Agama,
                    STATUS_PERKAWINAN = newCandidate.StatusPerkawinan,
                    ALAMAT_RUMAH = newCandidate.AlamatRumah,
                    ALAMAT_ORTU = newCandidate.AlamatOrtu,
                    TELP_RUMAH = newCandidate.TelpRumah,
                    EMAIL = newCandidate.Email,
                    NOHP = newCandidate.NoHP,
                    NO_KTP = newCandidate.NoKTP,
                    IS_DELETED = 0,
                    FOTO = newCandidate.Foto,
                    USER_CREATE = (string)Session["username"],
                    DATETIME_CREATE = DateTime.Now,
                    USER_UPDATE = (string)Session["username"],
                    DATETIME_UPDATE = DateTime.Now,
                    KODE_POS = newCandidate.KodePos,
                    EXPECTED_SALARY = newCandidate.ExpectedSalary,
                    JUDUL_POSISI = newCandidate.JudulPosisi,
                    CATATAN = newCandidate.Catatan,
                    STATE_ID = "ST000",
                    SOURCE_ID = newCandidate.SourceId,
                    REFERER = newCandidate.Referer,
                    NPWP = newCandidate.NPWP,
                    CV = newCandidate.CV,
                    AVAIABLE_JOIN = newCandidate.AvailableJoin
                };
                db.CANDIDATEs.Add(candidate);
                db.SaveChanges();
                TempData["status"] = "Data Berhasil Masuk";
                return Redirect("~/candidate");
            }
        }

        [Route("{id}/edit")]
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult EditCandidate(string id)
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                CANDIDATE candidate = db.CANDIDATEs.Find(id);
                CandidateDTO candidateDTO = new CandidateDTO
                {
                    CandidateId = candidate.CANDIDATE_ID,
                    NamaLengkap = candidate.NAMA_LENGKAP,
                    NamaPanggilan = candidate.NAMA_PANGGILAN,
                    JenisKelamin = candidate.KD_JENIS_KELAMIN,
                    TempatLahir = candidate.TEMPAT_LAHIR,
                    TanggalLahir = candidate.TANGGAL_LAHIR,
                    Agama = candidate.AGAMA,
                    StatusPerkawinan = candidate.STATUS_PERKAWINAN,
                    AlamatRumah = candidate.ALAMAT_RUMAH,
                    AlamatOrtu = candidate.ALAMAT_ORTU,
                    TelpRumah = candidate.TELP_RUMAH,
                    Email = candidate.EMAIL,
                    NoHP = candidate.NOHP,
                    NoKTP = candidate.NO_KTP,
                    IsDeleted = candidate.IS_DELETED,
                    Foto = candidate.FOTO,
                    UserCreate = candidate.USER_CREATE,
                    DateTimeCreate = candidate.DATETIME_CREATE,
                    UserUpdate = candidate.USER_UPDATE,
                    DateTimeUpdate = candidate.DATETIME_UPDATE,
                    KodePos = candidate.KODE_POS,
                    ExpectedSalary = candidate.EXPECTED_SALARY,
                    JudulPosisi = candidate.JUDUL_POSISI,
                    Catatan = candidate.CATATAN,
                    StateId = candidate.STATE_ID,
                    SourceId = candidate.SOURCE_ID,
                    Referer = candidate.REFERER,
                    NPWP = candidate.NPWP,
                    CV = candidate.CV,
                    AvailableJoin = (DateTime)candidate.AVAIABLE_JOIN
                };
                TempData["candidateEdit"] = candidateDTO;
                return View("EditCandidate", candidateDTO);
            }
        }

        [Route("edit")]
        [ActionName("Update")]
        [HttpPost]
        public ActionResult Update(CandidateDTO edittedCandidate)
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                CandidateDTO temp = (CandidateDTO)TempData.Peek("candidateEdit");
                CANDIDATE candidate = new CANDIDATE
                {
                    CANDIDATE_ID = temp.CandidateId,
                    NAMA_LENGKAP = edittedCandidate.NamaLengkap,
                    NAMA_PANGGILAN = edittedCandidate.NamaPanggilan,
                    KD_JENIS_KELAMIN = edittedCandidate.JenisKelamin,
                    TEMPAT_LAHIR = edittedCandidate.TempatLahir,
                    TANGGAL_LAHIR = edittedCandidate.TanggalLahir,
                    AGAMA = edittedCandidate.Agama,
                    STATUS_PERKAWINAN = edittedCandidate.StatusPerkawinan,
                    ALAMAT_RUMAH = edittedCandidate.AlamatRumah,
                    ALAMAT_ORTU = edittedCandidate.AlamatOrtu,
                    TELP_RUMAH = edittedCandidate.TelpRumah,
                    EMAIL = edittedCandidate.Email,
                    NOHP = edittedCandidate.NoHP,
                    NO_KTP = edittedCandidate.NoKTP,
                    IS_DELETED = 0,
                    FOTO = edittedCandidate.Foto,
                    USER_CREATE = temp.UserCreate,
                    DATETIME_CREATE = temp.DateTimeCreate,
                    USER_UPDATE = (string)Session["username"],
                    DATETIME_UPDATE = DateTime.Now,
                    KODE_POS = edittedCandidate.KodePos,
                    EXPECTED_SALARY = edittedCandidate.ExpectedSalary,
                    JUDUL_POSISI = edittedCandidate.JudulPosisi,
                    CATATAN = edittedCandidate.Catatan,
                    STATE_ID = edittedCandidate.StateId,
                    SOURCE_ID = edittedCandidate.SourceId,
                    REFERER = edittedCandidate.Referer,
                    NPWP = edittedCandidate.NPWP,
                    CV = edittedCandidate.CV,
                    AVAIABLE_JOIN = null
                };
                db.Entry(candidate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Redirect("~/candidate");
            }
        }

        private List<CandidateDTO> CandidateFilter(string filterStateId, string filterPositionId)
        {
            List<CandidateDTO> candidates = ListCandidate();
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                if (filterStateId == "None")
                {
                    if (filterPositionId == "None")
                    {
                        return candidates;
                    }
                    else
                    {
                        List<CandidateDTO> candidateDTOsFiltered = candidates.Where(m => m.JudulPosisi == filterPositionId).ToList();
                        return candidateDTOsFiltered;
                    }
                }
                else if (filterStateId != "None")
                {
                    if (filterPositionId != "None")
                    {
                        List<CandidateDTO> candidateDTOsFiltered = candidates.Where(m => m.JudulPosisi == filterPositionId && m.StateId == filterStateId).ToList();
                        return candidateDTOsFiltered;
                    }
                    else
                    {
                        List<CandidateDTO> candidateDTOsFiltered = candidates.Where(m => m.StateId == filterStateId).ToList();
                        return candidateDTOsFiltered;
                    }
                }

            }
            return candidates;

        }

        private List<CandidateDTO> ListCandidate()
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<CandidateDTO> candidatesAll = db.CANDIDATEs.Select(m => new CandidateDTO
                {
                    CandidateId = m.CANDIDATE_ID,
                    NamaLengkap = m.NAMA_LENGKAP,
                    NamaPanggilan = m.NAMA_PANGGILAN,
                    JenisKelamin = m.KD_JENIS_KELAMIN,
                    TempatLahir = m.TEMPAT_LAHIR,
                    TanggalLahir = m.TANGGAL_LAHIR,
                    Agama = m.AGAMA,
                    StatusPerkawinan = m.STATUS_PERKAWINAN,
                    AlamatRumah = m.ALAMAT_RUMAH,
                    AlamatOrtu = m.ALAMAT_ORTU,
                    TelpRumah = m.TELP_RUMAH,
                    Email = m.EMAIL,
                    NoHP = m.NOHP,
                    NoKTP = m.NO_KTP,
                    IsDeleted = m.IS_DELETED,
                    Foto = m.FOTO,
                    UserCreate = m.USER_CREATE,
                    DateTimeCreate = m.DATETIME_CREATE,
                    UserUpdate = m.USER_UPDATE,
                    DateTimeUpdate = m.DATETIME_UPDATE,
                    KodePos = m.KODE_POS,
                    ExpectedSalary = m.EXPECTED_SALARY,
                    JudulPosisi = m.JUDUL_POSISI,
                    Catatan = m.CATATAN,
                    StateId = m.STATE_ID,
                    SourceId = m.SOURCE_ID,
                    Referer = m.REFERER,
                    NPWP = m.NPWP,
                    CV = m.CV,
                    AvailableJoin = (DateTime)m.AVAIABLE_JOIN
                }).ToList();
                List<CandidateDTO> candidates = candidatesAll.Where(m => m.IsDeleted != 1).ToList();
                return candidates;
            }
        }

    }
}