using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS_API.Controllers;
using OMS_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace testProject {
    public class TestPaisesController {
        [Fact]
        public async Task GetAllPaisesAsync_ShouldReturnAllPaisesAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetAllPaises");
            var _paisesController = new PaisesController(testContext);


            //Act

            var result = await _paisesController.GetPais();

            //Assert

            Assert.IsType<List<Pais>>(result.Value);
        }

        [Fact]
        public async Task GetPaisByIDAsync_ShouldReturnPaisPTAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetByIdPais");
            var _paisesController = new PaisesController(testContext);


            //Act

            var result = await _paisesController.GetPais("PT");

            //Assert

            var item = Assert.IsType<Pais>(result.Value);
            Assert.Equal("PT", item.Id);
            Assert.Equal("Portugal", item.Nome);
        }


        [Fact]
        public async Task PostPaisAsync_ShouldReturnCreatedAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4PostPais");
            var _paisesController = new PaisesController(testContext);


            //Act

            var result = await _paisesController.PostPais(new Pais { Id = "ES", Nome = "Espanha", ZonaId = "EU"});
            var get = await _paisesController.GetPais("ES");

            //Assert

            var item = Assert.IsType<Pais>(get.Value);
            Assert.Equal("Espanha", item.Nome);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PutPaisAsync_ShouldUpdatePaisPTAsync() {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForPaisPutID");
            var _paisesController = new PaisesController(testContext);

            //Act
            var pais = new Pais {
                Id = "PT",
                Nome = "Portugal e Ilhas"
            };
            var getPais = await _paisesController.GetPais("PT");
            var p1 = getPais.Value;
            testContext.Entry(p1).State = EntityState.Detached;
            var result = await _paisesController.PutPais("PT", pais);
            var getresult = await _paisesController.GetPais("PT");

            //Assert
            Assert.IsType<NoContentResult>(result);
            var item = Assert.IsType<Pais>(getresult.Value);
            Assert.Equal("PT", item.Id);
            Assert.Equal("Portugal e Ilhas", item.Nome);
        }

        [Fact]
        public async Task DeletePaisAsync_ShouldDeletePaisPTAsync() {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForPaisDelete");
            var _paisesController = new PaisesController(testContext);

            //Act
            var result = await _paisesController.DeletePais("PT");
            var get = await _paisesController.GetPais("PT");

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            Assert.IsType<NoContentResult>(result);
        }

    }
}
