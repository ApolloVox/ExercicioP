using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
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

        private static readonly Expression<Func<Document, DocumentDTO>> AsDocumentDTO =
            x => new DocumentDTO
            {
                Id = x.Id,
                Name = x.Name,
                BenefitId = x.BenefitId
            };

        // GET: api/CustomeDoc/5
        public IQueryable<DocumentDTO> GetDocument(int id)
        {
            var teste = db.Documents.
                Include(b => b.BenefitId).
                Where(b => b.BenefitId == id).
                Select(AsDocumentDTO);

            return teste;
        }
    }
}