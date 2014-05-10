using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Catel.IoC;
using MaxBox.MVC.Models;

namespace MaxBox.MVC.Services
{
    public static class GenericExtensions
    {
        public static PageProcessService PageProcessService
        {
            get
            {
                if (HttpContext.Current.Session["pagingvm"] == null)
                {
                    HttpContext.Current.Session["pagingvm"] = TypeFactory.Default.CreateInstance<PageProcessService>();
                }
                return (PageProcessService)HttpContext.Current.Session["pagingvm"];
            }
            set
            {
                HttpContext.Current.Session["pagingvm"] = value;
            }
        }
        public static IQueryable<T> EnablePaging<T>(this IQueryable<T> queryable, int pagingSize = 10, int maxPagination = 6)
        {
            PageProcessService.SetPaging(pagingSize, maxPagination);
            return PageProcessService.ProcessPaging(queryable);
        }
        //public static IQueryable<T> EnableFilterFor<T, TKey>(this IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector, string label = null) where T : class
        //{
        //    var filter = PageProcessService.MakeFilter(queryable, keySelector, label);
        //    return PageProcessService.ProcessFilter(queryable, filter);
        //}
        public static IQueryable<T> EnableFilterFor<T, TKey, SecondKey>(this IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector, Expression<Func<T, SecondKey>> theenum, string label = null) where T : class
        {
            var filter = PageProcessService.MakeFilter(queryable, keySelector, theenum, label);
            return PageProcessService.ProcessFilter(queryable, filter);
        }
        public static IQueryable<T> EnableFilterFor<T, Y, TKey>(this IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector, IEnumerable<Y> filterlist, string valuefield = "Id", string displayfield = "Name", string label = null)
            where Y : class
            where T : class
        {
            var filter = PageProcessService.MakeFilter(queryable, keySelector, filterlist, valuefield, displayfield, label);
            return PageProcessService.ProcessFilter(queryable, filter);
        }

    }
}