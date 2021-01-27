using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OMS_API.Data;
using OMS_API.Models;

namespace testProject
{
    class OMSContextMocker
    {
        private static OMSContext dbContext;
        public static OMSContext GetOMSContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<OMSContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;
            dbContext = new OMSContext(options);
            Seed();
            return dbContext;
        }
        private static void Seed()
        {
            dbContext.Zonas.Add(new Zona { Id = "AA", Nome = "TestZonaAA" });
            dbContext.Zonas.Add(new Zona { Id = "BB", Nome = "TestZonaBB" });
            dbContext.Zonas.Add(new Zona { Id = "CC", Nome = "TestZonaCC" });
            dbContext.Paises.Add(new Pais { Id = "AA", Nome = "TestPaisAA", ZonaId = "AA" });
            dbContext.Paises.Add(new Pais { Id = "BB", Nome = "TestPaisBB", ZonaId = "AA" });
            dbContext.Paises.Add(new Pais { Id = "CC", Nome = "TestPaisCC", ZonaId = "AA" });
            dbContext.Virus.Add(new Virus { Id = 1, Nome = "TestVirus1" });
            dbContext.Virus.Add(new Virus { Id = 2, Nome = "TestVirus2" });
            dbContext.Virus.Add(new Virus { Id = 3, Nome = "TestVirus3" });
            dbContext.Recomendacoes.Add(new Recomendacao { Id = 1, ZonaId = "AA", Data = DateTime.Parse("2021-1-1"), Validade = DateTime.Parse("2021-12-31"), Informacao = "TestRecomendacao1" });
            dbContext.Recomendacoes.Add(new Recomendacao { Id = 2, ZonaId = "AA", Data = DateTime.Parse("2021-1-1"), Validade = DateTime.Parse("2021-12-31"), Informacao = "TestRecomendacao2" });
            dbContext.Recomendacoes.Add(new Recomendacao { Id = 3, ZonaId = "AA", Data = DateTime.Parse("2021-1-1"), Validade = DateTime.Parse("2021-12-31"), Informacao = "TestRecomendacao3" });
            dbContext.Surtos.Add(new Surto { Id = 1, VirusId = 1, ZonaId = "AA", DataDetecao = DateTime.Parse("2021-1-1"), DataFim = DateTime.Parse("2021-1-31") });
            dbContext.Surtos.Add(new Surto { Id = 2, VirusId = 1, ZonaId = "AA", DataDetecao = DateTime.Parse("2021-1-1") });
            dbContext.Surtos.Add(new Surto { Id = 3, VirusId = 1, ZonaId = "AA", DataDetecao = DateTime.Parse("2021-1-1") });
            dbContext.SaveChanges();
        }
    }
}