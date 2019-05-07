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
    public class RoleController : ApiController
    {
        private RecruitmentEntities db = new RecruitmentEntities();

        // GET: api/Role
        public IEnumerable<Roles> GetROLEs()
        {
            List<Roles> roles = db.ROLEs.Select(e => new Roles
            {
                RoleId = e.ROLE_ID,
                RoleName = e.ROLE_NAME
            }).ToList();

            return (roles);
        }

        [Route("api/MaxId")]
        public IHttpActionResult GetMaxId()
        {
            List<Roles> roles = db.ROLEs.Select(x => new Roles
            {
                RoleId = x.ROLE_ID,
                RoleName = x.ROLE_NAME
            }).ToList();

            string maxId = roles.Max(x => x.RoleId);
            int angka = 0;
            if (roles.Count != 0)
            {
                angka = int.Parse(maxId.Substring(maxId.Length - 3)) + 1;
            }
            string newRole = "RL" + string.Format("{0:D3}", angka);

            Roles ListRole = new Roles
            {
                RoleId = newRole,
                RoleName = null
            };
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(ListRole);
        }
        
        // GET: api/Role/5
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult GetROLE(string id)
        {
            ROLE rOLE = db.ROLEs.Find(id);
            Roles roles = new Roles
            {
                RoleId = rOLE.ROLE_ID,
                RoleName = rOLE.ROLE_NAME
            };
            if (rOLE == null)
            {
                return NotFound();
            }

            return Ok(roles);
        }

        // PUT: api/Role/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutROLE(string id, Roles roles)
        {
            ROLE role = new ROLE
            {
                ROLE_ID = roles.RoleId,
                ROLE_NAME = roles.RoleName
            };
            if (id != roles.RoleId)
            {
                return BadRequest();
            }

            db.Entry(role).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ROLEExists(id))
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

        // POST: api/Role
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult PostROLE(Roles roles)
        {
            ROLE rOLE = new ROLE
            {
                ROLE_ID = roles.RoleId,
                ROLE_NAME = roles.RoleName
            };
            db.ROLEs.Add(rOLE);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ROLEExists(roles.RoleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = roles.RoleId }, rOLE);
        }

        // DELETE: api/Role/5
        [ResponseType(typeof(ROLE))]
        public IHttpActionResult DeleteROLE(string id)
        {
            ROLE rOLE = db.ROLEs.Find(id);
            if (rOLE == null)
            {
                return NotFound();
            }

            db.ROLEs.Remove(rOLE);
            db.SaveChanges();

            return Ok(rOLE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ROLEExists(string id)
        {
            return db.ROLEs.Count(e => e.ROLE_ID == id) > 0;
        }
    }
}