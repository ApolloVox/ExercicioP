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
    public class CustomeDocController : ApiController
    {
        private EduardoPrimaveraContext db = new EduardoPrimaveraContext();

        // GET: api/CustomeDoc/5
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
    }
}