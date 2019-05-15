using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitment.Models;

namespace Recruitment.Controllers
{
    public class EducationController : Controller
    {
        // GET: Education

        public ActionResult EducationList()
        {
            using (RecruitmentEntities ent = new RecruitmentEntities())
            {
                List<EducationDTO> education = ent.EDUCATIONs.Select(x => new EducationDTO
                {
                    EducationId = x.EDUCATION_ID,
                    EducationName = x.EDUCATION_NAME,
                    TahunMasuk = (DateTime)x.TAHUN_MASUK,
                    GPA = (float)x.GPA,
                    Degree = x.DEGREE,
                    Major = x.MAJOR,
                    TahunLulus = (DateTime)x.TAHUN_MASUK,
                    CandidateId = x.CANDIDATE_ID
                }).ToList();
                return View(education);
            }
        }

        [HttpGet]
        public ActionResult AddEducation()
        {
            return View();
        }

        [HttpPost]
        [ActionName("addNewEducation")]
        public ActionResult AddEducation(EducationDTO education)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                EDUCATION ex = new EDUCATION
                {
                    EDUCATION_ID = education.EducationId,
                    EDUCATION_NAME = education.EducationName,
                    DEGREE = education.Degree,
                    GPA = education.GPA,
                    MAJOR = education.Major,
                    TAHUN_LULUS = education.TahunLulus,
                    TAHUN_MASUK = education.TahunMasuk,
                    CANDIDATE_ID = education.CandidateId

                };
                re.EDUCATIONs.Add(ex);
                re.SaveChanges();
                TempData["status"] = "Data Berhasil Ditambah";
                return Redirect("../Candidate/getDetails/" + @education.CandidateId);
            }
        }

        [HttpGet]
        public ActionResult EditEducation(int id)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                EDUCATION ex = re.EDUCATIONs.Find(id);
                EducationDTO education = new EducationDTO
                {
                    Degree = ex.DEGREE,
                    GPA = (float)ex.GPA,
                    Major = ex.MAJOR,
                    EducationId = ex.EDUCATION_ID,
                    TahunLulus = (DateTime)ex.TAHUN_LULUS,
                    TahunMasuk = (DateTime)ex.TAHUN_MASUK,
                    EducationName = ex.EDUCATION_NAME,
                    CandidateId = ex.CANDIDATE_ID
                };
                return View("EditEducation", education);
            }
        }
        [ActionName("updateEdu")]
        [HttpPost]
        public ActionResult updateEducation(EducationDTO edu)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                EDUCATION eduUpdate = new EDUCATION
                {
                    EDUCATION_ID = edu.EducationId,
                    DEGREE = edu.Degree,
                    EDUCATION_NAME = edu.EducationName,
                    GPA = edu.GPA,
                    MAJOR = edu.Major,
                    TAHUN_LULUS = edu.TahunLulus,
                    TAHUN_MASUK = edu.TahunMasuk,
                    CANDIDATE_ID = edu.CandidateId,
                };
                re.Entry(eduUpdate).State = System.Data.Entity.EntityState.Modified;
                re.SaveChanges();
                TempData["status"] = "Data Berhasil Diupdate";
                return Redirect("../Candidate/getDetails/" + edu.CandidateId);
            }
        }
    }
}