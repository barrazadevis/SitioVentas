using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SitioPersona.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SitioPersona.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IConfiguration _Configure;
        private readonly string apiBaseUrl;

        public ProductoController(IConfiguration configuration)
        {
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        // GET: HomeController
        public IActionResult Index()
        {
            return View("ListaProducto");
        }
        public IActionResult ListaProducto()
        {

            IEnumerable<Producto> productos;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP GET
                    var responseTask = client.GetAsync("Producto");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Producto>>();
                        readTask.Wait();

                        productos = readTask.Result;

                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        productos = Enumerable.Empty<Producto>();

                        ModelState.AddModelError(string.Empty, "Se presentaron errores al cargar la lista de clientes");
                    }
                }
                catch (Exception)
                {

                    productos = Enumerable.Empty<Producto>();

                    ModelState.AddModelError(string.Empty, "No Hubo conexión con el servidor remoto, contacte al administrador");
                }

            }
            return View(productos);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.Nombre))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                        //HTTP POST
                        var postTask = client.PostAsJsonAsync<Producto>("Producto", producto);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ListaProducto");
                        }
                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                        return View("Create", producto);
                    }

                }
            }

            ModelState.AddModelError(string.Empty, "Se presentaron errores al registrar");

            return View("Create", producto);
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
                    var getTask = client.GetAsync($"Producto/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var producto = result.Content.ReadAsAsync<Producto>().Result;
                        return View(producto);
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
        public IActionResult Edit(Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.Nombre))
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<Producto>("Producto", producto);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ListaProducto");
                        }
                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                        return View("Edit", producto);
                    }

                }
            }

            ModelState.AddModelError(string.Empty, "Se presentaron errores al editar");

            return View("Edit", producto);
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
                    var getTask = client.GetAsync($"Producto/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var producto = result.Content.ReadAsAsync<Producto>().Result;
                        return View(producto);
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
                    var getTask = client.DeleteAsync($"Producto/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaProducto");
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
