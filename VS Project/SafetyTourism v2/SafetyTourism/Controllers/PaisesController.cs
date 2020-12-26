﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyTourism.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SafetyTourism.Controllers
{
    public class PaisesController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        public PaisesController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }
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

        /*public async Task<IActionResult> Index()
        {
            var paises = new List<Pais>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/paises";
                using (var response = await client.GetAsync(endpoint))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    paises = JsonConvert.DeserializeObject<List<Pais>>(apiResponse);
                }
            }
            return View(paises);
        }*/

        // GET: Destinos/Details/5
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
    }
}
