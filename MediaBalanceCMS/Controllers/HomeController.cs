using MediaBalanceCMS.Models;
using MediaBalanceCMS.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace MediaBalanceCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? id)
        {
            if (id == 1)
            {
                ViewBag.Modal = 1;
            }
            else if (id == 2)
            {
                ViewBag.Modal = 2;
            }
            else if (id == 0)
            {
                ViewBag.Modal = 0;
            }
            return View();
        }
      
        //Hosted web API REST Service base url
        string Baseurl = "https://localhost:44350/";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserLoginVM userLoginVM)
        {
            
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.PostAsJsonAsync("user", userLoginVM);
             
                if (Res.IsSuccessStatusCode == false)
                {
                    return RedirectToAction("Index", new { id = 1 });
                }
                return RedirectToAction("ProductIndex", "Home");
            }
        }

        [HttpGet]
        public IActionResult UserRegister(int? id)
        {

            if (id == 1)
            {
                ViewBag.Modal = 1;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister(UserRegisterVM userRegisterVM)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.PostAsJsonAsync("user/userregister", userRegisterVM);

                if(Res.IsSuccessStatusCode == false)
                {
                    return RedirectToAction("UserRegister", new { id = 1 });
                }

                return RedirectToAction("Index", new { id = 2 });
            }
        }

       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductVM> resultCategories = null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("home/productIndex");
                var products = Res.Content.ReadAsStringAsync();
                resultCategories = JsonConvert.DeserializeObject<List<ProductVM>>(products.Result);
            }

            ProductCatagoryVM vm = new ProductCatagoryVM()
            {
                ProductVMs = resultCategories,
                CategoryVMs = null
            };
       
            return View(vm);
        }

        [HttpGet]
        public IActionResult CatagoryAdd()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CatagoryAdd(CategoryVM categoryVM )
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.PostAsJsonAsync("home/postcategory", categoryVM);

                return RedirectToAction("ProductIndex", "Home"); ;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductAdd()
        {
            List<CategoryVM> resultCategories = null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("home/getcategory");
                var categories = Res.Content.ReadAsStringAsync();
                resultCategories = JsonConvert.DeserializeObject<List<CategoryVM>>(categories.Result);
                //Checking the response is successful or not which is sent using HttpClient
               
                //returning the employee list to view
            }

            ProductCatagoryVM vm = new ProductCatagoryVM()
            {
                ProductVMs = null,
                CategoryVMs = resultCategories
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductAdd(ProductVM productVM)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.PostAsJsonAsync("home/postproduct", productVM);
               
                return RedirectToAction("ProductIndex", "Home"); ;
            }
        }


    }
}
