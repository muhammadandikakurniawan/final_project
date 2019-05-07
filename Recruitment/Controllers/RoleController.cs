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
            IEnumerable<RoleModels> roleList;
            HttpResponseMessage respone = GlobalVariables.WebApiClient.GetAsync("Role").Result;
            roleList = respone.Content.ReadAsAsync<IEnumerable<RoleModels>>().Result;
            return View(roleList);
        }


        public ActionResult Tambah()
        {
            return View(new RoleModels());
        }

        [HttpPost]
        public ActionResult Tambah(RoleModels role)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Role", role).Result;
            TempData["status"] = "Data Berhasil Ditambah";
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Role/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<RoleModels>().Result);
        }


        [HttpPost]
        public ActionResult Edit(RoleModels role)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Role/" + role.RoleId, role).Result;
            TempData["status"] = "Data Berhasil Diedit";
            return RedirectToAction("Index");

        }

        public ActionResult Delete(string id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Role/" + id).Result;
            TempData["status"] = "Data Berhasil Dihapus";
            return RedirectToAction("Index");
        }
    }
}