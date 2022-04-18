using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SitioPersona.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SitioPersona.Controllers
{
    public class VentaController : Controller
    {
        private readonly IConfiguration _Configure;
        private readonly string apiBaseUrl;

        public VentaController(IConfiguration configuration)
        {
            _Configure = configuration;
            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }
        // GET: HomeController
        public IActionResult Index()
        {
            return View("ListaVenta");
        }
        public IActionResult ListaVenta()
        {

            IEnumerable<Venta> ventas;
            IEnumerable<VentaViewModel> ventasListado;
            List<VentaViewModel> ventasd = new List<VentaViewModel>();
            VentaViewModel venta;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP GET
                    var responseTask = client.GetAsync("Venta");
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Venta>>();
                        readTask.Wait();

                        ventas = readTask.Result;

                        foreach (var ve in ventas)
                        {
                            venta = new VentaViewModel();
                            venta.venta = ve;
                            var cl = client.GetAsync($"Cliente/{ve.IdCliente}").Result.Content.ReadAsAsync<Cliente>().Result;
                            venta.Cliente = new List<Cliente>();
                            venta.Cliente.Add(cl);
                            var pr = client.GetAsync($"Producto/{ve.IdProducto}").Result.Content.ReadAsAsync<Producto>().Result;
                            venta.Producto = new List<Producto>();
                            venta.Producto.Add(pr);
                            ventasd.Add(venta);
                        }

                        ventasListado = ventasd;


                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        ventasListado = Enumerable.Empty<VentaViewModel>();

                        ModelState.AddModelError(string.Empty, "Se presentaron errores al cargar la lista de clientes");
                    }
                }
                catch (Exception ex)
                {

                    ventasListado = Enumerable.Empty<VentaViewModel>();

                    ModelState.AddModelError(string.Empty, "No Hubo conexión con el servidor remoto, contacte al administrador");
                }

            }
            return View(ventasListado);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            var ventaViewModel = new VentaViewModel();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP GET
                    var productos = client.GetAsync($"Producto").Result.Content.ReadAsAsync<List<Producto>>().Result;
                    ventaViewModel.Producto = productos;

                    var clientes = client.GetAsync($"Cliente").Result.Content.ReadAsAsync<List<Cliente>>().Result;
                    ventaViewModel.Cliente = clientes;
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "No se pudo cargar la lista");
                }

            }
            return View(ventaViewModel);

        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Venta venta)
        {
            var ventaViewModel = new VentaViewModel();
            ventaViewModel.venta = venta;

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<Venta>("Venta", venta);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaVenta");
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                    return View("Create", ventaViewModel);
                }

            }

            ModelState.AddModelError(string.Empty, "Se presentaron errores al registrar");

            return View("Create", ventaViewModel);
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
                    var ventaViewModel = new VentaViewModel();
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP POST
                    var getTask = client.GetAsync($"Venta/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ventaViewModel.venta = result.Content.ReadAsAsync<Venta>().Result;
                        ventaViewModel.Cliente = client.GetAsync("Cliente").Result.Content.ReadAsAsync<List<Cliente>>().Result;
                        ventaViewModel.Producto = client.GetAsync("Producto").Result.Content.ReadAsAsync<List<Producto>>().Result;

                        return View(ventaViewModel);
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
        public IActionResult Edit(Venta venta)
        {
            var ventaViewModel = new VentaViewModel();
            ventaViewModel.venta = venta;
            if (venta.IdVenta > 0)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<Venta>("Venta", venta);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("ListaVenta");
                        }
                    }
                    catch (Exception)
                    {

                        ModelState.AddModelError(string.Empty, "No hubo comunicación con el servidor remoto, comuniquese con el administrador");

                        return View("Edit", ventaViewModel);
                    }

                }
            }

            ModelState.AddModelError(string.Empty, "Se presentaron errores al editar");

            return View("Edit", venta);
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
                    var ventaViewModel = new VentaViewModel();
                    client.BaseAddress = new Uri(apiBaseUrl);
                    //HTTP POST
                    var getTask = client.GetAsync($"Venta/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ventaViewModel.venta = result.Content.ReadAsAsync<Venta>().Result;
                        ventaViewModel.Cliente = client.GetAsync("Cliente").Result.Content.ReadAsAsync<List<Cliente>>().Result;
                        ventaViewModel.Producto = client.GetAsync("Producto").Result.Content.ReadAsAsync<List<Producto>>().Result;

                        return View(ventaViewModel);
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
                    var getTask = client.DeleteAsync($"Venta/{id}");
                    getTask.Wait();

                    var result = getTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("ListaVenta");
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
