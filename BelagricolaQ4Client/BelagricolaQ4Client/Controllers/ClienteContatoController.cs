using BelagricolaQ4Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BelagricolaQ4Client.Controllers
{
    public class ClienteContatoController : Controller
    {
        private readonly ILogger<ClienteContatoController> _logger;

        string baseUrl = "https://localhost:7125";

        public ClienteContatoController(ILogger<ClienteContatoController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                //Call Api and store datatable
                IList<ClienteContato> contatos = new List<ClienteContato>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage getData = await client.GetAsync("ClienteContato");

                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        contatos = JsonConvert.DeserializeObject<List<ClienteContato>>(results);

                        ViewData.Model = contatos;
                    }
                    else
                        ViewData.Model = null;

                }
                return View();
            }
            catch(Exception ex) { ViewData.Model = null; return View(); }
            
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int cliente, int contato)
        {
            ClienteContato cliCon = new ClienteContato();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("ClienteContato/" + cliente + "/" + contato);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    cliCon = JsonConvert.DeserializeObject<ClienteContato>(result);
                    ViewData.Model = cliCon;
                    return View();
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                    return RedirectToAction("Index", "ClienteContato");
                }
            }

        }

        public async Task<ActionResult<String>> CreateClienteContato(ClienteContato clienteContato)
        {
           
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.PostAsJsonAsync<ClienteContato>("ClienteContato", clienteContato);
                    if (getData.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "ClienteContato");
                    
                    }
                    else
                    {
                        string responseBodyAsText = await getData.Content.ReadAsStringAsync();
                        TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {responseBodyAsText}');</script>";
                    }
                }
         
            return RedirectToAction("Create", "ClienteContato");
        }

        public async Task<IActionResult> Delete(int cliente, int contato)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.DeleteAsync("ClienteContato/" + cliente+"/"+contato);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "ClienteContato");
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                }
            }
            return RedirectToAction("Index", "ClienteContato");
        }

        public async Task<IActionResult> UpdateClienteContato(ClienteContato cliCon)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.PutAsJsonAsync("ClienteContato/" + cliCon.ClienteCodigo + "/" + cliCon.ContatoCodigo, cliCon);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "ClienteContato");
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                }
            }
            return RedirectToAction("Index", "ClienteContato");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
