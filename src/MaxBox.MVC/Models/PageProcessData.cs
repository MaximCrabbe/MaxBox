using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace MaxBox.MVC.Models
{
    public class PageProcessData
    {
        public PageProcessData()
        {
            IsPaged = false;
            Filters = new List<PropertyFilter>();
            CurrentPage = 1;
            MaxPagination = 4;
            ItemsPerPage = 20;
            Controller = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");
            Action = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("action");
            Query = HttpContext.Current.Request.QueryString;
            int page;
            if (Int32.TryParse(Query["page"], out page))
            {
                CurrentPage = page;
            }
        }

        public List<PropertyFilter> Filters { get; set; }
        private string Controller { get; set; }
        private string Action { get; set; }
        public bool IsPaged { get; set; }
        public NameValueCollection Query { get; set; }
        public int AuditPages { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int MaxPagination { get; set; }
        public int ItemsCount { get; set; }

        public string GenerateUrlWithPage(int page)
        {
            var outputstring = new StringBuilder();
            outputstring.AppendFormat("/{0}/{1}/?page={2}", Controller, Action, page);
            foreach (string parameter in Query.AllKeys.Where(x => x != "page"))
            {
                outputstring.Append("&" + parameter + "=" + Query[parameter]);
            }
            return outputstring.ToString();
        }
    }
}