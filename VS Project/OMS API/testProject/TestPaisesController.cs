using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OMS_API.Controllers;
using OMS_API.Models;
using OMS_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;


/*namespace testProject
{
    class TestPaisesController
    {
        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync()
        {
            //Arrange
            var TestContext = OMSContextMock.GetOMSContext("DBTest4GetAll");
            var theController = new PaisesController(TestContext);

            //Act
            var Result = await theController.GetPais();

            //Assert
            var items = Assert.IsType<List<Pais>>(Result.Value);
            Assert.Equal(5, items.Count);

        }
    }
}
*/