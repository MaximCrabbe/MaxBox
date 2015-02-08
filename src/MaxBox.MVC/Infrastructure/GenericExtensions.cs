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
                if (HttpContext.Current.Session["PagingData"] == null)
                {
                    var item = TypeFactory.Default.CreateInstance<PageProcessService>();
                    HttpContext.Current.Session["PagingData"] = item;
                }
                return (PageProcessService) HttpContext.Current.Session["PagingData"];
            }
            set { HttpContext.Current.Session["PagingData"] = value; }
        }

        public static IQueryable<T> EnablePaging<T>(this IQueryable<T> queryable, int itemsPerPage = 10,
            int maxPagination = 6)
        {
            PageProcessService.SetPaging(itemsPerPage, maxPagination);
            return PageProcessService.ProcessPaging(queryable);
        }

        public static IQueryable<T> EnableFilterFor<T, Y, TKey>(this IQueryable<T> queryable,
            Expression<Func<T, TKey>> keySelector, IEnumerable<Y> filterlist, string valuefield = "Id",
            string displayfield = "Name", string label = null)
            where Y : class
            where T : class
        {
            PageProcessService.MakeFilter(queryable, keySelector, filterlist, valuefield, displayfield, label);
            return PageProcessService.ProcessFilter(queryable, keySelector);
        }

        public static IQueryable<T> EnableFilterFor<T>(this IQueryable<T> queryable,
            Expression<Func<T, bool>> keySelector, string label = null)
            where T : class
        {
            PageProcessService.MakeBoolFilter(queryable, keySelector, label);
            return PageProcessService.ProcessFilter(queryable, keySelector);
        }

        public static IQueryable<T> EnableFilterFor<T, TKey>(this IQueryable<T> queryable,
            Expression<Func<T, TKey>> keySelector, string label = null)
            where T : class where TKey : struct
        {
            PageProcessService.MakeEnumFilter(queryable, keySelector, label);
            return PageProcessService.ProcessFilter(queryable, keySelector);
        }
    }
}