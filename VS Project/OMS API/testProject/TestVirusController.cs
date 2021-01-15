using Microsoft.AspNetCore.Mvc;
using OMS_API.Controllers;
using OMS_API.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestVirusController
    {
        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetAll");
            var _virusController = new VirusController(testContext);


            //Act

            var result = await _virusController.GetVirus();

            //Assert

            var items = Assert.IsType<List<Virus>>(result.Value);
            Assert.Equal(3, items.Count);
            testContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetItemByIdAsync_ShouldReturnFirstItemAsync()
        {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetAll");
            var _virusController = new VirusController(testContext);


            //Act

            var result = await _virusController.GetVirus(1);

            //Assert

            var item = Assert.IsType<Virus>(result.Value);
            Assert.Contains("Corona", item.Nome);
            testContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CreateItemAsync_ShouldReturnItemAsync()
        {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetAll");
            var _virusController = new VirusController(testContext);


            //Act

            var virus = new Virus
            {
                Id = 4,
                Nome = "COVID-19"
            };
            var result = await _virusController.PostVirus(virus);
            var code = result.Result;
            //Assert

            //Assert.IsType<Virus>(result.Value);
            //Assert.Equal(201, code);
            testContext.Database.EnsureDeleted();
        }
    }
}
