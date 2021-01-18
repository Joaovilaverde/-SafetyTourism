using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS_API.Controllers;
using OMS_API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestVirusController
    {
        [Fact]
        public async Task GetAllVirusesAsync_ShouldReturnAllVirusesAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetAll");
            var _virusController = new VirusController(testContext);


            //Act

            var result = await _virusController.GetVirus();

            //Assert

            Assert.IsType<List<Virus>>(result.Value);
        }

        [Fact]
        public async Task GetVirusByIDAsync_ShouldReturnVirusOneAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetById");
            var _virusController = new VirusController(testContext);


            //Act

            var result = await _virusController.GetVirus(1);

            //Assert

            var item = Assert.IsType<Virus>(result.Value);
            Assert.Equal(1, item.Id);
            Assert.Equal("Corona", item.Nome);
        }


        [Fact]
        public async Task PostItemAsync_ShouldReturnCreatedAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4PostVirus");
            var _virusController = new VirusController(testContext);


            //Act

            var result = await _virusController.PostVirus(new Virus{ Id = 4, Nome = "testvirus"});
            var get = await _virusController.GetVirus(4);

            //Assert

            var item = Assert.IsType<Virus>(get.Value);
            Assert.Equal("testvirus", item.Nome);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task PutVirusAsync_ShouldUpdateVirus1Async() {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForVirusPutID");
            var testController = new VirusController(testContext);

            //Act
            var virus = new Virus {
                Id = 1,
                Nome = "COVID-19"
            };
            var getVirus = await testController.GetVirus(1);
            var v1 = getVirus.Value;
            testContext.Entry(v1).State = EntityState.Detached;
            var result = await testController.PutVirus(1, virus);
            var getresult = await testController.GetVirus(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
            var item = Assert.IsType<Virus>(getresult.Value);
            Assert.Equal(1, item.Id);
            Assert.Equal("COVID-19", item.Nome);
        }

        [Fact]
        public async Task DeleteVirusAsync_ShouldDeleteVirus1Async() {
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForVirusDelete");
            var testController = new VirusController(testContext);

            //Act
            var result = await testController.DeleteVirus(1);
            var get = await testController.GetVirus(1);

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            Assert.IsType<NoContentResult>(result);
        }

    }
}
