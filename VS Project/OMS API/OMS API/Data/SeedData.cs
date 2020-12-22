using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OMS_API.Entities;
using OMS_API.Models;

namespace OMS_API.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OMSContext(serviceProvider.GetRequiredService<DbContextOptions<OMSContext>>()))
            {
                // Look for any Zonas.
                if (context.Virus.Any())
                {
                    return;   // DB has been seeded
                }

                context.Zonas.AddRange(
                    new Zona { Id = "eur", Nome = "Europa" },
                    new Zona { Id = "asi", Nome = "Ásia" },
                    new Zona { Id = "afr", Nome = "África" },
                    new Zona { Id = "amn", Nome = "América do Norte" },
                    new Zona { Id = "ams", Nome = "América do Sul" },
                    new Zona { Id = "amc", Nome = "América Central" },
                    new Zona { Id = "ant", Nome = "Antárctida" },
                    new Zona { Id = "oce", Nome = "Oceânia" }
                );
                context.Paises.AddRange(
                    new Pais { Id = "pt", Nome = "Portugal", ZonaId = "eur" },
                    new Pais { Id = "es", Nome = "Espanha", ZonaId = "eur" },
                    new Pais { Id = "ang", Nome = "Angola", ZonaId = "afr" },
                    new Pais { Id = "moc", Nome = "Moçambique", ZonaId = "afr" }
                );
                context.Recomendacoes.AddRange(
                    new Recomendacao {ZonaId = "eur", Data = DateTime.Parse("2020-2-12"), Validade = 10, Informacao = "Lavar as mãos" },
                    new Recomendacao {ZonaId = "afr", Data = DateTime.Parse("2020-2-12"), Validade = 10, Informacao = "Aceitar a morte certa" }
                );
                context.Virus.AddRange(
                    new Virus {Nome = "Covid" },
                    new Virus {Nome = "Pinguins" },
                    new Virus { Nome = "Americanos" },
                    new Virus {Nome = "Malária" }
                );
                context.Surtos.AddRange(
                    new Surto {VirusId = 1, ZonaId = "eur", DataDetecao = DateTime.Parse("2020-12-13"), DataFim = DateTime.Parse("2020-12-20") },
                    new Surto {VirusId = 1, ZonaId = "eur", DataDetecao = DateTime.Parse("2020-12-13"), DataFim = DateTime.Parse("2020-12-20") },
                    new Surto {VirusId = 2, ZonaId = "eur", DataDetecao = DateTime.Parse("2020-11-21") },
                    new Surto {VirusId = 2, ZonaId = "ant", DataDetecao = DateTime.Parse("2020-10-09"), DataFim = DateTime.Parse("2020-11-09") },
                    new Surto {VirusId = 2, ZonaId = "ant", DataDetecao = DateTime.Parse("2020-12-21") },
                    new Surto {VirusId = 3, ZonaId = "oce", DataDetecao = DateTime.Parse("2020-12-13"), DataFim = DateTime.Parse("2020-12-20") },
                    new Surto {VirusId = 3, ZonaId = "amn", DataDetecao = DateTime.Parse("2020-12-13"), DataFim = DateTime.Parse("2020-12-20") },
                    new Surto {VirusId = 3, ZonaId = "ams", DataDetecao = DateTime.Parse("2020-12-20") },
                    new Surto {VirusId = 4, ZonaId = "afr", DataDetecao = DateTime.Parse("2020-2-12") }
                );
                context.Users.AddRange(
                    new User {FirstName = "Inês", LastName = "Oliveira",  Username = "arya", Password = "admin"}
                );
                context.SaveChanges();
            }
        }
    }
}
