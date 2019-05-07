using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Controllers
{
    [RoutePrefix("source")]
    public class SourceController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return Redirect("~/source/getall");
        }

        [Route("getall")]
        public ActionResult GetAll()
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<Sumber> sumbers = db.SOURCEs.Select(m => new Sumber
                {
                    SumberId = m.SOURCE_ID,
                    SumberNama = m.SOURCE_NAME
                }).ToList();
                TempData["Sumbers"] = sumbers;
                return View("ListSource");
            }
        }

        [Route("get/{id}")]
        public ActionResult GetById(string id)
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<Sumber> sumbers = db.SOURCEs.Select(m => new Sumber
                {
                    SumberId = m.SOURCE_ID,
                    SumberNama = m.SOURCE_NAME
                }).ToList();
                Sumber sumber = sumbers.Find(m => m.SumberId == id);
                TempData["Sumbers"] = sumbers;
                return View("ListSource");
            }
        }

        [Route("post")]
        public ActionResult Adding()
        {
            return View("Source");
        }

        [ActionName("Insert")]
        [HttpPost]
        public ActionResult AddSource(string sumberNama)
        {
            if (!ModelState.IsValid)
            {
                return View("Source");
            }
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<Sumber> sumbers = db.SOURCEs.Select(m => new Sumber
                {
                    SumberId = m.SOURCE_ID,
                    SumberNama = m.SOURCE_NAME
                }).ToList();
                string last = sumbers.Select(m => m.SumberId).LastOrDefault();
                int count = 0;
                if (sumbers.Count != 0)
                {
                    count = int.Parse(last.Substring(last.Length - 3)) + 1;
                }
                string id = "SR" + String.Format("{0:D3}", count);
                SOURCE source = new SOURCE
                {
                    SOURCE_ID = id,
                    SOURCE_NAME = sumberNama
                };
                db.SOURCEs.Add(source);
                db.SaveChanges();
                return Redirect("~/source/getall");
            }
        }

        [Route("put")]
        public ActionResult Editting(string id, string name)
        {
            Sumber sumber = new Sumber
            {
                SumberId = id,
                SumberNama = name
            };
            TempData["Sumber"] = sumber;
            return View("EditSource");
        }


        [ActionName("Update")]
        public ActionResult EditSource(Sumber sumberEdit)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("~/source");
            }
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                SOURCE sourceEdit = new SOURCE
                {
                    SOURCE_ID = sumberEdit.SumberId,
                    SOURCE_NAME = sumberEdit.SumberNama
                };
                SOURCE source = db.SOURCEs.Where(m => m.SOURCE_ID == sourceEdit.SOURCE_ID).FirstOrDefault();
                source.SOURCE_NAME = sourceEdit.SOURCE_NAME;
                db.SaveChanges();
                return Redirect("~/source");
            }
        }

        [Route("delete")]
        public ActionResult DeleteSource(string id)
        {
            using (RecruitmentEntities db = new RecruitmentEntities())
            {
                List<Sumber> sumbers = db.SOURCEs.Select(m => new Sumber
                {
                    SumberId = m.SOURCE_ID,
                    SumberNama = m.SOURCE_NAME
                }).ToList();
                Sumber sumberDelete = sumbers.Find(m => m.SumberId == id);
                SOURCE sourceDelete = new SOURCE
                {
                    SOURCE_ID = sumberDelete.SumberId,
                    SOURCE_NAME = sumberDelete.SumberNama
                };
                db.SOURCEs.Attach(sourceDelete);
                db.SOURCEs.Remove(sourceDelete);
                db.SaveChanges();
                return Redirect("~/source");
            }
        }
    }
}