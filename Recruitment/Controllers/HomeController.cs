using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitment.Models;
using System.Data.Entity;

namespace Recruitment.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult MenuLayout()
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {

                List<MenuModels> menu = recruitment.MENUs.Select(m =>
                new MenuModels
                {
                    MenuId = m.MENU_ID,
                    MenuName = m.MENU_NAME,
                    RoleId = m.ROLE_ID,
                    Action = m.ACTION,
                    Controller = m.CONTROLLER
                }).ToList();
                return PartialView("_MenuLayout", menu);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}