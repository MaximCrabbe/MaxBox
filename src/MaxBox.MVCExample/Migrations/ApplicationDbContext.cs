using System.Data.Entity;
using System.Diagnostics;
using MaxBox.MVCExample.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MaxBox.MVCExample.Migrations
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        {
#if DEBUG
            Database.Log = LogAndStepThrough;
#endif
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductsCategories { get; set; }

        [DebuggerStepThrough]
        private void LogAndStepThrough(string log)
        {
            Debug.WriteLine(log);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}