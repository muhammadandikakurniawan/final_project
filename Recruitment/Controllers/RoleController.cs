using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Controllers
{
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            //IEnumerable<RoleModels> roleList;
            //HttpResponseMessage respone = GlobalVariables.WebApiClient.GetAsync("Role").Result;
            //roleList = respone.Content.ReadAsAsync<IEnumerable<RoleModels>>().Result;
            //return View(roleList);
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                List<RoleModels> Roles = re.ROLEs.Select(e => new RoleModels
                {
                    RoleId = e.ROLE_ID,
                    RoleName = e.ROLE_NAME
                }).ToList();

                return View("Index", Roles);
            }
        }


        public ActionResult Tambah()
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("MaxId").Result;
            //return View(response.Content.ReadAsAsync<RoleModels>().Result);
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                List<RoleModels> roles = re.ROLEs.Select(x => new RoleModels
                {
                    RoleId = x.ROLE_ID,
                    RoleName = x.ROLE_NAME
                }).ToList();

                string maxId = roles.Max(x => x.RoleId);
                int angka = 0;
                if (roles.Count != 0)
                {
                    angka = int.Parse(maxId.Substring(maxId.Length - 3)) + 1;
                }
                string newRole = "RL" + string.Format("{0:D3}", angka);

                RoleModels ListRole = new RoleModels
                {
                    RoleId = newRole,
                    RoleName = null
                };
                return View("Tambah", ListRole);
            }


        }

        [HttpPost]
        public ActionResult Tambah(RoleModels role)
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Role", role).Result;
            //TempData["status"] = "Data Berhasil Ditambah";
            //return RedirectToAction("Index");
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                ROLE Role = new ROLE
                {
                    ROLE_ID = role.RoleId,
                    ROLE_NAME = role.RoleName
                };
                re.ROLEs.Add(Role);
                re.SaveChanges();
                TempData["status"] = "Add Data Succesfully";
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(string id)
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Role/" + id.ToString()).Result;
            //return View(response.Content.ReadAsAsync<RoleModels>().Result);
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                ROLE Role = re.ROLEs.Find(id);
                RoleModels Roles = new RoleModels
                {
                    RoleId = Role.ROLE_ID,
                    RoleName = Role.ROLE_NAME
                };

                return View("Edit", Roles);
            }
        }


        [HttpPost]
        public ActionResult Edit(RoleModels role)
        {

            //HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Role/" + role.RoleId, role).Result;
            //TempData["status"] = "Data Berhasil Diedit";
            //return RedirectToAction("Index");
            using (RecruitmentEntities re = new RecruitmentEntities())
            {

                ROLE Roles = new ROLE
                {
                    ROLE_ID = role.RoleId,
                    ROLE_NAME = role.RoleName
                };
                re.Entry(Roles).State = System.Data.Entity.EntityState.Modified;
                re.SaveChanges();
                TempData["status"] = "Data Successfully Updated";
                return RedirectToAction("Index");
            }

        }

        public ActionResult Delete(string id)
        {
            //HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Role/" + id).Result;
            //TempData["status"] = "Data Berhasil Dihapus";
            //return RedirectToAction("Index");
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                ROLE Role = re.ROLEs.Find(id);
                re.ROLEs.Remove(Role);
                re.SaveChanges();
                TempData["status"] = "Data Successfully Deleted";
                return RedirectToAction("Index");
            }
        }
    }
}