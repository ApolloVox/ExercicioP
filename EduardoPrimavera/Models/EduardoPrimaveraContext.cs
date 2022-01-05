using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EduardoPrimavera.Models
{
    public class EduardoPrimaveraContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EduardoPrimaveraContext() : base("name=EduardoPrimaveraContext")
        {
        }

        public System.Data.Entity.DbSet<EduardoPrimavera.Models.Benefit> Benefits { get; set; }

        public System.Data.Entity.DbSet<EduardoPrimavera.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<EduardoPrimavera.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<EduardoPrimavera.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<EduardoPrimavera.Models.Document> Documents { get; set; }
    }
}
