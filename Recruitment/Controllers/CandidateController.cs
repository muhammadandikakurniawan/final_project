using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using PagedList;
using System.IO;

namespace Recruitment.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        public ActionResult Index(string searchString, string searchState, string searchPosition, string CurrentFilter, string CurrentPosition, string CurrentState, int? page)
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

                List<PositionPoco> positions = db.POSITIONs.Select(e => new PositionPoco
                {
                    IdPosisi = e.POSITION_ID,
                    Nama = e.POSITION_NAME,
                }).ToList();

                var join = from c in candidates
                           join s in sumbers on c.SourceId equals s.SumberId into table1
                           from s in table1.DefaultIfEmpty()
                           join st in states on c.StateId equals st.StateId into table2
                           from st in table2.DefaultIfEmpty()
                           join p in positions on c.JudulPosisi equals p.IdPosisi into table3
                           from p in table3.DefaultIfEmpty()
                           where c.StateId == "ST000" || c.StateId == "ST001" && c.IsDeleted == 0
                           select new CandidateJoin { CandidateDetails = c, SumberDetails = s, StateDetails = st, Position = p };
                List<string> position = db.POSITIONs.Select(e => e.POSITION_NAME).ToList();
                TempData["position"] = position;
                List<string> state = db.STATEs.Select(e => e.STATE_NAME).ToList();
                TempData["state"] = state;

                if (searchString != null || searchPosition != null || searchState != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = CurrentFilter;
                    searchPosition = CurrentPosition;
                    searchState = CurrentState;
                }

                ViewBag.CurrentFilter = searchString;
                ViewBag.CurrentPosition = searchPosition;
                ViewBag.CurrentState = searchState;

                if (!string.IsNullOrEmpty(searchString))
                {
                    join = join = join.Where(x => x.CandidateDetails.NamaLengkap.ToLower().Contains(searchString.ToLower()) || x.Position.Nama.ToLower().Contains(searchString.ToLower())
                    || x.SumberDetails.SumberNama.ToLower().Contains(searchString.ToLower()) || x.CandidateDetails.NoHP.ToString().Contains(searchString.ToLower()) || x.CandidateDetails.Email.ToLower().Contains(searchString.ToLower()) ||
                    x.CandidateDetails.UserCreate.ToLower().Contains(searchString.ToLower()) || x.CandidateDetails.DateTimeCreate.ToString().Contains(searchString.ToLower()) || x.StateDetails.StateName.ToLower().Contains(searchString.ToLower()) ||
                    x.CandidateDetails.Catatan.ToLower().Contains(searchString.ToLower())
                    );
                }

                if (!string.IsNullOrEmpty(searchState) && !string.IsNullOrEmpty(searchPosition))
                {
                    join = join.Where(x => x.Position.Nama == searchPosition && x.StateDetails.StateName == searchState);
                }
                else if (!string.IsNullOrEmpty(searchState))
                {
                    join = join.Where(x => x.StateDetails.StateName == searchState);
                }
                else if (!string.IsNullOrEmpty(searchPosition))
                {
                    join = join.Where(x => x.Position.Nama == searchPosition);
                }

                int pageSize = 5;
                int pageNumber = (page ?? 1);

                return View("Index", join.ToPagedList(pageNumber, pageSize));
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

        //[Route("candidate/{id}")]
        [ActionName("getDetails")]
        [HttpGet]
        public ActionResult getDataCandidate(string id)
        {
            try
            {
                using (RecruitmentEntities db = new RecruitmentEntities())
                {
                    
                    List<CandidateDTO> cn = db.CANDIDATEs.Where(m => m.CANDIDATE_ID == id).Select(e => new CandidateDTO
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
                    List<PositionPoco> positions = db.POSITIONs.Select(e => new PositionPoco { IdPosisi = e.POSITION_ID, Nama = e.POSITION_NAME }).ToList();
                    var join = from c in cn
                               join p in positions on c.JudulPosisi equals p.IdPosisi into table1
                               from p in table1.DefaultIfEmpty()
                               select new CandidateJoin { CandidateDetails = c, Position = p };

                    List<string> candidate = new List<string>();
                    candidate.Add(id);
                    TempData["candidateID"] = candidate;
                    return View("DataCandidate", join);
                }
            }
            catch (Exception err)
            {
                Session["user"] = null;
                Session["menu"] = null;
                Session["username"] = null;
                Session["name"] = null;
                return Redirect("~/Login");
            }
        }

        [ActionName("getExperience")]
        [HttpGet]
        public ActionResult getExperience(string id)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                List<EXPERIENCE> eXPERIENCEs = re.EXPERIENCEs.Where(m => m.CANDIDATE_ID == id).ToList();
                List<ExperienceDTO> ex = eXPERIENCEs.Select(e =>
                new ExperienceDTO
                {

                    ExperienceId = e.EXPERIENCE_ID,
                    ExperienceName = e.EXPERIENCE_NAME,
                    Industri = e.INDUSTRI,
                    Posisi = e.POSISI,
                    StartDate = (DateTime)e.START_DATE,
                    EndDate = (DateTime)e.END_DATE,
                    Salary = (int)e.SALARY,
                    JobDesc = e.JOB_DESC,
                    Skill = e.SKILL,
                    Project = e.PROJECT,
                    Benefit = e.BENEFIT,
                    CandidateId = e.CANDIDATE_ID
                }).ToList();
                List<PositionPoco> positions = re.POSITIONs.Select(e => new PositionPoco
                {
                    IdPosisi = e.POSITION_ID,
                    Nama = e.POSITION_NAME
                }).ToList();

                var join = from x in ex
                           join p in positions on x.Posisi equals p.IdPosisi
                           select new CandidateJoin { Experience = x, Position = p };

                return View("JobExperience", join);
            }
        }

        [ActionName("getEducation")]
        [HttpGet]
        public ActionResult EducationList(string id)
        {
            using (RecruitmentEntities ent = new RecruitmentEntities())
            {
                List<EDUCATION> education = ent.EDUCATIONs.Where(m => m.CANDIDATE_ID == id).ToList();
                List<EducationDTO> ex = education.Select(x => new EducationDTO
                {
                    EducationId = x.EDUCATION_ID,
                    EducationName = x.EDUCATION_NAME,
                    TahunMasuk = (DateTime)x.TAHUN_MASUK,
                    GPA = (int)x.GPA,
                    Degree = x.DEGREE,
                    Major = x.MAJOR,
                    TahunLulus = (DateTime)x.TAHUN_MASUK,
                    CandidateId = x.CANDIDATE_ID
                }).ToList();
                List<string> Education = new List<string>();
                Education.Add(id);
                TempData["candidateID"] = Education;
                return View("EducationList", ex);
            }
        }

        [ActionName("Insert")]
        [HttpPost]
        public ActionResult NewCandidate(CandidateJoin newCandidate)
        {
            if (ModelState.IsValid)
            {
                if (newCandidate.CandidateDetails.GambarFile != null)
                {
                    string fileNameFoto = newCandidate.CandidateDetails.NamaLengkap;
                    string extensionFoto = Path.GetExtension(newCandidate.CandidateDetails.GambarFile.FileName);
                    fileNameFoto = fileNameFoto + extensionFoto;
                    newCandidate.CandidateDetails.Foto = "~/Images/" + fileNameFoto;
                    fileNameFoto = Path.Combine(Server.MapPath("~/Images/"), fileNameFoto);
                    newCandidate.CandidateDetails.GambarFile.SaveAs(fileNameFoto);
                    
                }else if (newCandidate.CandidateDetails.CvFile != null)
                {
                    string fileNameCV = newCandidate.CandidateDetails.NamaLengkap;
                    string extensionCV = Path.GetExtension(newCandidate.CandidateDetails.CvFile.FileName);
                    fileNameCV = fileNameCV + extensionCV;
                    newCandidate.CandidateDetails.CV = "~/Cv/" + fileNameCV;
                    fileNameCV = Path.Combine(Server.MapPath("~/Cv/"), fileNameCV);
                    newCandidate.CandidateDetails.CvFile.SaveAs(fileNameCV);
                }
                else
                {
                    newCandidate.CandidateDetails.Foto = "~/Images/DEFAULT.png";
                    newCandidate.CandidateDetails.CV = "Cv belum di upload";
                }
                
                using (RecruitmentEntities db = new RecruitmentEntities())
                {
                    CANDIDATE candidate = new CANDIDATE
                    {
                        CANDIDATE_ID = newCandidate.CandidateDetails.CandidateId,
                        NAMA_LENGKAP = newCandidate.CandidateDetails.NamaLengkap,
                        NAMA_PANGGILAN = newCandidate.CandidateDetails.NamaPanggilan,
                        KD_JENIS_KELAMIN = newCandidate.CandidateDetails.JenisKelamin,
                        TEMPAT_LAHIR = newCandidate.CandidateDetails.TempatLahir,
                        TANGGAL_LAHIR = newCandidate.CandidateDetails.TanggalLahir,
                        AGAMA = newCandidate.CandidateDetails.Agama,
                        STATUS_PERKAWINAN = newCandidate.CandidateDetails.StatusPerkawinan,
                        ALAMAT_RUMAH = newCandidate.CandidateDetails.AlamatRumah,
                        ALAMAT_ORTU = newCandidate.CandidateDetails.AlamatOrtu,
                        TELP_RUMAH = newCandidate.CandidateDetails.TelpRumah,
                        EMAIL = newCandidate.CandidateDetails.Email,
                        NOHP = newCandidate.CandidateDetails.NoHP,
                        NO_KTP = newCandidate.CandidateDetails.NoKTP,
                        IS_DELETED = 0,
                        FOTO = newCandidate.CandidateDetails.Foto,
                        USER_CREATE = (string)Session["name"],
                        DATETIME_CREATE = DateTime.Now,
                        USER_UPDATE = (string)Session["name"],
                        DATETIME_UPDATE = DateTime.Now,
                        KODE_POS = newCandidate.CandidateDetails.KodePos,
                        EXPECTED_SALARY = newCandidate.CandidateDetails.ExpectedSalary,
                        JUDUL_POSISI = newCandidate.CandidateDetails.JudulPosisi,
                        CATATAN = newCandidate.CandidateDetails.Catatan,
                        STATE_ID = "ST000",
                        SOURCE_ID = newCandidate.CandidateDetails.SourceId,
                        REFERER = newCandidate.CandidateDetails.Referer,
                        NPWP = newCandidate.CandidateDetails.NPWP,
                        CV = newCandidate.CandidateDetails.CV,
                        AVAIABLE_JOIN = newCandidate.CandidateDetails.AvailableJoin
                    };

                    EDUCATION education = new EDUCATION
                    {
                        EDUCATION_NAME = newCandidate.Education.EducationName,
                        MAJOR = newCandidate.Education.Major,
                        DEGREE = newCandidate.Education.Degree,
                        GPA = newCandidate.Education.GPA,
                        TAHUN_MASUK = newCandidate.Education.TahunMasuk,
                        TAHUN_LULUS = newCandidate.Education.TahunLulus,
                        CANDIDATE_ID = newCandidate.CandidateDetails.CandidateId
                    };
                    
                    EXPERIENCE experience = new EXPERIENCE
                    {
                        EXPERIENCE_NAME = newCandidate.Experience.ExperienceName,
                        INDUSTRI = newCandidate.Experience.Industri,
                        POSISI = newCandidate.Experience.Posisi,
                        START_DATE = newCandidate.Experience.StartDate,
                        END_DATE = newCandidate.Experience.EndDate,
                        JOB_DESC = newCandidate.Experience.JobDesc,
                        SKILL = newCandidate.Experience.Skill,
                        SALARY = newCandidate.Experience.Salary,
                        PROJECT = newCandidate.Experience.Project,
                        BENEFIT = newCandidate.Experience.Benefit,
                        CANDIDATE_ID = newCandidate.CandidateDetails.CandidateId
                    };

                    db.CANDIDATEs.Add(candidate);
                    db.EDUCATIONs.Add(education);
                    db.EXPERIENCEs.Add(experience);
                    db.SaveChanges();
                    TempData["status"] = "Data Berhasil Masuk";
                    return Redirect("~/candidate");
                }
                
            }else
            {
                return View("FormCandidate");
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

        //[Route("edit")]
        [ActionName("Update")]
        [HttpPost]
        public ActionResult Update(CandidateDTO edittedCandidate)
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {

                ModelState.Remove("TanggalLahir");
                ModelState.Remove("AvailableJoin");
                CandidateDTO temp = (CandidateDTO)TempData.Peek("candidateEdit");
                if (edittedCandidate.Foto == null)
                {
                    edittedCandidate.Foto = temp.Foto;
                }
                if (edittedCandidate.CV == null)
                {
                    edittedCandidate.CV = temp.CV;
                }
                if (ModelState.IsValid)
                {
                    
                    if (edittedCandidate.GambarFile != null)
                    {
                        string fileNameFoto = edittedCandidate.NamaLengkap;
                        string extensionFoto = Path.GetExtension(edittedCandidate.GambarFile.FileName);
                        fileNameFoto = fileNameFoto + extensionFoto;
                        edittedCandidate.Foto = "~/Images/" + fileNameFoto;
                        fileNameFoto = Path.Combine(Server.MapPath("~/Images/"), fileNameFoto);
                        edittedCandidate.GambarFile.SaveAs(fileNameFoto);
                    }else if (edittedCandidate.CvFile != null)
                    {
                        string fileNameCV = edittedCandidate.NamaLengkap;
                        string extensionCV = Path.GetExtension(edittedCandidate.CvFile.FileName);
                        fileNameCV = fileNameCV + extensionCV;
                        edittedCandidate.CV = "~/Cv/" + fileNameCV;
                        fileNameCV = Path.Combine(Server.MapPath("~/Cv/"), fileNameCV);
                        edittedCandidate.CvFile.SaveAs(fileNameCV);
                    }
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
                        USER_UPDATE = (string)Session["name"],
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
                        AVAIABLE_JOIN = edittedCandidate.AvailableJoin
                    };
                    db.Entry(candidate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["status"] = "Data Berhasil Di Edit";
                    return Redirect("~/candidate");
                }
                return View("EditCandidate", edittedCandidate);
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