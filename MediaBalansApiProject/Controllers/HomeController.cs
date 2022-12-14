using MediaBalansApiProject.DAL;
using MediaBalansApiProject.Models;
using MediaBalansApiProject.Models.Requests;
using MediaBalansApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly MB_Context _context;

        public HomeController(UserService userService, MB_Context context)
        {
            _userService = userService;
            _context = context;
        }     

        [HttpPost]
        [Route("postcategory")]

        public IActionResult AddCatagory(CatagoryReguest catagoryReguest)
        {
            _userService.PostCatagory(catagoryReguest);

            return Ok("success");
        }

        [HttpGet]
        [Route("productIndex")]
        public IActionResult GetProduct()
        {
            var products = _userService.GetProdact();

            List<ProductReguest> productReguests = new List<ProductReguest>();

            foreach (var item in products)
            {
                ProductReguest productReguests1 = new ProductReguest();
                productReguests1.Id = item.Id;
                productReguests1.Name = item.Name;
                productReguests1.Price = item.Price;
                productReguests1.Color = item.Color;
                productReguests1.Whiting = item.Whiting;
                productReguests1.CategoryId = item.CategoryId;
                productReguests1.CategoryName = item.Category.Name;

                productReguests.Add(productReguests1);
            }

            return Ok(productReguests);
        }


        [HttpGet]
        [Route("getcategory")]
        public IActionResult GetCatagory()
        {
            var categories = _userService.GetCatagory();

            List<CatagoryReguest> catagoryReguest = new List<CatagoryReguest>();

            foreach (var item in categories)
            {
                CatagoryReguest catagoryReguest1 = new CatagoryReguest();

                catagoryReguest1.Name = item.Name;
                catagoryReguest1.Id = item.Id;

                catagoryReguest.Add(catagoryReguest1);
            }


            return Ok(catagoryReguest);
        }

        [HttpDelete]
        [Route("categoryDelete/{id}")]
        public IActionResult CategoryDelete(int id)
        {
            _userService.CategoryDelete(id);
            return Ok();
        }

        [HttpPost]
        [Route("postproduct")]
        public IActionResult AddProduct(ProductReguest productReguest)
        {
            _userService.PostProduct(productReguest);

            return Ok("success");
        }

        [HttpDelete]
        [Route("proDelete/{id}")]
        public IActionResult ProductDelete(int id)
        {
            _userService.ProductDelete(id);
            return Ok();
        }

        [HttpGet]
        [Route("proEdit/{id}")]
        public IActionResult ProductEdit(int id)
        {
            var products = _userService.ProductEdit();

            var category = _context.Categories.ToList();

            CatagoryProductReguest catagoryProductReguest = new CatagoryProductReguest();

            List<CatagoryReguest> catagoryReguest = new List<CatagoryReguest>();

            foreach (var item in category)
            {
                CatagoryReguest catagoryReguest1 = new CatagoryReguest();

                catagoryReguest1.Name = item.Name;
                catagoryReguest1.Id = item.Id;

                catagoryReguest.Add(catagoryReguest1);

            }
            catagoryProductReguest.CatagoryReguests = catagoryReguest;

            foreach (var item in products)
            {
                ProductReguest productReguests1 = new ProductReguest();

                if (item.Id == id)
                {
                    productReguests1.Id = item.Id;
                    productReguests1.Name = item.Name;
                    productReguests1.Price = item.Price;
                    productReguests1.Color = item.Color;
                    productReguests1.Whiting = item.Whiting;
                    productReguests1.CategoryId = item.CategoryId;
                    productReguests1.CategoryName = item.Category.Name;

                    catagoryProductReguest.ProductReguest = productReguests1;
                }
            }

            return Ok(catagoryProductReguest);

        }

       [HttpPost]
       [Route("proUpdate")]
       public IActionResult ProductEdit(ProductReguest productReguest)
        {

            _userService.ProductEdit(productReguest);

            return Ok();
        }
    }
}
