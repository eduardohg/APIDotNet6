using BelagricolaQ4Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BelagricolaQ4Client.Controllers
{
    public class ContatoController : Controller
    {
        private readonly ILogger<ContatoController> _logger;

        string baseUrl = "https://localhost:7125";

        public ContatoController(ILogger<ContatoController> logger)
        {
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                //Call Api and store datatable
                IList<Contato> contatos = new List<Contato>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage getData = await client.GetAsync("Contato");

                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        contatos = JsonConvert.DeserializeObject<List<Contato>>(results);

                        ViewData.Model = contatos;
                    }
                    else
                        ViewData.Model = null;
                    

                }
                return View();
            }catch(Exception ex) { ViewData.Model = null; return View(); }
            
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int codigo)
        {
            Contato con = new Contato();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("Contato/" + codigo);
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    con = JsonConvert.DeserializeObject<Contato>(result);
                    ViewData.Model = con;
                    return View();
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                    return RedirectToAction("Index", "Contato");
                }
            }

        }

        public async Task<ActionResult<String>> CreateContato(Contato contato)
        {
            
            if (contato.Nome != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage getData = await client.PostAsJsonAsync<Contato>("Contato", contato);
                    if (getData.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return RedirectToAction("Index", "Contato");
                    }
                    else
                    {
                        TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                    }
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('Não há parâmetros suficientes');</script>";

            }
            return RedirectToAction("Create", "Contato");
        }

        public async Task<IActionResult> Delete(int codigo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.DeleteAsync("Contato/" + codigo);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Contato");
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                }
            }
            return RedirectToAction("Index", "Contato");
        }

        public async Task<IActionResult> UpdateContato(Contato con)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage getData = await client.PutAsJsonAsync("Contato/" + con.Codigo, con);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Contato");
                }
                else
                {
                    TempData["msg"] = $"<script>alert('Ocorreu um erro ao consumir a API: {getData.ReasonPhrase}');</script>";
                }
            }
            return RedirectToAction("Index", "Contato");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
