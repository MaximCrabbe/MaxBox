using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MaxBox.MVC.Services;
using MaxBox.MVCExample.Migrations;
using MaxBox.MVCExample.Models;
using MaxBox.MVC;
using Microsoft.Ajax.Utilities;

namespace MaxBox.MVCExample.Controllers
{
    public class ProductController : MaxBoxController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public ActionResult Index()
        {
            IQueryable<Product> products = db.Products.Include(p => p.Category).OrderBy(x => x.Id);
            products = products
                .EnableFilterFor(x => x.CategorieId, db.ProductsCategories, valuefield: "Id", displayfield: "Naam")
                .EnableFilterFor(x => x.IsBeschikbaar)
                .EnableFilterFor(x => x.Status)
                .EnablePaging(5);
            return View(products);
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
