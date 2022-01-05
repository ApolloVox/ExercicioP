namespace EduardoPrimavera.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using EduardoPrimavera.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EduardoPrimavera.Models.EduardoPrimaveraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EduardoPrimavera.Models.EduardoPrimaveraContext context)
        {

            context.Countries.AddOrUpdate(
              p => p.Id,
              new Country { Id = 1, Name = "Portugal" },
              new Country { Id = 2, Name = "Espanha" },
              new Country { Id = 3, Name = "França" }
            );

            context.Cities.AddOrUpdate(
              p => p.Id,
              new City { Id = 1, Name = "Lisboa", CountryId = 1 },
              new City { Id = 2, Name = "Porto", CountryId = 1 },
              new City { Id = 3, Name = "Madrid", CountryId = 2 },
              new City { Id = 4, Name = "Barcelona", CountryId = 2 },
              new City { Id = 5, Name = "Paris", CountryId = 3 }
            );

            context.Categories.AddOrUpdate(
              p => p.Id,
              new Category { Id = 1, Name = "Saúde"},
              new Category { Id = 2, Name = "Comércio" },
              new Category { Id = 3, Name = "Automóvel" }
            );

            context.Benefits.AddOrUpdate(
              p => p.Id,
              new Benefit { Id = 1, Name = "Farmácia Lamaçães", Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", CityId = 1, CategoryId = 1 },
              new Benefit { Id = 2, Name = "Pharmacie Paris", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", CityId = 5, CategoryId = 1 },
              new Benefit { Id = 3, Name = "Farmacia  Madrid", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", CityId = 3, CategoryId = 1 },
              new Benefit { Id = 4, Name = "Mecánico Barcelona", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", CityId = 5, CategoryId = 3 },
              new Benefit { Id = 5, Name = "Mecânico Porto", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", CityId = 2, CategoryId = 3 },
              new Benefit { Id = 6, Name = "Roupa Barcelona", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.", CityId = 4, CategoryId = 2 }
            );

        }
    }
}
