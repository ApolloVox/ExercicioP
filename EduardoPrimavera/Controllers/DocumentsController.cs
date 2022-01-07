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
    public class DocumentsController : ApiController
    {
        private EduardoPrimaveraContext db = new EduardoPrimaveraContext();

        // GET: api/Documents
        public IQueryable<Document> GetDocuments()
        {
            return db.Documents;
        }

        // GET: api/Documents/5
        [ResponseType(typeof(Document))]
        public async Task<IHttpActionResult> GetDocument(int id)
        {
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }


        // PUT: api/Documents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDocument(int id, Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != document.Id)
            {
                return BadRequest();
            }

            db.Entry(document).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
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

        // POST: api/Documents
        [ResponseType(typeof(Document))]
        public async Task<IHttpActionResult> PostDocument(Document document)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Documents.Add(document);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [ResponseType(typeof(Document))]
        public async Task<IHttpActionResult> DeleteDocument(int id)
        {
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            db.Documents.Remove(document);
            await db.SaveChangesAsync();

            return Ok(document);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DocumentExists(int id)
        {
            return db.Documents.Count(e => e.Id == id) > 0;
        }
    }
}