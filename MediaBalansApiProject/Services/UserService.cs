using Azure;
using MediaBalansApiProject.DAL;
using MediaBalansApiProject.Models;
using MediaBalansApiProject.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Services
{
    public class UserService
    {
        private readonly MB_Context _context;

        public UserService (MB_Context context )
        {
            _context = context;

        }

        public void PostUser(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                
                Firstname = addUserRequest.Name,
                Surname = addUserRequest.Surname,
                UserName = addUserRequest.Username,
                Email = addUserRequest.Email,
                PhoneNumber = addUserRequest.Phone,
                Address = addUserRequest.Address,
                Password = addUserRequest.Password
        };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void PostCatagory(CatagoryReguest catagoryReguest )
        {

            var cat = new Category()
            {

               Name = catagoryReguest.Name
            };

            _context.Categories.Add(cat);
            _context.SaveChanges();
        }
      
        public void PostProduct(ProductReguest productReguest  )
        {

            var pro = new Product()
            {

                Name = productReguest.Name,
                Whiting = productReguest.Whiting,
                Color = productReguest.Color,
                Price = productReguest.Price,
                CategoryId = productReguest.CategoryId

            };

            _context.Products.Add(pro);
            _context.SaveChanges();
        }
       
        public IEnumerable<Product> GetProdact()
        {
            return _context.Products.Include(c=>c.Category).ToList();
        }

        public IEnumerable<Category> GetCatagory()
        {
            return _context.Categories.ToList();
        }

        public void ProductDelete(int id)
        {
            var pro = _context.Products.Find(id);

            if (pro.Id == id)
            {
                _context.Products.Remove(pro);
            }
            _context.SaveChanges();
        }

        public IEnumerable<Product> ProductEdit()
        {
            //var pro = _context.Products.Find(id);
              return _context.Products.Include(c => c.Category).ToList();
        }
    }
}
