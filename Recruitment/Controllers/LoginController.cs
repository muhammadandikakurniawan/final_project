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
            using (RecruitmentEntities RE = new RecruitmentEntities())
            {
                string pass = MD5Encryption.encryption(users.Password);
                var user = RE.USERs.Where(e => e.USERNAME == users.Username && e.PASSWORD == pass).FirstOrDefault();
                var role = RE.ROLEs.Where(e => e.ROLE_ID == user.ROLE_ID).FirstOrDefault();
                if (user == null)
                {
                    Response.Write("Username Atau Password Anda Salah");
                }
                else
                {
                    if (user.ROLE_ID == role.ROLE_ID)
                    {
                        if (role.ROLE_NAME.Equals("Admin"))
                        {
                            Session["admin"] = users.Username;
                            Session["role"] = role.ROLE_NAME;
                            return Redirect("Home");
                        }else if (role.ROLE_NAME.Equals("Pra Seleksi"))
                        {
                            Session["pra seleksi"] = users.Username;
                            Session["role"] = role.ROLE_NAME;
                            return Redirect("Home");
                        }else if (role.ROLE_NAME.Equals("Call"))
                        {
                            Session["call"] = users.Username;
                            Session["role"] = role.ROLE_NAME;
                            return Redirect("Home");
                        }
                    }
                }
                return View("Index");
            }

        }
    }
} 