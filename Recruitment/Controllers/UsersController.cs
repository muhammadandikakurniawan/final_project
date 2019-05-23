using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Controllers
{
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult ListUser()
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                List<Users> users = recruitment.USERs.Select(e => new Users
                {
                    UserId = e.USER_ID,
                    Username = e.USERNAME,
                    Fullname = e.FULLNAME,
                    Roleid = e.ROLE_ID

                }).ToList();
                List<string> selec = recruitment.ROLEs.Select(x => x.ROLE_ID).ToList();
                TempData["rolesID"] = selec;
                return View("ListUser", users);
            }
        }

        [Route("")]
        [HttpPost]
        [ActionName("NewUser")]
        public ActionResult AddUser(Users user)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                try
                {
                    var count = recruitment.USERs.Where(e => e.USERNAME == user.Username).Count();
                    if (count > 0)
                    {
                        TempData["message"] = "Username has been used.!";
                    }
                    else
                    {
                        if (ModelState.IsValid)
                        {
                            USER AddUsr = new USER
                            {
                                USER_ID = user.UserId,
                                USERNAME = user.Username,
                                FULLNAME = user.Fullname,
                                PASSWORD = MD5Encryption.encryption(user.Password),
                                ROLE_ID = user.Roleid
                            };

                            recruitment.USERs.Add(AddUsr);
                            recruitment.SaveChanges();
                            return Redirect("~/users");
                        }
                    }
                    return View("UserForm");
                }
                catch (Exception err)
                {
                    TempData["message"] = err;
                    return Redirect("~/users");
                }
            }
        }


        [Route("new")]
        [HttpGet]
        public ActionResult NewUser()
        {
            return View("UserForm");
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult GetUser(string id)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                try
                {
                    Users user = recruitment.USERs.Where(e => e.ROLE_ID == id).Select(e =>
                    new Users { Username = e.USERNAME, Fullname = e.FULLNAME, Roleid = e.ROLE_ID }).FirstOrDefault();
                    return View("Users", user);
                }
                catch (Exception err)
                {
                    TempData["message"] = err;
                    return Redirect("~/users");
                }
            }
        }

        [Route("{id}/edit")]
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                try
                {
                    USER usrModel = recruitment.USERs.Find(id);
                    Users users = new Users
                    {
                        UserId = usrModel.USER_ID,
                        Username = usrModel.USERNAME,
                        Fullname = usrModel.FULLNAME,
                        Password = usrModel.PASSWORD,
                        Roleid = usrModel.ROLE_ID
                    };
                    return View("EditForm", users);
                }
                catch (Exception err)
                {
                    TempData["message"] = err;
                    return Redirect("~/users");
                }
            }
        }

        [Route("update")]
        [ActionName("editform")]
        [HttpPost]
        public ActionResult UpdateUser(Users edittedUser)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                ModelState.Remove("Password");
                try
                {
                    if (ModelState.IsValid)
                    {
                        USER addedusr = new USER
                        {
                            USER_ID = edittedUser.UserId,
                            USERNAME = edittedUser.Username,
                            FULLNAME = edittedUser.Fullname,
                            PASSWORD = edittedUser.Password,
                            ROLE_ID = edittedUser.Roleid
                        };
                        recruitment.Entry(addedusr).State = System.Data.Entity.EntityState.Modified;
                        recruitment.SaveChanges();
                        return Redirect("~/users");
                    }
                    return View("EditForm");
                }
                catch (Exception err)
                {
                    TempData["message"] = err;
                    return Redirect("~/users");
                }
            }
        }

        [Route("{id}/delete")]
        [HttpPost]
        public ActionResult DeleteUser(int id)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                try
                {
                    USER user = recruitment.USERs.Find(id);
                    recruitment.USERs.Remove(user);
                    recruitment.SaveChanges();
                    return Redirect("~/users");
                }
                catch (Exception err)
                {
                    TempData["message"] = err;
                    return Redirect("~/users");
                }
            }
        }
    }
}