using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RecruitmentApi.Models;

namespace RecruitmentApi.Controllers
{
    public class StateController : ApiController
    {
        private RecruitmentEntities db = new RecruitmentEntities();

        // GET: api/state
        public IQueryable<STATE> GetSTATEs()
        {
            return db.STATEs;
        }

        // GET: api/state/5
        [ResponseType(typeof(STATE))]
        public IHttpActionResult GetSTATE(string id)
        {
            STATE sTATE = db.STATEs.Find(id);
            if (sTATE == null)
            {
                return NotFound();
            }

            return Ok(sTATE);
        }

        // PUT: api/state/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSTATE(STATE sTATE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(sTATE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!STATEExists(sTATE.STATE_ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/state
        [ResponseType(typeof(STATE))]
        public IHttpActionResult PostSTATE(STATE sTATE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.STATEs.Add(sTATE);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (STATEExists(sTATE.STATE_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sTATE.STATE_ID }, sTATE);
        }

        // DELETE: api/state/5
        [ResponseType(typeof(STATE))]
        public IHttpActionResult DeleteSTATE(string id)
        {
            STATE sTATE = db.STATEs.Find(id);
            if (sTATE == null)
            {
                return NotFound();
            }

            db.STATEs.Remove(sTATE);
            db.SaveChanges();

            return Ok(sTATE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool STATEExists(string id)
        {
            return db.STATEs.Count(e => e.STATE_ID == id) > 0;
        }
    }
}