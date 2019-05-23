using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitment.Models;
using PagedList;
using System.Data.Entity;

namespace Recruitment.Controllers
{
    [RoutePrefix("call")]
    public class CallController : Controller
    {
        List<CandidateCallDTO> candidates;

        #region Candidate List Pages
        // GET: Call
        [Route("")]
        public ActionResult Index(string filterPosition, string currentFilter, string searchString, string currentSearch, int? page)
        {
            InitializeCandidates();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentSearch;
            }
            ViewBag.currentSearch = searchString;

            if (filterPosition != null)
            {
                page = 1;
            }
            else
            {
                filterPosition = currentFilter;
            }
            ViewBag.currentFilter = filterPosition;

            //SEARCHING
            if (!String.IsNullOrEmpty(searchString))
            {
                candidates = candidates.Where(c => c.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Position.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Source.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Phone.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Email.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.PreSelectPIC.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Notes.ToUpper().Contains(searchString.ToUpper())

                ).ToList();
            }

            if (!String.IsNullOrEmpty(filterPosition))
            {
                candidates = candidates.Where(c => c.Position.ToUpper().Equals(filterPosition.ToUpper())).ToList();
            }

            //PAGING
            //Require nuGet package PagedList.mvc
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 5; //The amount of row displayed each page
            int pageNumber = (page ?? 1);
            IPagedList<CandidateCallDTO> pagedCandidate = candidates.ToPagedList(pageNumber, pageSize);
            //Get Experiences for candidates of the current page
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                foreach (CandidateCallDTO candidate in pagedCandidate)
                {
                    candidate.Experiences = db.EXPERIENCEs.Where(e => e.CANDIDATE_ID == candidate.CandidateId).ToList();
                }
            }

            return View("ListCall", pagedCandidate);
        }

        [Route("called")]
        public ActionResult IndexCalled(string filterPosition, string currentFilter, string searchString, string currentSearch, int? page)
        {
            InitializeCalledCandidates();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentSearch;
            }
            ViewBag.currentSearch = searchString;

            if (filterPosition != null)
            {
                page = 1;
            }
            else
            {
                filterPosition = currentFilter;
            }
            ViewBag.currentFilter = filterPosition;

            //SEARCHING
            if (!String.IsNullOrEmpty(searchString))
            {
                candidates = candidates.Where(c => c.Name.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Position.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Source.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Phone.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Email.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.PreSelectPIC.ToUpper().Contains(searchString.ToUpper()) ||
                                                   c.Notes.ToUpper().Contains(searchString.ToUpper())

                ).ToList();
            }

            if (!String.IsNullOrEmpty(filterPosition))
            {
                candidates = candidates.Where(c => c.Position.ToUpper().Equals(filterPosition.ToUpper())).ToList();
            }

            //PAGING
            //Require nuGet package PagedList.mvc
            if (page == null)
            {
                page = 1;
            }
            int pageSize = 5; //The amount of row displayed each page
            int pageNumber = (page ?? 1);
            IPagedList<CandidateCallDTO> pagedCandidate = candidates.ToPagedList(pageNumber, pageSize);
            //Get Experiences for candidates of the current page
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                foreach (CandidateCallDTO candidate in pagedCandidate)
                {
                    candidate.Experiences = db.EXPERIENCEs.Where(e => e.CANDIDATE_ID == candidate.CandidateId).ToList();
                }
            }

            return View("ListCalled", pagedCandidate);
        }

        #endregion

        #region Candidate List Support
        void InitializeCandidates()
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                candidates = (from c in db.CANDIDATEs
                              join s in db.SOURCEs on c.SOURCE_ID equals s.SOURCE_ID into table1
                              from s in table1.DefaultIfEmpty()
                              join u in db.USERs on c.USER_CREATE equals u.USERNAME into table2
                              from u in table2.DefaultIfEmpty()
                              join st in db.STATEs on c.STATE_ID equals st.STATE_ID into table3
                              from st in table3.DefaultIfEmpty()
                              join p in db.POSITIONs on c.JUDUL_POSISI equals p.POSITION_ID into table4
                              from p in table4.DefaultIfEmpty()
                              where c.STATE_ID == "ST001"
                              select new CandidateCallDTO
                              {
                                  CandidateId = c.CANDIDATE_ID,
                                  Name = c.NAMA_LENGKAP,
                                  Position = p.POSITION_NAME,
                                  Source = s.SOURCE_NAME,
                                  Phone = c.NOHP,
                                  Email = c.EMAIL,
                                  PreSelectPIC = u.FULLNAME,
                                  State = st.STATE_NAME,
                                  Notes = c.CATATAN,
                                  ExpectedSalary = c.EXPECTED_SALARY,
                                  AvailableJoin = (DateTime)c.AVAIABLE_JOIN
                              }).ToList();

                //Insert each row of EXPERIENCE into List<EXPERIENCE>
                //Needed for popup function
                //foreach (CandidateCallDTO candidate in candidates)
                //{
                //    candidate.Experiences = db.EXPERIENCEs.Where(e => e.CANDIDATE_ID == candidate.CandidateId).ToList();
                //}

                //Populate the SelectListItem used for filter dropdown
                List<SelectListItem> filterPositions = db.POSITIONs.Select(p => new SelectListItem
                {
                    Text = p.POSITION_NAME,
                    Value = p.POSITION_NAME
                }).ToList();
                filterPositions.Insert(0, (new SelectListItem { Text = "", Value = "" }));

                TempData["filterPositions"] = filterPositions;
            }
        }

        void InitializeCalledCandidates()
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                candidates = (from c in db.CANDIDATEs
                              join s in db.SOURCEs on c.SOURCE_ID equals s.SOURCE_ID into table1
                              from s in table1.DefaultIfEmpty()
                              join u in db.USERs on c.USER_CREATE equals u.USERNAME into table2
                              from u in table2.DefaultIfEmpty()
                              join st in db.STATEs on c.STATE_ID equals st.STATE_ID into table3
                              from st in table3.DefaultIfEmpty()
                              join p in db.POSITIONs on c.JUDUL_POSISI equals p.POSITION_ID into table4
                              from p in table4.DefaultIfEmpty()
                              where c.STATE_ID == "ST002" || c.STATE_ID == "ST003" || c.STATE_ID == "ST009"
                              select new CandidateCallDTO
                              {
                                  CandidateId = c.CANDIDATE_ID,
                                  Name = c.NAMA_LENGKAP,
                                  Position = p.POSITION_NAME,
                                  Source = s.SOURCE_NAME,
                                  Phone = c.NOHP,
                                  Email = c.EMAIL,
                                  PreSelectPIC = u.FULLNAME,
                                  State = st.STATE_NAME,
                                  Notes = c.CATATAN,
                                  ExpectedSalary = c.EXPECTED_SALARY,
                                  AvailableJoin = (DateTime)c.AVAIABLE_JOIN
                              }).ToList();

                //Insert each row of EXPERIENCE into List<EXPERIENCE>
                //Needed for popup function
                //foreach (CandidateCallDTO candidate in candidates)
                //{
                //    candidate.Experiences = db.EXPERIENCEs.Where(e => e.CANDIDATE_ID == candidate.CandidateId).ToList();
                //}

                //Populate the SelectListItem used for filter dropdown
                List<SelectListItem> filterPositions = db.POSITIONs.Select(p => new SelectListItem
                {
                    Text = p.POSITION_NAME,
                    Value = p.POSITION_NAME
                }).ToList();
                filterPositions.Insert(0, (new SelectListItem { Text = "", Value = "" }));

                TempData["filterPositions"] = filterPositions;
            }
        }
        #endregion

        #region Email
        [ActionName("FormEmail")]
        public ActionResult FormEmail(string id)
        {
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {
                FormEmailViewModel formEmail = new FormEmailViewModel();
                formEmail.Candidate = RE.CANDIDATEs.Find(id);
                formEmail.InitializeAceTemplate(Session["name"].ToString());

                return View("FormEmail", formEmail);
            }
        }

        [ActionName("SendEmail")]
        [ValidateInput(false)]
        public ActionResult SendEmail(FormEmailViewModel formEmail)
        {
            return Redirect("~/call/called");
        }
        #endregion

        #region Hanif
        public ActionResult NextProses(string id, string from)
        {
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {

                CANDIDATE c = RE.CANDIDATEs.Find(id);
                EDUCATION e = RE.EDUCATIONs.Where(ed => ed.CANDIDATE_ID == id).FirstOrDefault();
                EXPERIENCE x = RE.EXPERIENCEs.Where(xp => xp.CANDIDATE_ID == id).FirstOrDefault();
                CallModelProses candidate = new CallModelProses
                {
                    CandidateId = c.CANDIDATE_ID,
                    NamaLengkap = c.NAMA_LENGKAP,
                    NamaPanggilan = c.NAMA_PANGGILAN,
                    JenisKelamin = c.KD_JENIS_KELAMIN,
                    TempatLahir = c.TEMPAT_LAHIR,
                    TanggalLahir = c.TANGGAL_LAHIR,
                    Agama = c.AGAMA,
                    StatusPerkawinan = c.STATUS_PERKAWINAN,
                    AlamatRumah = c.ALAMAT_RUMAH,
                    AlamatOrtu = c.ALAMAT_ORTU,
                    TelpRumah = c.TELP_RUMAH,
                    Email = c.EMAIL,
                    NoHP = c.NOHP,
                    NoKTP = c.NO_KTP,
                    IsDeleted = c.IS_DELETED,
                    Foto = c.FOTO,
                    UserCreate = c.USER_CREATE,
                    DateTimeCreate = c.DATETIME_CREATE,
                    UserUpdate = c.USER_UPDATE,
                    DateTimeUpdate = c.DATETIME_UPDATE,
                    KodePos = c.KODE_POS,
                    ExpectedSalary = c.EXPECTED_SALARY,
                    JudulPosisi = c.JUDUL_POSISI,
                    Catatan = c.CATATAN,
                    StateId = c.STATE_ID,
                    SourceId = c.SOURCE_ID,
                    Referer = c.REFERER,
                    NPWP = c.NPWP,
                    CV = c.CV,
                    AvailableJoin = (DateTime)c.AVAIABLE_JOIN,
                    EducationId = e.EDUCATION_ID,
                    EducationName = e.EDUCATION_NAME,
                    Major = e.MAJOR,
                    Degree = e.DEGREE,
                    GPA = (float)e.GPA,
                    TahunMasuk = (DateTime)e.TAHUN_MASUK,
                    TahunLulus = (DateTime)e.TAHUN_LULUS,
                    CandidateIdinEdu = e.CANDIDATE_ID,
                    ExperienceId = x.EXPERIENCE_ID,
                    ExperienceName = x.EXPERIENCE_NAME,
                    Industri = x.INDUSTRI,
                    Posisi = x.POSISI,
                    StartDate = (DateTime)x.START_DATE,
                    EndDate = (DateTime)x.END_DATE,
                    JobDesc = x.JOB_DESC,
                    Skill = x.SKILL,
                    Salary = (int)x.SALARY,
                    Project = x.PROJECT,
                    Benefit = x.BENEFIT,
                    CandidateIdinExp = x.CANDIDATE_ID

                };
                TempData["from"] = from;
                Session["candidateId"] = c.CANDIDATE_ID;
                return View(candidate);
            }
        }

        [HttpPost]
        public ActionResult PostNextProses(CallModelProses call)
        {
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {
                CANDIDATE candidates = new CANDIDATE
                {
                    CANDIDATE_ID = call.CandidateId,
                    NAMA_LENGKAP = call.NamaLengkap,
                    NAMA_PANGGILAN = call.NamaPanggilan,
                    KD_JENIS_KELAMIN = call.JenisKelamin,
                    TEMPAT_LAHIR = call.TempatLahir,
                    TANGGAL_LAHIR = call.TanggalLahir,
                    AGAMA = call.Agama,
                    STATUS_PERKAWINAN = call.StatusPerkawinan,
                    ALAMAT_RUMAH = call.AlamatRumah,
                    ALAMAT_ORTU = call.AlamatOrtu,
                    TELP_RUMAH = call.TelpRumah,
                    EMAIL = call.Email,
                    NOHP = call.NoHP,
                    NO_KTP = call.NoKTP,
                    IS_DELETED = call.IsDeleted,
                    FOTO = call.Foto,
                    USER_CREATE = call.UserCreate,
                    DATETIME_CREATE = call.DateTimeCreate,
                    USER_UPDATE = call.UserUpdate,
                    DATETIME_UPDATE = call.DateTimeUpdate,
                    KODE_POS = call.KodePos,
                    EXPECTED_SALARY = call.ExpectedSalary,
                    JUDUL_POSISI = call.JudulPosisi,
                    CATATAN = call.Catatan,
                    STATE_ID = call.StateId,
                    SOURCE_ID = call.SourceId,
                    REFERER = call.Referer,
                    NPWP = call.NPWP,
                    CV = call.CV,
                    AVAIABLE_JOIN = call.AvailableJoin
                };
                RE.Entry(candidates).State = EntityState.Modified;
                RE.SaveChanges();

                EXPERIENCE experience = new EXPERIENCE
                {
                    EXPERIENCE_ID = call.ExperienceId,
                    EXPERIENCE_NAME = call.ExperienceName,
                    INDUSTRI = call.Industri,
                    POSISI = call.Posisi,
                    START_DATE = call.StartDate,
                    END_DATE = call.EndDate,
                    JOB_DESC = call.JobDesc,
                    SKILL = call.Skill,
                    SALARY = call.Salary,
                    PROJECT = call.Project,
                    BENEFIT = call.Benefit,
                    CANDIDATE_ID = call.CandidateId
                };
                RE.Entry(experience).State = EntityState.Modified;
                RE.SaveChanges();
                if (TempData.Peek("from") != null)
                {
                    return Redirect("~/" + TempData["from"]);
                }
                return Redirect("~/call");
            }
        }

        public ActionResult Cancel()
        {
            if (TempData.Peek("from") != null)
            {
                return Redirect("~/" + TempData["from"]);
            }
            return Redirect("~/call");
        }

        [HttpGet]
        public ActionResult EditExperience(int id)
        {
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {
                EXPERIENCE expModel = RE.EXPERIENCEs.Find(id);
                ExperienceDTO experienceDTO = new ExperienceDTO
                {

                    ExperienceId = expModel.EXPERIENCE_ID,
                    ExperienceName = expModel.EXPERIENCE_NAME,
                    Industri = expModel.INDUSTRI,
                    Posisi = expModel.POSISI,
                    StartDate = (DateTime)expModel.START_DATE,
                    EndDate = (DateTime)expModel.END_DATE,
                    JobDesc = expModel.JOB_DESC,
                    Skill = expModel.SKILL,
                    Salary = (int)expModel.SALARY,
                    Project = expModel.PROJECT,
                    Benefit = expModel.BENEFIT,
                    CandidateId = expModel.CANDIDATE_ID

                };
                return View(experienceDTO);
            }
        }

        public ActionResult PostEditExperience(ExperienceDTO experienceDTO)
        {
            string id = (string)Session["candidateId"];
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {
                EXPERIENCE EditExperience = new EXPERIENCE
                {
                    EXPERIENCE_ID = experienceDTO.ExperienceId,
                    EXPERIENCE_NAME = experienceDTO.ExperienceName,
                    INDUSTRI = experienceDTO.Industri,
                    POSISI = experienceDTO.Posisi,
                    START_DATE = (DateTime)experienceDTO.StartDate,
                    END_DATE = (DateTime)experienceDTO.EndDate,
                    JOB_DESC = experienceDTO.JobDesc,
                    SALARY = experienceDTO.Salary,
                    SKILL = experienceDTO.Skill,
                    PROJECT = experienceDTO.Project,
                    BENEFIT = experienceDTO.Benefit,
                    CANDIDATE_ID = id
                };
                RE.Entry(EditExperience).State = EntityState.Modified;
                RE.SaveChanges();
                return Redirect("/Call/NextProses/" + id);
            }
        }
        #endregion

        #region Ridwan
        public ActionResult addExperienced()
        {

            return View();
        }

        public ActionResult addEx(ExperienceDTO experienceDTO)
        {
            string id = (string)Session["candidateId"];
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {
                EXPERIENCE addexperience = new EXPERIENCE
                {
                    EXPERIENCE_ID = experienceDTO.ExperienceId,
                    EXPERIENCE_NAME = experienceDTO.ExperienceName,
                    INDUSTRI = experienceDTO.Industri,
                    POSISI = experienceDTO.Posisi,
                    START_DATE = (DateTime)experienceDTO.StartDate,
                    END_DATE = (DateTime)experienceDTO.EndDate,
                    JOB_DESC = experienceDTO.JobDesc,
                    SALARY = experienceDTO.Salary,
                    SKILL = experienceDTO.Skill,
                    PROJECT = experienceDTO.Project,
                    BENEFIT = experienceDTO.Benefit,
                    CANDIDATE_ID = id
                };
                RE.EXPERIENCEs.Add(addexperience);
                RE.SaveChanges();
                return Redirect("/Call/NextProses/" + id);
            }
        }
        #endregion

        #region UNUSED

        //SORTING
        //string sortOrder = (string)TempData.Peek("SortOrder");

        //if(sortOrder == "id_desc") {
        //    candidates = candidates.OrderByDescending(c => c.CANDIDATE_ID).ToList();
        //}
        //else if(sortOrder == "name_asc") {
        //    candidates = candidates.OrderBy(c => c.NAMA_LENGKAP).ToList();
        //}
        //else if(sortOrder == "name_desc") {
        //    candidates = candidates.OrderByDescending(c => c.NAMA_LENGKAP).ToList();
        //}
        //else { //Default sort is id_asc
        //    candidates = candidates.OrderBy(c => c.CANDIDATE_ID).ToList();
        //}

        //[ActionName("SortByID")]
        //public ActionResult SortById() {
        //    string sortOrder = (string)TempData.Peek("SortOrder");
        //    if (sortOrder == "id_asc" || String.IsNullOrEmpty(sortOrder)) {
        //        TempData["SortOrder"] = "id_desc";
        //    }
        //    else {
        //        TempData["SortOrder"] = "id_asc";
        //    }

        //    return Redirect("~/call");
        //}

        //[ActionName("SortByName")]
        //public ActionResult SortByName() {
        //    string sortOrder = (string)TempData.Peek("SortOrder");
        //    if (sortOrder == "name_asc") {
        //        TempData["SortOrder"] = "name_desc";
        //    }
        //    else {
        //        TempData["SortOrder"] = "name_asc";
        //    }

        //    return Redirect("~/call");
        //}


        #endregion
    }
}