using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using OMS_API.Data;
using OMS_API.Controllers;
using OMS_API.Models;
using static testProject.OMSContextMock;

namespace testProject
{
    public class TestTodoController
    {
        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync()
        {
            //Arrange
            var TestContext = OMSContextMock.GetOMSContext("DBTest4GetAll");
            var theController = new ZonasController(TestContext);

            //Act
            var Result = await theController.GetZona();

            //Assert
            var items = Assert.IsType<List<Zona>>(Result.Value);
            Assert.Equal(8, items.Count);

        }
        [Fact]
        public async Task GetZonaByIDAsync_ShouldReturnZonaBBAsync()
        {
            //Arrange
            var TestContext = OMSContextMocker.GetOMSContext("DBTest4GetAll");
            var theController = new ZonasController(TestContext);

            //Act
            var Result = await theController.GetZona("BB");

            //Assert
            var items = Assert.IsType<Zona>(Result.Value);
            Assert.Equal("BB", items.Id);

        }
        [Fact]
        public async Task PostZona()
        {
            //Arrange
            var TestContext = OMSContextMocker.GetOMSContext("DBTest4GetAll");
            var theController = new ZonasController(TestContext);

            //Act
            var Result = await theController.PostZona();

            //Assert
            var items = Assert.IsType<Zona>(Result.Value);
            Assert.Equal("BB", items.Id);

        }
    }
}
