using MediaBalansApiProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.DAL
{
    public class MB_Context : IdentityDbContext<User>
    {
        public MB_Context(DbContextOptions<MB_Context> options) : base(options)
        {
        }


        public DbSet<Category> Categories  { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
