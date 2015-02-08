using System.Web.Mvc;
using MaxBox.Db.Helpers;
using MaxBox.MVC.Infrastructure;
using MaxBox.MVCExample.Migrations;

namespace MaxBox.MVCExample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult ReCreate()
        {
            var maintanceHelper = new MaintanceHelper();
            maintanceHelper.ReCreateTables<ApplicationDbContext>(true);
            PastaSeeder.Seed(new ApplicationDbContext());
            return RedirectToAction("Index").WithSuccess("Successfully recreated database");
        }
    }
}