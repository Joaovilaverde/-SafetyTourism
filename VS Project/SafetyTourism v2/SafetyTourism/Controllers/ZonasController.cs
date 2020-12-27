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

        //GET ZONAS NO INDEX A APARECER
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

        //DETAILS
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Zona zona;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/zonas/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                zona = await response.Content.ReadAsAsync<Zona>();
            }
            if (zona == null)
            {
                return NotFound();
            }
            return View(zona);
        }

        // CREATE GET
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
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Zona zona)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(zona), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/zonas";
                    var response = await client.PostAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(zona);
        }

        //DELETE GET
        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Zona zona;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/zonas/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                zona = await response.Content.ReadAsAsync<Zona>();
            }
            if (zona == null)
            {
                return NotFound();
            }
            return View(zona);
        }

        //DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = apiBaseUrl + "/zonas/" + id;
                    var response = await client.DeleteAsync(endpoint);
                }
                return RedirectToAction(nameof(Index));
        }

        //EDIT GET
        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Zona zona;
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/zonas/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                zona = await response.Content.ReadAsAsync<Zona>();
            }
            if (zona == null)
            {
                return NotFound();
            }
            return View(zona);
        }

        //EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome")] Zona zona)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(zona), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/zonas/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
