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
using static MediaBalanceCMS.Models.VM.ProductCategoryResponseVM;

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

                if (Res.IsSuccessStatusCode == false)
                {
                    return RedirectToAction("UserRegister", new { id = 1 });
                }

                return RedirectToAction("Index", new { id = 2 });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ProductIndex(int? id)
        {
            if (id == 3)
            {
                ViewBag.Modal = 3;
            }

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
        public async Task<IActionResult> CatagoryAdd(int? id)
        {
            if (id == 4)
            {
                ViewBag.Modal = 4;
            }
            else if (id == 3)
            {
                ViewBag.Modal = 3;
            }
            else if (id == 0)
            {
                ViewBag.Modal = 0;
            }
            //return View();
            List<CategoryVM> resultCategories = null;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync($"home/getcategory");
                var categories = Res.Content.ReadAsStringAsync();
                resultCategories = JsonConvert.DeserializeObject<List<CategoryVM>>(categories.Result);

                return View(resultCategories);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CatagoryAdd(CategoryVM categoryVM)
        {
            if (categoryVM.Name == null)
            {
                return RedirectToAction("CatagoryAdd", new { id = 4 });
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.PostAsJsonAsync("home/postcategory", categoryVM);
                return RedirectToAction("CatagoryAdd", new { id = 3 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                var deleteTask = await client.DeleteAsync($"home/categoryDelete/{id}");

                return RedirectToAction("CatagoryAdd");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductAdd(int? id)
        {
            if (id == 4)
            {
                ViewBag.Modal = 4;
            }
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
            if (productVM.CategoryId == 0 || productVM.Name == null || productVM.Price == null)
            {
                return RedirectToAction("ProductAdd", new { id = 4 });
            }
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.PostAsJsonAsync("home/postproduct", productVM);

                return RedirectToAction("ProductIndex", new { id = 3 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //root action(proDelete/{id}) should be like this
                var deleteTask = await client.DeleteAsync($"home/proDelete/{id}");

                return RedirectToAction("ProductIndex");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdidt(int id)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync($"home/proEdit/{id}");
                var products = Res.Content.ReadAsStringAsync();
                //If there is a list and a single value in the incoming response, first of all, it is necessary to create the appropriate class and meet it.
                var productCatagoryVM = JsonConvert.DeserializeObject<RootProductCategoryResponseVM>(products.Result);

                ResponsVM vm = new ResponsVM()
                {
                    productReguest = productCatagoryVM.productReguest,
                    catagoryReguests = productCatagoryVM.catagoryReguests
                };
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdidt(ResponsVM responsVM)
        {
            ProductVM productVM = new ProductVM()
            {
                CategoryId = responsVM.productReguest.CategoryId,
                Id = responsVM.productReguest.Id,
                Name = responsVM.productReguest.Name,
                Price = responsVM.productReguest.Price,
                Whiting = responsVM.productReguest.Whiting,
                Color = responsVM.productReguest.Color
            };

            if (productVM.CategoryId == 0 || productVM.Name == null || productVM.Price == null)
            {
                return RedirectToAction("ProductAdd", new { id = 4 });
            }
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsJsonAsync("home/proUpdate", productVM);

                return RedirectToAction("ProductIndex", new { id = 3 });
            }
        }

    }
}
