using System.Web.Mvc;
using MaxBox.MVC.Models;
using MaxBox.MVC.Views.Shared;

namespace MaxBox.MVC
{
    public abstract class MaxBoxController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["pagingvm"] != null)
            {
                var pagingVm = (PageProcessService)Session["pagingvm"];
                pagingVm.Save(ViewBag);
                Session["pagingvm"] = null;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
