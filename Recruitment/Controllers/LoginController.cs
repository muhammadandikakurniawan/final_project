using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [ActionName("Login")]
        public ActionResult Login(Users users)
        {
            ModelState.Remove("Fullname");
            ModelState.Remove("Roleid");
            if (ModelState.IsValid)
            {
                bool isExist = false;
                using (RecruitmentEntities RE = new RecruitmentEntities())
                {

                    isExist = RE.USERs.Where(e => e.USERNAME.Trim().ToLower() == users.Username.Trim().ToLower()).Any();
                    var GetUser = RE.USERs.Where(e => e.USERNAME == users.Username).FirstOrDefault();
                    string pass = MD5Encryption.encryption(users.Password);
                    if (isExist)
                    {
                        if (GetUser.PASSWORD.Equals(pass))
                        {
                            Users usr = RE.USERs.Where(x => x.USERNAME.Trim().ToLower() == users.Username.Trim().ToLower()).Select(x => new Users
                            {
                                Username = x.USERNAME,
                                Fullname = x.FULLNAME,
                                Roleid = x.ROLE_ID,
                                UserId = x.USER_ID
                            }).FirstOrDefault();

                            //cek login

                            List<MenuModels> tempMenus = RE.MENUs.Select(x => new MenuModels
                            {
                                MenuId = x.MENU_ID,
                                MenuName = x.MENU_NAME,
                                Action = x.ACTION,
                                Controller = x.CONTROLLER,
                                RoleId = x.ROLE_ID
                            }).ToList();

                            List<MenuModels> menus = new List<MenuModels>();
                            foreach (MenuModels menuModels in tempMenus)
                            {
                                String[] roleArr = menuModels.RoleId.Split(',');
                                if (roleArr.Contains(usr.Roleid))
                                {
                                    menus.Add(menuModels);
                                }
                            }

                            Session["user"] = usr;
                            Session["menu"] = menus;
                            Session["username"] = usr.Roleid;
                            Session["name"] = usr.Fullname;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["cek"] = "Username / Password Salah";
                            return Redirect("Index");
                        }

                    }
                    else
                    {
                        TempData["cek"] = "Username tidak terdaftar";
                        return Redirect("Index");
                    }
                }
            }
            return View("Index");
        }

        [ActionName("Logout")]
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("Index");
        }
    }
} 