﻿using System;
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

namespace SafetyTourism.Controllers
{
    public class VirusController : Controller
    {
            private readonly IConfiguration _configure;
            private readonly string apiBaseUrl;

            // Construtor do controller
            public VirusController(IConfiguration configuration)
            {
                _configure = configuration;
                apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
            }

            //GET VIRUS NO INDEX A APARECER
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
                var listaVirus = new List<Virus>();
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = apiBaseUrl + "/virus";
                    var response = await client.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();
                    listaVirus = await response.Content.ReadAsAsync<List<Virus>>();
                }
                IQueryable<Virus> virus = (from v in listaVirus select v).AsQueryable();
                if (!String.IsNullOrEmpty(searchString))
                {
                    virus = virus.Where(v => v.Nome.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "nome_desc":
                        virus = virus.OrderByDescending(v => v.Nome);
                        break;
                    default:
                        virus = virus.OrderBy(v => v.Nome);
                        break;
                }
                int pageSize = 10;
                return View(await PaginatedList<Virus>.CreateAsync(virus, pageNumber ?? 1, pageSize));
            }

            //DETAILS
            public async Task<IActionResult> Details(long? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                Virus virus;
                using (HttpClient client = new HttpClient())
                {
                    string endpoint = apiBaseUrl + "/virus/" + id;
                    var response = await client.GetAsync(endpoint);
                    response.EnsureSuccessStatusCode();
                    virus = await response.Content.ReadAsAsync<Virus>();
                }
                if (virus == null)
                {
                    return NotFound();
                }
                return View(virus);
            }

        // CREATE GET

        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> Create()
        {
            var listaVirus = new List<Virus>();
            using (HttpClient client = new HttpClient())
            {
                string endpoint = apiBaseUrl + "/virus";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaVirus = await response.Content.ReadAsAsync<List<Virus>>();
            }
            return View();
        }

        //CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        
        public async Task<IActionResult> Create([Bind("Id,Nome")] Virus virus)
        { 
            if (ModelState.IsValid)
            { 
                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(virus), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/virus";
                    var response = await client.PostAsync(endpoint, content);
                }
            return RedirectToAction(nameof(Index));
            }
            return View(virus);
        }       
    }
}
