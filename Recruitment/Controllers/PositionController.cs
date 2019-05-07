using Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruitment.Controllers
{
    public class PositionController : Controller
    {
        // GET: Position
        [Route("list")]
        public ActionResult ListPosition()
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                List<PositionPoco> pp = re.POSITIONs.Select(e =>
                new PositionPoco
                {
                    IdPosisi = e.POSITION_ID,
                    Nama = e.POSITION_NAME
                }).ToList();
                return View("ListPosition", pp);
            }
        }

        [HttpGet]
        [ActionName("AddPosition")]
        public ActionResult NewPosition()
        {
            return View("NewPosition");
        }


        [HttpPost]
        [ActionName("NewPosition")]
        public ActionResult NewPosition(PositionPoco pp)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                POSITION posisi = new POSITION
                {
                    POSITION_ID = pp.IdPosisi,
                    POSITION_NAME = pp.Nama
                };
                re.POSITIONs.Add(posisi);
                re.SaveChanges();
                return Redirect("~/list");
            }
        }

        [Route("{id}/edit")]
        [HttpGet]
        [ActionName("editposition")]
        public ActionResult EditPosition(string id)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                POSITION posisi = re.POSITIONs.Find(id);
                PositionPoco position = new PositionPoco
                {
                    IdPosisi = posisi.POSITION_ID,
                    Nama = posisi.POSITION_NAME
                };

                return View("EditPosition", position);
            }
        }

        [Route("edit")]
        [ActionName("UpdatePosition")]
        [HttpPost]
        public ActionResult UpdatePosition(PositionPoco edittedposition)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                POSITION addedPosition = new POSITION
                {
                    POSITION_ID = edittedposition.IdPosisi,
                    POSITION_NAME = edittedposition.Nama
                };
                re.Entry(addedPosition).State = System.Data.Entity.EntityState.Modified;
                re.SaveChanges();
                return Redirect("~/list");
            }
        }

        [Route("delete/{id}")]
        [HttpPost]
        public ActionResult DeletePosition(string id)
        {
            using (RecruitmentEntities re = new RecruitmentEntities())
            {
                POSITION posisi = re.POSITIONs.Find(id);
                re.POSITIONs.Remove(posisi);
                re.SaveChanges();
                return Redirect("~/list");
            }

        }
    }
}