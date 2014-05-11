using System.Web.Mvc;
using MaxBox.MVC.Models;
using MaxBox.MVC.Views.Shared;

namespace MaxBox.MVC
{
    public abstract class MaxBoxController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Session["PagingData"] != null)
            {
                var pageProcessService = (PageProcessService)Session["PagingData"];
                pageProcessService.Save(ViewBag);
                Session["PagingData"] = null;
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
