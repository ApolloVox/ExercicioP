using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduardoPrimavera.Models
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Foreign Key
        public int BenefitId { get; set; }
        // Navigation property
        public Benefit Benefit { get; set; }
    }
}