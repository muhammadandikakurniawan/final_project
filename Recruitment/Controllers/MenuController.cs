using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recruitment.Models;
using System.Data.Entity;
//using Recruitment.Common;

namespace Recruitment.Controllers
{

    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult GetMainMenu()
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
                return View(menu);
            }
        }

        public ActionResult Setting()
        {

            return View();
        }

        public ActionResult ListMenu()
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
                return View(menu);
            }
        }

        public ActionResult FormMenu()
        {
            return View();
        }


        public ActionResult addmainmenu(MenuModels menu)
        {
            if (!ModelState.IsValid)
            {
                return View("FormMenu");
            }
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                List<MenuModels> menus = recruitment.MENUs.Select(m => new MenuModels
                {
                    MenuId = m.MENU_ID,
                    MenuName = m.MENU_NAME,
                    RoleId = m.ROLE_ID,
                    Action = m.ACTION,
                    Controller = m.CONTROLLER
                }).ToList();

                string last = menus.Select(m => m.MenuId).LastOrDefault();
                int count = 0;
                if (menus.Count != 0)
                {
                    count = int.Parse(last.Substring(last.Length - 3)) + 1;
                }
                string id = "MN" + String.Format("{0:D3}", count);

                MENU addmenu = new MENU
                {
                    MENU_ID = id,
                    MENU_NAME = menu.MenuName,
                    ROLE_ID = menu.RoleId,
                    ACTION = menu.Action,
                    CONTROLLER = menu.Controller
                };
                recruitment.MENUs.Add(addmenu);
                recruitment.SaveChanges();
                return Redirect("/Menu/ListMenu");
            }

        }



        public ActionResult FormEditMenu(string id)
        {

            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                MENU menu = recruitment.MENUs.Find(id);
                MenuModels menus = new MenuModels
                {
                    MenuId = menu.MENU_ID,
                    MenuName = menu.MENU_NAME,
                    RoleId = menu.ROLE_ID,
                    Action = menu.ACTION,
                    Controller = menu.CONTROLLER
                };
                return View(menus);
            }
        }

        public ActionResult EditMenu(MenuModels menu)
        {
            if (!ModelState.IsValid)
            {
                return View("FormEditMenu");
            }
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                MENU editMenu = new MENU
                {
                    MENU_ID = menu.MenuId,
                    MENU_NAME = menu.MenuName,
                    ROLE_ID = menu.RoleId,
                    ACTION = menu.Action,
                    CONTROLLER = menu.Controller
                };
                recruitment.Entry(editMenu).State = EntityState.Modified;
                recruitment.SaveChanges();
                return Redirect("/Menu/ListMenu");
            }
        }

        public ActionResult DeleteMenu(string id)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                MENU menu = recruitment.MENUs.Find(id);
                recruitment.MENUs.Remove(menu);
                recruitment.SaveChanges();
                return Redirect("/Menu/ListMenu");
            }

        }


        public ActionResult SideMenu()
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
                return PartialView("_SideMenu", menu);
            }

        }




    }
}