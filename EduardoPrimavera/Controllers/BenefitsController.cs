using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EduardoPrimavera.Models;

namespace EduardoPrimavera.Controllers
{
    public class BenefitsController : ApiController
    {
        private EduardoPrimaveraContext db = new EduardoPrimaveraContext();
        private SessionController sessionControler = new SessionController();

        // GET: api/Benefits
        public IQueryable<Benefit> GetBenefits()
        {
            return db.Benefits;
        }

        // GET: api/Benefits/5
        [ResponseType(typeof(Benefit))]
        public async Task<IHttpActionResult> GetBenefit(int id)
        {
            if (!sessionControler.SessionAuthentication(Request))
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            Benefit benefit = await db.Benefits.FindAsync(id);
            if (benefit == null)
            {
                return NotFound();
            }

            return Ok(benefit);
        }

        // PUT: api/Benefits/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBenefit(int id, Benefit benefit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != benefit.Id)
            {
                return BadRequest();
            }

            db.Entry(benefit).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BenefitExists(id))
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

        // POST: api/Benefits
        [ResponseType(typeof(Benefit))]
        public async Task<IHttpActionResult> PostBenefit(Benefit benefit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Benefits.Add(benefit);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = benefit.Id }, benefit);
        }

        // DELETE: api/Benefits/5
        [ResponseType(typeof(Benefit))]
        public async Task<IHttpActionResult> DeleteBenefit(int id)
        {
            Benefit benefit = await db.Benefits.FindAsync(id);
            if (benefit == null)
            {
                return NotFound();
            }

            db.Benefits.Remove(benefit);
            await db.SaveChangesAsync();

            return Ok(benefit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BenefitExists(int id)
        {
            return db.Benefits.Count(e => e.Id == id) > 0;
        }
    }
}