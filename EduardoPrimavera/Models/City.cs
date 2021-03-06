using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EduardoPrimavera.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        // Foreign Key
        public int CountryId { get; set; }
        // Navigation property
        public virtual Country Country { get; set; }
    }
}