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
using System.Net.Http.Headers;

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
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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
                    UserInfo user = new UserInfo();
                    StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                    UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(virus), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/virus";
                    var response = await client.PostAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(virus);
        }

        //DELETE GET
        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Virus virus;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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

        //DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/virus/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }

        //EDIT GET
        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Virus virus;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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

        //EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]

        public async Task<IActionResult> Edit(long id, [Bind("Id, Nome")] Virus virus)
        {
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(virus), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/virus/" + id;
                var response = await client.PutAsync(endpoint, content);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Obter os surtos activos para o virus referido
        public async Task<IActionResult> SurtosActivos(string id, string sortOrder, string currentFilter, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ZonaSortParm"] = sortOrder == "zona" ? "zona_desc" : "zona";
            ViewData["VirusSortParm"] = sortOrder == "vir" ? "vir_desc" : "vir";
            ViewData["DataDetecaoSortParm"] = String.IsNullOrEmpty(sortOrder) ? "data_desc" : "";
            ViewData["CurrentFilter"] = currentFilter;
            var listaSurtos = new List<Surto>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/surtos/virus/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaSurtos = await response.Content.ReadAsAsync<List<Surto>>();
            }

            if (listaSurtos == null)
            {
                return NotFound();
            }
            IQueryable<Surto> surtos = (from s in listaSurtos select s).AsQueryable();
            switch (sortOrder)
            {
                case "zona":
                    surtos = surtos.OrderBy(s => s.Zona.Nome);
                    break;
                case "zona_desc":
                    surtos = surtos.OrderByDescending(s => s.Zona.Nome);
                    break;
                case "vir":
                    surtos = surtos.OrderBy(s => s.Virus.Nome);
                    break;
                case "vir_desc":
                    surtos = surtos.OrderByDescending(s => s.Virus.Nome);
                    break;
                case "data_desc":
                    surtos = surtos.OrderByDescending(s => s.DataDetecao);
                    break;
                default:
                    surtos = surtos.OrderBy(s => s.DataDetecao);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Surto>.CreateAsync(surtos, pageNumber ?? 1, pageSize));
        }

        // GET: Obter os surtos ocorridos para o virus referido
        public async Task<IActionResult> Surtos(string id, string sortOrder, string currentFilter, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["CurrentSort"] = sortOrder;
            ViewData["ZonaSortParm"] = sortOrder == "zona" ? "zona_desc" : "zona";
            ViewData["VirusSortParm"] = sortOrder == "vir" ? "vir_desc" : "vir";
            ViewData["DataDetecaoSortParm"] = String.IsNullOrEmpty(sortOrder) ? "data_desc" : "";
            ViewData["DataFimSortParm"] = sortOrder == "datafim" ? "datafim_desc" : "datafim";
            ViewData["CurrentFilter"] = currentFilter;
            var listaSurtos = new List<Surto>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/virus/" + id + "/surtos";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaSurtos = await response.Content.ReadAsAsync<List<Surto>>();
            }

            if (listaSurtos == null)
            {
                return NotFound();
            }
            IQueryable<Surto> surtos = (from s in listaSurtos select s).AsQueryable();
            switch (sortOrder)
            {
                case "zona":
                    surtos = surtos.OrderBy(s => s.Zona.Nome);
                    break;
                case "zona_desc":
                    surtos = surtos.OrderByDescending(s => s.Zona.Nome);
                    break;
                case "vir":
                    surtos = surtos.OrderBy(s => s.Virus.Nome);
                    break;
                case "vir_desc":
                    surtos = surtos.OrderByDescending(s => s.Virus.Nome);
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
                    surtos = surtos.OrderBy(s => s.DataDetecao);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Surto>.CreateAsync(surtos, pageNumber ?? 1, pageSize));
        }
    }
}
