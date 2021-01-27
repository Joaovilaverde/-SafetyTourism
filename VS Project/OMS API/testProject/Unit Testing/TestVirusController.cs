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
    public class TestVirusController
    {
        [Fact]
        public async Task GetAllVirusAsync_ShouldReturnAllVirusAsync()
        {
            Thread.Sleep(6000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForVirusGetAll");
            var testController = new VirusController(testContext);

            //Act
            var result = await testController.GetVirus();

            //Assert
            var items = Assert.IsType<List<Virus>>(result.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetVirusbyIDAsync_ShouldReturnVirus2Async()
        {
            Thread.Sleep(6000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForVirusGetID");
            var testController = new VirusController(testContext);

            //Act
            var result = await testController.GetVirus(2);

            //Assert
            var items = Assert.IsType<Virus>(result.Value);
            Assert.Equal(2, items.Id);
        }
        [Fact]
        public async Task PutVirusbyIDAsync_ShouldUpdateVirus3Async()
        {
            Thread.Sleep(6000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForVirusPutID");
            var testController = new VirusController(testContext);

            //Act
            var getVirus = await testController.GetVirus(3);
            var virus = getVirus.Value;
            virus.Nome = "TestVirus3Updated";
            var result = await testController.PutVirus(3, virus);
            var getresult = await testController.GetVirus(3);

            //Assert
            var items = Assert.IsType<Virus>(getresult.Value);
            Assert.Equal("TestVirus3Updated", items.Nome);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PostVirusAsync_ShouldCreateNewVirusAsync()
        {
            Thread.Sleep(6000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForVirusPost");
            var testController = new VirusController(testContext);

            //Act
            var result = await testController.PostVirus(new Virus { Id = 4, Nome = "TestVirus4" });
            var get = await testController.GetVirus(4);

            //Assert
            Assert.IsType<Virus>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public async Task DeleteVirusAsync_ShouldDeleteVirus2Async()
        {
            Thread.Sleep(6000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForVirusDelete");
            var testController = new VirusController(testContext);

            //Act
            var result = await testController.DeleteVirus(2);
            var get = await testController.GetVirus(2);

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
