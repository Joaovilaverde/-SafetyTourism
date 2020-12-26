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

namespace SafetyTourism.Controllers
{
    public class PaisesController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        // Construtor do controller
        public PaisesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: Paises
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
            var listaPaises = new List<Pais>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/paises";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaPaises = await response.Content.ReadAsAsync<List<Pais>>();
            }
            IQueryable<Pais> paises = (from p in listaPaises select p).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                paises = paises.Where(p => p.Nome.Contains(searchString) || p.Zona.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nome_desc":
                    paises = paises.OrderByDescending(p => p.Nome);
                    break;
                case "zona":
                    paises = paises.OrderBy(p => p.Zona.Nome);
                    break;
                case "zona_desc":
                    paises = paises.OrderByDescending(p => p.Zona.Nome);
                    break;
                default:
                    paises = paises.OrderBy(p => p.Nome);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Pais>.CreateAsync(paises, pageNumber ?? 1, pageSize));
        }

        // GET: Paises/Create
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create()
        {
            var listaZonas = new List<Zona>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/zonas";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<Zona>>();
            }
            PopulateZonasDropDownList(listaZonas);
            return View();
        }

        // GET: Paises/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pais pais;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/paises/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                pais = await response.Content.ReadAsAsync<Pais>();
            }
            if (pais == null)
            {
                return NotFound();
            }
            return View(pais);
        }

        // POST: Destinos/Create
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create([Bind("Nome,ZonaId")] Pais pais)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = apiBaseUrl + "/paises";
                    var response = await client.PostAsync(endpoint);
                    response.EnsureSuccessStatusCode();
                    pais = await response.Content.ReadAsAsync<Pais>();
                }



                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }*/

        private void PopulateZonasDropDownList(List<Zona> listaZonas, object selectedZona = null)
        {
            var zonasQuery = from z in listaZonas
                               orderby z.Nome
                               select z;
            ViewBag.Zona = new SelectList(zonasQuery, "Id", "Nome", selectedZona);
        }
    }
}
