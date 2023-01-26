using BelagricolaQ4Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace BelagricolaQ4Client.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;

        string baseUrl = "https://localhost:7125";

        public ClienteController(ILogger<ClienteController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                //Call Api and store datatable
                IList<Cliente> clientes = new List<Cliente>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage getData = await client.GetAsync("Cliente");

                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        clientes = JsonConvert.DeserializeObject<List<Cliente>>(results);

                        ViewData.Model = clientes;
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

        public async Task<IActionResult> Edit(int codigo)
        {
            Cliente cli = new Cliente();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("Cliente/"+ codigo);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    cli = JsonConvert.DeserializeObject<Cliente>(result);
                    ViewData.Model = cli;
                    return View();
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                    return RedirectToAction("Index", "Cliente");
                }
            }
            
        }

        public async Task<ActionResult<String>> CreateClient(Cliente cliente)
        {
            

            if (cliente.Nome != null && cliente.Cpf != null && cliente.Email != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.PostAsJsonAsync<Cliente>("Cliente", cliente);
                    if (getData.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "Cliente");
                    }
                    else
                    {
                        TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API');</script>";
                    }
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('Não há parâmetros suficientes');</script>";
                
            }
            return RedirectToAction("Create", "Cliente");
        }

        public async Task<IActionResult> Delete(int codigo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.DeleteAsync("Cliente/"+codigo);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Cliente");
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                }
            }
            return RedirectToAction("Index", "Cliente");
        }

        public async Task<IActionResult> UpdateClient(Cliente cli)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.PutAsJsonAsync("Cliente/" + cli.Codigo, cli);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Cliente");
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                }
            }
            return RedirectToAction("Index", "Cliente");
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}