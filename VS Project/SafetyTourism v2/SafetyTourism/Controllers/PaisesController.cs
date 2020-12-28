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
using System.Net.Http.Headers;

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

        // GET: paises
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
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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

        // GET: paises/create
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create()
        {
            var listaZonas = new List<Zona>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/zonas";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<Zona>>();
            }
            PopulateZonasDropDownList(listaZonas);
            return View();
        }

        // POST: paises/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nome,ZonaId")] Pais pais)
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
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pais), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/paises";
                    var response = await client.PostAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: paises/details/pt
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pais pais;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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

        // GET: paises/edit/pt
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pais pais;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/paises/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                pais = await response.Content.ReadAsAsync<Pais>();
            }
            if (pais == null)
            {
                return NotFound();
            }
            var listaZonas = new List<Zona>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/zonas";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaZonas = await response.Content.ReadAsAsync<List<Zona>>();
            }
            PopulateZonasDropDownList(listaZonas, id);
            return View(pais);
        }

        // POST: paises/edit/pt
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nome,ZonaId")] Pais pais)
        {
            if (id != pais.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    UserInfo user = new UserInfo();
                    StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                    UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    StringContent content = new StringContent(JsonConvert.SerializeObject(pais), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/paises/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pais);
        }

        // GET: paises/delete/pt
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Pais pais;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
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

        // POST: paises/delete/pt
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/paises/" + id;
                var response = await client.DeleteAsync(endpoint);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Obter as recomendações válidas para o país referido
        public async Task<IActionResult> Recomendacoes(string id, string sortOrder, string currentFilter, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["CurrentSort"] = sortOrder;
            ViewData["InformacaoSortParm"] = sortOrder == "inf" ? "inf_desc" : "inf";
            ViewData["DataSortParm"] = String.IsNullOrEmpty(sortOrder) ? "data_desc" : "";
            ViewData["ValidadeSortParm"] = sortOrder == "val" ? "val_desc" : "val";
            ViewData["CurrentFilter"] = currentFilter;
            var listaRecomendacoes = new List<Recomendacao>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/paises/" + id + "/recomendacoes";
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                listaRecomendacoes = await response.Content.ReadAsAsync<List<Recomendacao>>();
            }
            if (listaRecomendacoes == null)
            {
                return NotFound();
            }
            IQueryable<Recomendacao> recomendacoes = (from r in listaRecomendacoes select r).AsQueryable();
            switch (sortOrder)
            {
                case "inf":
                    recomendacoes = recomendacoes.OrderBy(r => r.Informacao);
                    break;
                case "inf_desc":
                    recomendacoes = recomendacoes.OrderByDescending(r => r.Informacao);
                    break;
                case "data_desc":
                    recomendacoes = recomendacoes.OrderByDescending(r => r.Data);
                    break;
                case "val":
                    recomendacoes = recomendacoes.OrderBy(r => r.Validade);
                    break;
                case "val_desc":
                    recomendacoes = recomendacoes.OrderByDescending(r => r.Validade);
                    break;
                default:
                    recomendacoes = recomendacoes.OrderBy(r => r.Data);
                    break;
            }
            int pageSize = 10;
            return View(await PaginatedList<Recomendacao>.CreateAsync(recomendacoes, pageNumber ?? 1, pageSize));
        }

        // GET: Obter os surtos activos para o país referido
        public async Task<IActionResult> Surtos(string id, string sortOrder, string currentFilter, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["CurrentSort"] = sortOrder;
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
                string endpoint = apiBaseUrl + "/paises/" + id + "/surtos";
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

        private void PopulateZonasDropDownList(List<Zona> listaZonas, object selectedZona = null)
        {
            var zonasQuery = from z in listaZonas
                             orderby z.Nome
                             select z;
            ViewBag.Zona = new SelectList(zonasQuery, "Id", "Nome", selectedZona);
        }
    }
}
