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
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync() {
            //Arrange

            var testContext = OMSContextMocker.GetOMSContext("DBTest4GetAll");
            var _virusController = new VirusController(testContext);


            //Act

            var result = await _virusController.GetVirus();

            //Assert

            Assert.IsType<List<Virus>>(result.Value);
        }
    }
}
