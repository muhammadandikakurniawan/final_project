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
            bool isExist = false;
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {

                isExist = RE.USERs.Where(e => e.USERNAME.Trim().ToLower() == users.Username.Trim().ToLower()).Any();
                if (isExist)
                {
                    Users usr = RE.USERs.Where(x => x.USERNAME.Trim().ToLower() == users.Username.Trim().ToLower()).Select(x => new Users {
                        Username = x.USERNAME,
                        Roleid = x.ROLE_ID,
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
                    foreach(MenuModels menuModels in tempMenus) {
                        String[] roleArr = menuModels.RoleId.Split(',');
                        if (roleArr.Contains(usr.Roleid)){
                            menus.Add(menuModels);
                        }
                    }

                    Session["user"] = usr;
                    Session["menu"] = menus;
                    Session["username"] = usr.Username;
                    return RedirectToAction("Index","Home");
                }else
                {
                    TempData["cek"] = "Username tidak terdaftar";
                    return View("Index");
                }
                //string pass = MD5Encryption.encryption(users.Password);
                //var user = RE.USERs.Where(e => e.USERNAME == users.Username && e.PASSWORD == pass).FirstOrDefault();
                //var role = RE.ROLEs.Where(e => e.ROLE_ID == user.ROLE_ID).FirstOrDefault();
                //if (user == null)
                //{
                //    Response.Write("Username Atau Password Anda Salah");
                //}
                //else
                //{
                //    if (user.ROLE_ID == role.ROLE_ID)
                //    {
                //        if (role.ROLE_NAME.Equals("Admin"))
                //        {
                //            Session["admin"] = users.Username;
                //            Session["role"] = role.ROLE_NAME;
                //            return Redirect("Home");
                //        }else if (role.ROLE_NAME.Equals("Pra Seleksi"))
                //        {
                //            Session["pra seleksi"] = users.Username;
                //            Session["role"] = role.ROLE_NAME;
                //            return Redirect("Home");
                //        }else if (role.ROLE_NAME.Equals("Call"))
                //        {
                //            Session["call"] = users.Username;
                //            Session["role"] = role.ROLE_NAME;
                //            return Redirect("Home");
                //        }
                //    }
                //}
                return View("Index");
            }

        }
    }
} 