using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OMS_API.Models;

namespace OMS_API.Data
{
    public class DbInitializer
    {
        public static void Initialize(OMSContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Zona.Any())
            {
                return;   // DB has been seeded
            }

            var zonas = new Zona[]
            {
                new Zona { Id = "eur",   Nome = "Europa" }
            };
            foreach (Zona z in zonas)
            {
                context.Zona.Add(z);
            }
            context.SaveChanges();
        }
    }
}
