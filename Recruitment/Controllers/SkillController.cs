using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Controllers
{
    public class SkillController : Controller
    {
        // GET: Skill
        public ActionResult Index()
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                List<SkillModels> skills = re.SKILLs.Select(e => new SkillModels
                {
                   SkillId = e.ID_SKILL,
                    SkillName = e.SKILL_NAME
                }).ToList();

                return View("Index", skills);
            }
        }

        public ActionResult Tambah()
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("MaxId").Result;
            //return View(response.Content.ReadAsAsync<SkillModels>().Result);
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                //List<SkillModels> skills = re.SKILLs.Select(x => new SkillModels
                //{
                //    SkillId = x.ID_SKILL,
                //    SkillName = x.SKILL_NAME
                //}).ToList();

                //string maxId = roles.Max(x => x.SkillId);
                //int angka = 0;
                //if (roles.Count != 0)
                //{
                //    angka = int.Parse(maxId.Substring(maxId.Length - 3)) + 1;
                //}
                //string newRole = "RL" + string.Format("{0:D3}", angka);

                //SkillModels ListRole = new SkillModels
                //{
                //    SkillId = newRole,
                //    SkillName = null
                //};
                return View("Tambah");
            }


        }

        [HttpPost]
        public ActionResult Tambah(SkillModels skill)
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Role", role).Result;
            //TempData["status"] = "Data Berhasil Ditambah";
            //return RedirectToAction("Index");
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                int cekname = re.SKILLs.Where(x => x.SKILL_NAME == skill.SkillName).Count();
                if (cekname > 0)
                {
                    TempData["status"] = "Data Wes Ono";
                }
                else
                {
                    SKILL Skill = new SKILL
                    {
                        //ID_SKILL = skill.SkillId,
                        SKILL_NAME = skill.SkillName
                    };
                    re.SKILLs.Add(Skill);
                    re.SaveChanges();
                    TempData["status"] = "Add Data Succesfully";
                }
               
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(long id)
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Role/" + id.ToString()).Result;
            //return View(response.Content.ReadAsAsync<SkillModels>().Result);
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                SKILL Skill = re.SKILLs.Find(id);
                SkillModels Skills = new SkillModels
                {
                    SkillId = Skill.ID_SKILL,
                    SkillName = Skill.SKILL_NAME
                };

                return View("Edit", Skills);
            }
        }


        [HttpPost]
        public ActionResult Edit(SkillModels skill)
        {

            //HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Role/" + role.SkillId, role).Result;
            //TempData["status"] = "Data Berhasil Diedit";
            //return RedirectToAction("Index");
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                int cekname = re.SKILLs.Where(x => x.SKILL_NAME == skill.SkillName && x.ID_SKILL != skill.SkillId).Count();
                if (cekname > 0)
                {
                    TempData["status"] = "Data Wes Ono";
                }
                else
                {
                    SKILL Skills = new SKILL
                    {
                        ID_SKILL = skill.SkillId,
                        SKILL_NAME = skill.SkillName
                    };
                    re.Entry(Skills).State = System.Data.Entity.EntityState.Modified;
                    re.SaveChanges();
                    TempData["status"] = "Data Successfully Updated";
                }
                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(long id)
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Role/" + id).Result;
            //TempData["status"] = "Data Berhasil Dihapus";
            //return RedirectToAction("Index");
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                SKILL Skill = re.SKILLs.Find(id);
                re.SKILLs.Remove(Skill);
                re.SaveChanges();
                TempData["status"] = "Data Successfully Deleted";
                return RedirectToAction("Index");
            }
        }
    }
}