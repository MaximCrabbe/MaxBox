using System.Data.Entity;
using MaxBox.MVCExample.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MaxBox.MVCExample.Migrations
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductsCategories { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}