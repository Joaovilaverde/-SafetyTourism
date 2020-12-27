using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SafetyTourism.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SafetyTourism.Controllers
{
    public class RecomendacoesController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        // Construtor do controller
        public RecomendacoesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET RECOMENDAÇÕES NO INDEX A APARECER
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NomeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewData["ZonaSortParm"] = sortOrder == "zona" ? "zona_desc" : "zona";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var listaRecomendacoes = new List<Recomendacao>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/recomendacoes";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaRecomendacoes = await response.Content.ReadAsAsync<List<Recomendacao>>();
            }
            IQueryable<Recomendacao> recomendacoes = (from r in listaRecomendacoes select r).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                recomendacoes = recomendacoes.Where(r => r.Informacao.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nome_desc":
                    recomendacoes = recomendacoes.OrderByDescending(r => r.Informacao);
                    break;
                default:
                    recomendacoes = recomendacoes.OrderBy(r => r.Informacao);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Recomendacao>.CreateAsync(recomendacoes, pageNumber ?? 1, pageSize));
        }
        private void PopulateZonasDropDownList(List<Zona> listaZonas, object selectedZona = null)
        {
            var zonasQuery = from z in listaZonas
                             orderby z.Nome
                             select z;
            ViewBag.Zona = new SelectList(zonasQuery, "Id", "Nome", selectedZona);
        }
    }
}
