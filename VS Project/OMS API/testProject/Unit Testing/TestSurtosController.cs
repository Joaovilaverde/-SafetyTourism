using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TestSurtosController
    {
        [Fact]
        public async Task GetAllSurtosAsync_ShouldReturnAllSurtosAsync()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosGetAll");
            var testController = new SurtosController(testContext);

            //Act
            var result = await testController.GetSurto();

            //Assert
            var items = Assert.IsType<List<Surto>>(result.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetSurtobyIDAsync_ShouldReturnSurto2Async()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosGetID");
            var testController = new SurtosController(testContext);

            //Act
            var result = await testController.GetSurto(2);

            //Assert
            var items = Assert.IsType<Surto>(result.Value);
            Assert.Equal(2, items.Id);
        }
        [Fact]
        public async Task GetSurtoByPaisAsync_ShouldReturnAllActiveSurtosForPaisAAAsync_ActiveMeansDataFimIsNull()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosGetByPais");
            var testController = new SurtosController(testContext);

            //Act
            var result = await testController.GetSurtoByPaisAsync("AA");

            //Assert
            Assert.IsAssignableFrom<IQueryable<Surto>>(result);
            var list = result.ToList();
            Assert.Equal(2, list.Count);
        }
        [Fact]
        public void GetSurtoByVirusAsync_ShouldReturnAllActiveSurtosForVirus1Async_ActiveMeansDataFimIsNull()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosGetByVirus");
            var testController = new SurtosController(testContext);

            //Act
            var result = testController.GetVirusById(1);

            //Assert
            Assert.IsAssignableFrom<IQueryable<Surto>>(result);
            var list = result.ToList();
            Assert.Equal(2, list.Count);
        }
        [Fact]
        public void GetSurtoByVirusAsync_ShouldReturnAllSurtosForVirus1Async()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosGetAllByVirus");
            var testController = new SurtosController(testContext);

            //Act
            var result = testController.GetSurtosById(1);

            //Assert
            Assert.IsAssignableFrom<IQueryable<Surto>>(result);
            var list = result.ToList();
            Assert.Equal(3, list.Count);
        }
        [Fact]
        public async Task PutSurtobyIDAsync_ShouldUpdateSurto3Async()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosPutID");
            var testController = new SurtosController(testContext);

            //Act
            var getSurto = await testController.GetSurto(3);
            var surto = getSurto.Value;
            surto.VirusId = 3;
            surto.ZonaId = "CC";
            surto.DataDetecao = DateTime.Parse("2021-12-1");
            surto.DataFim = DateTime.Parse("2021-12-31");
            var result = await testController.PutSurto(3, surto);
            var getresult = await testController.GetSurto(3);

            //Assert
            var items = Assert.IsType<Surto>(getresult.Value);
            Assert.Equal(3, items.VirusId);
            Assert.Equal("CC", items.ZonaId);
            Assert.Equal(DateTime.Parse("2021-12-1"), items.DataDetecao);
            Assert.Equal(DateTime.Parse("2021-12-31"), items.DataFim);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PutSurtoDataFimAsync_ShouldUpdateDataFimInSurto3Async()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosPutDataFim");
            var testController = new SurtosController(testContext);

            //Act
            var getsurto = await testController.GetSurto(3);
            var surto = getsurto.Value;
            surto.DataFim = DateTime.Parse("2021-12-31");
            var result = await testController.DataFim(surto.ZonaId, surto.VirusId, surto);
            var getresult = await testController.GetSurto(3);

            //Assert
            var items = Assert.IsType<Surto>(getresult.Value);
            Assert.Equal(DateTime.Parse("2021-12-31"), items.DataFim);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PostSurtoAsync_ShouldCreateNewSurtoAsync()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosPost");
            var testController = new SurtosController(testContext);

            //Act
            var result = await testController.PostSurto(new Surto { Id = 4, VirusId = 3, ZonaId = "CC", DataDetecao = DateTime.Parse("2021-12-1"), DataFim = DateTime.Parse("2021-12-31") });
            var get = await testController.GetSurto(4);

            //Assert
            Assert.IsType<Surto>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public async Task DeleteSurtoAsync_ShouldDeleteSurto2Async()
        {
            Thread.Sleep(8000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForSurtosDelete");
            var testController = new SurtosController(testContext);

            //Act
            var result = await testController.DeleteSurto(2);
            var get = await testController.GetSurto(2);

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
