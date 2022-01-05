using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EduardoPrimavera.Models
{
    public class Benefit
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }
        // Navigation property
        public virtual Category Category { get; set; }
        // Foreign Key
        public int CityId { get; set; }
        // Navigation property
        public virtual City City { get; set; }
    }
}