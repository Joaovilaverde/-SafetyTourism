using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS_API.Controllers;
using OMS_API.Data;
using OMS_API.Models;
using Xunit;

namespace testProject
{
    public class TestZonasController
    {
        [Fact]
        public async Task GetAllZonasAsync_ShouldReturnAllZonasAsync()
        {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForZonasGetAll");
            var testController = new ZonasController(testContext);

            //Act
            var result = await testController.GetZona();

            //Assert
            var items = Assert.IsType<List<Zona>>(result.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetZonabyIDAsync_ShouldReturnZonaBBAsync()
        {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForZonasGetID");
            var testController = new ZonasController(testContext);

            //Act
            var result = await testController.GetZona("BB");

            //Assert
            var items = Assert.IsType<Zona>(result.Value);
            Assert.Equal("BB", items.Id);
        }
        [Fact]
        public async Task PutZonabyIDAsync_ShouldUpdateZonaCCAsync()
        {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForZonasPutID");
            var testController = new ZonasController(testContext);

            //Act
            var getZona = await testController.GetZona("CC");
            var zona = getZona.Value;
            zona.Nome = "TestZonaCCUpdated";
            var result = await testController.PutZona("CC", zona);
            var getresult = await testController.GetZona("CC");

            //Assert
            var items = Assert.IsType<Zona>(getresult.Value);
            Assert.Equal("TestZonaCCUpdated", items.Nome);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PostZonaAsync_ShouldCreateNewZonaAsync()
        {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForZonasPost");
            var testController = new ZonasController(testContext);

            //Act
            var result = await testController.PostZona(new Zona { Id = "DD", Nome = "TestZonaDD" });
            var get = await testController.GetZona("DD");

            //Assert
            Assert.IsType<Zona>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public async Task DeleteZonaAsync_ShouldDeleteZonaBBAsync()
        {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForZonasDelete");
            var testController = new ZonasController(testContext);

            //Act
            var result = await testController.DeleteZona("BB");
            var get = await testController.GetZona("BB");

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
