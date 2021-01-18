using Microsoft.EntityFrameworkCore;
using OMS_API.Data;
using OMS_API.Models;

namespace testProject {
    public static class OMSContextMocker {
        private static OMSContext dbContext;
        public static OMSContext GetOMSContext(string dbName) {
            var options = new DbContextOptionsBuilder<OMSContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
            dbContext = new OMSContext(options);
            Seed();
            return dbContext;
        }
        private static void Seed() {
            dbContext.Virus.Add(new Virus
            {
                Id = 1,
                Nome = "Corona"
            });

            dbContext.Virus.Add(new Virus {
                Id = 2,
                Nome = "Super Death"
            });

            dbContext.Virus.Add(new Virus {
                Id = 3,
                Nome = "Uirusu"
            });

            dbContext.SaveChanges();

        }
    }
}