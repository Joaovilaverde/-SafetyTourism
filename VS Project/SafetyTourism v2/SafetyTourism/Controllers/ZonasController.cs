using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyTourism.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace SafetyTourism.Controllers
{
    public class ZonasController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        // Construtor do controller
        public ZonasController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        //GET ZONAS
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NomeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var listaZonas = new List<Zona>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/zonas";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<Zona>>();
            }
            IQueryable<Zona> zonas = (from z in listaZonas select z).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                zonas = zonas.Where(z => z.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nome_desc":
                    zonas = zonas.OrderByDescending(z => z.Nome);
                    break;
                default:
                    zonas = zonas.OrderBy(z => z.Nome);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Zona>.CreateAsync(zonas, pageNumber ?? 1, pageSize));
        }
    }
}
