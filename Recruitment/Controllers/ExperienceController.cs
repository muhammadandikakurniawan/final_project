using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitment.Models;
namespace Recruitment.Controllers
{
    public class ExperienceController : Controller
    {
        // GET: Experience
        public ActionResult ListExperience()
        {
            using(RecruitmentEntities re = new RecruitmentEntities())
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddExperience(string id)
        {    
            return View("AddExperience");
        }

        [HttpPost]
        [ActionName("AddNewExperience")]
        public ActionResult AddExperience(ExperienceDTO experience)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                EXPERIENCE ex = new EXPERIENCE
                {
                    EXPERIENCE_ID = experience.ExperienceId,
                    EXPERIENCE_NAME = experience.ExperienceName,
                    INDUSTRI = experience.Industri,
                    POSISI = experience.Posisi,
                    START_DATE = experience.StartDate,
                    END_DATE = experience.EndDate,
                    SALARY = experience.Salary,
                    JOB_DESC = experience.JobDesc,
                    SKILL = experience.Skill,
                    PROJECT = experience.Project,
                    BENEFIT = experience.Benefit,
                    CANDIDATE_ID = experience.CandidateId
                };
                re.EXPERIENCEs.Add(ex);
                re.SaveChanges();
                TempData["status"] = "Data Berhasil Ditambah";
                return Redirect("~/Candidate");
            }
        }

        [HttpGet]
        public ActionResult EditExperiences(int id)
        {
            using(RecruitmentEntities re = new RecruitmentEntities())
            {
                EXPERIENCE ex = re.EXPERIENCEs.Find(id);
                ExperienceDTO experience = new ExperienceDTO
                {
                    ExperienceId = ex.EXPERIENCE_ID,
                    ExperienceName = ex.EXPERIENCE_NAME,
                    Industri = ex.INDUSTRI,
                    Posisi = ex.POSISI,
                    StartDate = (DateTime)ex.START_DATE,
                    EndDate = (DateTime)ex.END_DATE,
                    JobDesc = ex.JOB_DESC,
                    Skill = ex.SKILL,
                    Salary = (int)ex.SALARY,
                    Project = ex.PROJECT,
                    Benefit = ex.BENEFIT,
                    CandidateId = ex.CANDIDATE_ID
                };
                return View("EditExperiences", experience);
            }
        }

        [HttpPost]
        [ActionName("UpdateExperience")]
        public ActionResult EditExperiences(ExperienceDTO experienceDTO)
        {
            using(RecruitmentEntities re = new RecruitmentEntities())
            {
                EXPERIENCE addExperience = new EXPERIENCE
                {
                    EXPERIENCE_ID = experienceDTO.ExperienceId,
                    EXPERIENCE_NAME = experienceDTO.ExperienceName,
                    INDUSTRI = experienceDTO.Industri,
                    POSISI = experienceDTO.Posisi,
                    START_DATE = experienceDTO.StartDate,
                    END_DATE = experienceDTO.EndDate,
                    JOB_DESC = experienceDTO.JobDesc,
                    SKILL = experienceDTO.Skill,
                    SALARY = experienceDTO.Salary,
                    PROJECT = experienceDTO.Project,
                    BENEFIT = experienceDTO.Benefit,
                    CANDIDATE_ID = experienceDTO.CandidateId
                };
                re.Entry(addExperience).State = System.Data.Entity.EntityState.Modified;
                re.SaveChanges();
                TempData["status"] = "Data Berhasil Diupdate";
                return Redirect("~/candidate");
            }
        }
    }
}