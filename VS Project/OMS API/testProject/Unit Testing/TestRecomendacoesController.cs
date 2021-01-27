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
    public class TestRecomendacoesController
    {
        [Fact]
        public async Task GetAllRecomendacoesAsync_ShouldReturnAllRecomendacoesAsync()
        {
            Thread.Sleep(4000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForRecomendacoesGetAll");
            var testController = new RecomendacoesController(testContext);

            //Act
            var result = await testController.GetRecomendacao();

            //Assert
            var items = Assert.IsType<List<Recomendacao>>(result.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public async Task GetRecomendacaobyIDAsync_ShouldReturnRecomendacao2Async()
        {
            Thread.Sleep(4000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForRecomendacoesGetID");
            var testController = new RecomendacoesController(testContext);

            //Act
            var result = await testController.GetRecomendacao(2);

            //Assert
            var items = Assert.IsType<Recomendacao>(result.Value);
            Assert.Equal(2, items.Id);
        }
        [Fact]
        public async Task GetRecomendacaoByPaisAsync_ShouldReturnAllRecomendacoesForPaisAAAsync()
        {
            Thread.Sleep(4000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForRecomendacoesGetByPais");
            var testController = new RecomendacoesController(testContext);

            //Act
            var result = await testController.GetRecomendacaoByPaisAsync("AA");

            //Assert
            Assert.IsAssignableFrom<IQueryable<Recomendacao>>(result);
            var list = result.ToList();
            Assert.Equal(3, list.Count);
        }
        [Fact]
        public async Task PutRecomendacaobyIDAsync_ShouldUpdateRecomendacao3Async()
        {
            Thread.Sleep(4000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForRecomendacoesPutID");
            var testController = new RecomendacoesController(testContext);

            //Act
            var getRecomendacao = await testController.GetRecomendacao(3);
            var recomendacao = getRecomendacao.Value;
            recomendacao.ZonaId = "CC";
            recomendacao.Data = DateTime.Parse("2022-1-1");
            recomendacao.Validade = DateTime.Parse("2022-12-31");
            recomendacao.Informacao = "TestRecomendacao3Updated";
            var result = await testController.PutRecomendacao(3, recomendacao);
            var getresult = await testController.GetRecomendacao(3);

            //Assert
            var items = Assert.IsType<Recomendacao>(getresult.Value);
            Assert.Equal("CC", items.ZonaId);
            Assert.Equal(DateTime.Parse("2022-1-1"), items.Data);
            Assert.Equal(DateTime.Parse("2022-12-31"), items.Validade);
            Assert.Equal("TestRecomendacao3Updated", items.Informacao);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task PostRecomendacaoAsync_ShouldCreateNewRecomendacaoAsync()
        {
            Thread.Sleep(4000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForRecomendacoesPost");
            var testController = new RecomendacoesController(testContext);

            //Act
            var result = await testController.PostRecomendacao(new Recomendacao { Id = 4, ZonaId = "AA", Data = DateTime.Parse("2021-1-1"), Validade = DateTime.Parse("2021-12-31"), Informacao = "TestRecomendacao4" });
            var get = await testController.GetRecomendacao(4);

            //Assert
            Assert.IsType<Recomendacao>(get.Value);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }
        [Fact]
        public async Task DeleteRecomendacaoAsync_ShouldDeleteRecomendacao2Async()
        {
            Thread.Sleep(4000);
            //Arrange
            var testContext = OMSContextMocker.GetOMSContext("DBTestForRecomendacoesDelete");
            var testController = new RecomendacoesController(testContext);

            //Act
            var result = await testController.DeleteRecomendacao(2);
            var get = await testController.GetRecomendacao(2);

            //Assert
            Assert.IsType<NotFoundResult>(get.Result);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
