using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SitioPersona.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SitioPersona.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IConfiguration _Configure;
        private readonly string apiBaseUrl;

        public ClienteController(ILogger<ClienteController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _Configure = configuration;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        // GET: HomeController
        public IActionResult Index()
        {
            return View("ListaCliente");
        }
        public IActionResult ListaCliente()
        {

            IEnumerable<Cliente> clientes;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP GET
                    var responseTask = client.GetAsync("Cliente");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Cliente>>();
                        readTask.Wait();

                        clientes = readTask.Result;

                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        clientes = Enumerable.Empty<Cliente>();

                        ModelState.AddModelError(string.Empty, "Se presentaron errores al cargar la lista de Personas");
                    }
                }
                catch (Exception)
                {

                    clientes = Enumerable.Empty<Cliente>();

                    ModelState.AddModelError(string.Empty, "No Hubo conexión con el servidor remoto, contacte al administrador");
                }

            }
            return View(clientes);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            if (!string.IsNullOrEmpty(cliente.Nombre))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<Cliente>("Cliente", cliente);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                                return RedirectToAction("ListaCliente");
                        }
                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                        return View("Create", cliente);
                    }

                }
            }

            ModelState.AddModelError(string.Empty, "Se presentaron errores al registrar");

            return View("Create", cliente);
        }

        // GET: HomeController/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP POST
                    var getTask = client.GetAsync($"Cliente/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var cliente = result.Content.ReadAsAsync<Cliente>().Result;
                        return View(cliente);
                    }
                    return NotFound();
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                    return View("Edit");
                }

            }

        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente cliente)
        {
            if (!string.IsNullOrEmpty(cliente.Nombre))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<Cliente>("Cliente", cliente);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ListaCliente");
                        }
                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                        return View("Edit", cliente);
                    }

                }
            }

            ModelState.AddModelError(string.Empty, "Se presentaron errores al editar");

            return View("Edit", cliente);
        }

        // GET: HomeController/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP POST
                    var getTask = client.GetAsync($"Cliente/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var cliente = result.Content.ReadAsAsync<Cliente>().Result;
                        return View(cliente);
                    }
                    return NotFound();
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                    return View("Delete");
                }

            }
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP POST
                    var getTask = client.DeleteAsync($"Cliente/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaCliente");
                    }
                    return NotFound();
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                    return View();
                }

            }
        }
    }
}
