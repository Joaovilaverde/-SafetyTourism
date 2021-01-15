using Microsoft.EntityFrameworkCore;
using OMS_API.Controllers;
using OMS_API.Data;
using OMS_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TesteZonasController
    {
        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync()
        {
            //Arrange
            var TestContext = OmsContextMocker.GetOMSContext("TesteTesteTeste");
            var testController = new ZonasController(TestContext);

            //Act
            var result = await testController.GetZona();

            //Assert
            var items = Assert.IsType<List<Zona>>(result.Value);
            Assert.Equal(3, items.Count);

        }

    }
    public static class OmsContextMocker
    {
        private static OMSContext dbContext;
        public static OMSContext GetOMSContext (string dbTeste)
        {
            var options = new DbContextOptionsBuilder<OMSContext>()
                .UseInMemoryDatabase(databaseName: dbTeste)
                .Options;

            OMSContext mSContext = new OMSContext(options);
            Seed();
            return dbContext;
        }
        private static void Seed() { }
    }
}
