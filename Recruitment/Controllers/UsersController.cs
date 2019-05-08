﻿using Recruitment.Models;
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
                    if (ModelState.IsValid)
                    {
                        USER AddUsr = new USER
                        {
                            USERNAME = user.Username,
                            FULLNAME = user.Fullname,
                            PASSWORD = MD5Encryption.encryption(user.Password),
                            ROLE_ID = user.Roleid
                        };
                        recruitment.USERs.Add(AddUsr);
                        recruitment.SaveChanges();
                        return Redirect("~/users");
                    }
                    return View("UserForm");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
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
                Users user = recruitment.USERs.Where(e => e.ROLE_ID == id).Select(e =>
                new Users { Username = e.USERNAME, Fullname = e.FULLNAME, Roleid = e.ROLE_ID }).FirstOrDefault();
                return View("Users", user);
            }
        }

        [Route("{Username}/edit")]
        [HttpGet]
        public ActionResult EditUser(string Username)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                try
                {
                    USER usrModel = recruitment.USERs.Find(Username);
                    Users users = new Users
                    {
                        Username = usrModel.USERNAME,
                        Fullname = usrModel.FULLNAME,
                        Password = usrModel.PASSWORD,
                        Roleid = usrModel.ROLE_ID
                    };
                    return View("EditForm", users);
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        [Route("edit")]
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
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        [Route("{Username}/delete")]
        [HttpPost]
        public ActionResult DeleteUser(string Username)
        {
            using (RecruitmentEntities recruitment = new RecruitmentEntities())
            {
                USER user = recruitment.USERs.Find(Username);
                recruitment.USERs.Remove(user);
                recruitment.SaveChanges();
                return Redirect("~/users");
            }
        }
    }
}