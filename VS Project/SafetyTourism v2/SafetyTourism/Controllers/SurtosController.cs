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
            ViewData["VirusSortParm"] = sortOrder == "vir" ? "vir_desc" : "vir";
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
                surtos = surtos.Where(s => s.Zona.Nome.Contains(searchString) || s.Virus.Nome.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "zona_desc":
                    surtos = surtos.OrderByDescending(s => s.Zona.Nome);
                    break;
                case "vir":
                    surtos = surtos.OrderBy(s => s.Virus.Nome);
                    break;
                case "vir_desc":
                    surtos = surtos.OrderByDescending(s => s.Virus.Nome);
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

        //DETAILS
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Surto surto;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/surtos/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                surto = await response.Content.ReadAsAsync<Surto>();
            }
            if (surto == null)
            {
                return NotFound();
            }
            return View(surto);
        }

        //CREATE GET
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create()
        {
            var listaZonas = new List<Zona>();
            var listaVirus = new List<Virus>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                var responseZonas = await client.GetAsync(apiBaseUrl + "/zonas");
                responseZonas.EnsureSuccessStatusCode();
                listaZonas = await responseZonas.Content.ReadAsAsync<List<Zona>>();
                var responseVirus = await client.GetAsync(apiBaseUrl + "/virus");
                responseZonas.EnsureSuccessStatusCode();
                listaVirus = await responseVirus.Content.ReadAsAsync<List<Virus>>();
            }
            PopulateZonasDropDownList(listaZonas);
            PopulateVirusDropDownList(listaVirus);
            return View();
        }

        //CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Create([Bind("Id,VirusId,DataDetecao,DataFim,ZonaId")] Surto surto)
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
                    StringContent content = new StringContent(JsonConvert.SerializeObject(surto), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/surtos";
                    var response = await client.PostAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(surto);
        }

        //DELETE GET
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Surto surto;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/surtos/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                surto = await response.Content.ReadAsAsync<Surto>();
            }
            if (surto == null)
            {
                return NotFound();
            }
            return View(surto);
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
                string endpoint = apiBaseUrl + "/surtos/" + id;
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
            Surto surto;
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                string endpoint = apiBaseUrl + "/surtos/" + id;
                var response = await client.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                surto = await response.Content.ReadAsAsync<Surto>();
            }
            if (surto == null)
            {
                return NotFound();
            }
            var listaZonas = new List<Zona>();
            var listaVirus = new List<Virus>();
            using (HttpClient client = new HttpClient())
            {
                UserInfo user = new UserInfo();
                StringContent contentUser = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var responseLogin = await client.PostAsync(apiBaseUrl + "/users/login", contentUser);
                UserToken token = await responseLogin.Content.ReadAsAsync<UserToken>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                var responseZonas = await client.GetAsync(apiBaseUrl + "/zonas");
                responseZonas.EnsureSuccessStatusCode();
                listaZonas = await responseZonas.Content.ReadAsAsync<List<Zona>>();
                var responseVirus = await client.GetAsync(apiBaseUrl + "/virus");
                responseZonas.EnsureSuccessStatusCode();
                listaVirus = await responseVirus.Content.ReadAsAsync<List<Virus>>();
            }
            PopulateZonasDropDownList(listaZonas, id);
            PopulateVirusDropDownList(listaVirus, id);
            return View(surto);
        }

        //EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Funcionario,Administrador")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,VirusId,DataDetecao,DataFim,ZonaId")] Surto surto)
        {
            if (id != surto.Id)
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
                    StringContent content = new StringContent(JsonConvert.SerializeObject(surto), Encoding.UTF8, "application/json");
                    string endpoint = apiBaseUrl + "/surtos/" + id;
                    var response = await client.PutAsync(endpoint, content);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(surto);
        }
     
        private void PopulateZonasDropDownList(List<Zona> listaZonas, object selectedZona = null)
        {
            var zonasQuery = from z in listaZonas
                             orderby z.Nome
                             select z;
            ViewBag.Zona = new SelectList(zonasQuery, "Id", "Nome", selectedZona);
        }

        private void PopulateVirusDropDownList(List<Virus> listaVirus, object selectedVirus = null)
        {
            var virusQuery = from v in listaVirus
                             orderby v.Nome
                             select v;
            ViewBag.Virus = new SelectList(virusQuery, "Id", "Nome", selectedVirus);
        }
    }
}
