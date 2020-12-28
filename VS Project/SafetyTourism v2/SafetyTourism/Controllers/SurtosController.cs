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
using System.Net.Http.Headers;

namespace SafetyTourism.Controllers
{
    public class SurtosController : Controller
    {
        private readonly IConfiguration _configure;
        private readonly string apiBaseUrl;

        // Construtor do controller
        public SurtosController(IConfiguration configuration)
        {
            _configure = configuration;
            apiBaseUrl = _configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET SURTOS NO INDEX A APARECER
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ZonaSortParm"] = String.IsNullOrEmpty(sortOrder) ? "zona_desc" : "";
            ViewData["VirusIdSortParm"] = sortOrder == "vir" ? "vir_desc" : "vir";
            ViewData["DataDetecaoSortParm"] = sortOrder == "data" ? "data_desc" : "data";
            ViewData["DataFimSortParm"] = sortOrder == "datafim" ? "datafim_desc" : "datafim";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var listaSurtos = new List<Surto>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/surtos";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaSurtos = await response.Content.ReadAsAsync<List<Surto>>();
            }
            IQueryable<Surto> surtos = (from s in listaSurtos select s).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                surtos = surtos.Where(s => s.Zona.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "zona_desc":
                    surtos = surtos.OrderByDescending(s => s.Zona.Nome);
                    break;
                case "vir":
                    surtos = surtos.OrderBy(s => s.VirusId);
                    break;
                case "vir_desc":
                    surtos = surtos.OrderByDescending(s => s.VirusId);
                    break;
                case "data":
                    surtos = surtos.OrderBy(s => s.DataDetecao);
                    break;
                case "data_desc":
                    surtos = surtos.OrderByDescending(s => s.DataDetecao);
                    break;
                case "datafim":
                    surtos = surtos.OrderBy(s => s.DataFim);
                    break;
                case "datafim_desc":
                    surtos = surtos.OrderByDescending(s => s.DataFim);
                    break;
                default:
                    surtos = surtos.OrderBy(s => s.Zona.Nome);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Surto>.CreateAsync(surtos, pageNumber ?? 1, pageSize));
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
