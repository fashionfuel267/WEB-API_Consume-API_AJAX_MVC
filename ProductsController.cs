using M05C16_MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace M05C16_MVCClient.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();
            using(var client=new  HttpClient())
            {
                client.BaseAddress =new Uri( "https://localhost:44324/api/Products");
                var response = client.GetAsync(client.BaseAddress);
                response.Wait();
                var output = response.Result;
                if (output.IsSuccessStatusCode)
                {
                    var result = output.Content.ReadAsAsync<List<Product>>().Result;
                    products = result;
                    //return View(products);
                }
                else
                {
                    string msg = output.ReasonPhrase;
                }

            }
            return View(products);
        }
        public ActionResult Create( )
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44324/api/Products");
                var response = client.PostAsJsonAsync<Product>(client.BaseAddress,product);
                response.Wait();
                var output = response.Result;
                if (output.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                   
                }
                else
                {
                    string msg = output.ReasonPhrase;
                }

            }
            return View();
        }
        public ActionResult Details(int id)
        {

            Product product = new Product();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44324/api/Products/" + id);
                var response = client.GetAsync(client.BaseAddress);
                response.Wait();
                var output = response.Result;
                if (output.IsSuccessStatusCode)
                {
                    var result = output.Content.ReadAsAsync<Product>().Result;
                    product = result;
                    //return View(products);
                }
                else
                {
                    string msg = output.ReasonPhrase;
                }

            }
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44324/api/Products/"+id);
                var response = client.DeleteAsync(client.BaseAddress);
                response.Wait();
                var output = response.Result;
                if (output.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    string msg = output.ReasonPhrase;
                }

            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            Product product= new Product();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44324/api/Products/"+id);
                var response = client.GetAsync(client.BaseAddress);
                response.Wait();
                var output = response.Result;
                if (output.IsSuccessStatusCode)
                {
                    var result = output.Content.ReadAsAsync <Product>().Result;
                    product = result;
                    //return View(products);
                }
                else
                {
                    string msg = output.ReasonPhrase;
                }

            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44324/api/Products/"+product.Id);
                var response = client.PutAsJsonAsync<Product>(client.BaseAddress, product);
                response.Wait();
                var output = response.Result;
                if (output.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
                else
                {
                    string msg = output.ReasonPhrase;
                }

            }

            return View();
        }

    }
}