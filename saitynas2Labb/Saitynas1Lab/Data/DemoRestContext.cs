using Saitynas1Lab.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Saitynas1Lab.Data.Dtos.Auth;

namespace Saitynas1Lab.Data
{
    public class DemoRestContext :  IdentityDbContext<DemoRestUser>//DbContext
    {
        
        public DbSet<Post> Posts { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // !!! DON'T STORE THE REAL CONNECTION STRING THE IN PUBLIC REPO !!!
            // Use secret managers provided by your chosen cloud provider
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=RestDemo2");
        }
    }
}
