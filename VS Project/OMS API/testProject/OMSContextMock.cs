using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using OMS_API.Controllers;
using OMS_API.Data;
using OMS_API.Models;

namespace testProject
{
    public static class OMSContextMock
    {
        private static OMSContext dbContext;
        public static OMSContext GetOMSContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<OMSContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            dbContext = new OMSContext(options);
            Seed();
            return dbContext;
        }
        private static void Seed()
        {
            dbContext.Zonas.Add(new Zona
            {
                Id = "1",
                Nome = "testItem1",
            });
            dbContext.Zonas.Add(new Zona
            {
                Id = "2",
                Nome = "testItem2",
            });
            dbContext.Zonas.Add(new Zona
            {
                Id = "3",
                Nome = "testItem3",
            });
            dbContext.Zonas.Add(new Zona
            {
                Id = "4",
                Nome = "testItem4",
            });
            dbContext.Zonas.Add(new Zona
            {
                Id = "5",
                Nome = "testItem5",
            });
            dbContext.SaveChanges();
        }
        public static class OMSContextMocker
        {
            private static OMSContext dbContext;
            public static OMSContext GetOMSContext(string dbName)
            {
                var options = new DbContextOptionsBuilder<OMSContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;
                dbContext = new OMSContext(options);
                Seed();
                return dbContext;
            }
            private static void Seed()
            {
                dbContext.Zonas.Add(new Zona
                {
                    Id = "AA",
                    Nome = "testZonaAA",
                });
                dbContext.Zonas.Add(new Zona
                {
                    Id = "BB",
                    Nome = "testZonaBB",
                });
                dbContext.Zonas.Add(new Zona
                {
                    Id = "CC",
                    Nome = "testZonaCC",
                });

                dbContext.SaveChanges();
            }
        }
    }
}

