using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS_API.Controllers;
using OMS_API.Data;
using OMS_API.Models;
using Xunit;

namespace testProject
{
    public class TestPaisesController
    {
        [Fact]
        public async Task GetAllPaisesAsync_ShouldReturnAllPaisesAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForPaisesGetAll");
            var testController = new PaisesController(testContext);

            //Act
            var result = await testController.GetPais();

            //Assert
            var items = Assert.IsType<List<Pais>>(result.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetPaisbyIDAsync_ShouldReturnPaisBBAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForPaisesGetID");
            var testController = new PaisesController(testContext);

            //Act
            var result = await testController.GetPais("BB");

            //Assert
            var items = Assert.IsType<Pais>(result.Value);
            Assert.Equal("BB", items.Id);
        }
        [Fact]
        public async Task PutPaisbyIDAsync_ShouldUpdatePaisCCAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForPaisesPutID");
            var testController = new PaisesController(testContext);

            //Act
            var getPais = await testController.GetPais("CC");
            var pais = getPais.Value;
            pais.Nome = "TestPaisCCUpdated";
            pais.ZonaId = "CC";
            var result = await testController.PutPais("CC", pais);
            var getresult = await testController.GetPais("CC");

            //Assert
            var items = Assert.IsType<Pais>(getresult.Value);
            Assert.Equal("TestPaisCCUpdated", items.Nome);
            Assert.Equal("CC", items.ZonaId);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PostPaisAsync_ShouldCreateNewPaisAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForPaisesPost");
            var testController = new PaisesController(testContext);

            //Act
            var result = await testController.PostPais(new Pais { Id = "DD", Nome = "TestPaisDD", ZonaId = "CC" });
            var get = await testController.GetPais("DD");

            //Assert
            Assert.IsType<Pais>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public async Task DeletePaisAsync_ShouldDeletePaisBBAsync()
        {
            Thread.Sleep(2000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForPaisesDelete");
            var testController = new PaisesController(testContext);

            //Act
            var result = await testController.DeletePais("BB");
            var get = await testController.GetPais("BB");

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
