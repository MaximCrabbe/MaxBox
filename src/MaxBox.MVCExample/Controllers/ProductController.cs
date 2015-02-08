using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MaxBox.MVC;
using MaxBox.MVC.Infrastructure;
using MaxBox.MVC.Services;
using MaxBox.MVCExample.Migrations;
using MaxBox.MVCExample.Models;

namespace MaxBox.MVCExample.Controllers
{
    public class ProductController : MaxBoxController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            IQueryable<Product> products = db.Products.Include(p => p.Category).OrderBy(x => x.Id);
            products = products
                .EnableFilterFor(x => x.CategorieId, db.ProductsCategories, "Id", "Naam")
                .EnableFilterFor(x => x.IsBeschikbaar)
                .EnableFilterFor(x => x.Status)
                .EnablePaging(5);
            return View(products).WithSuccess("All loaded well");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}