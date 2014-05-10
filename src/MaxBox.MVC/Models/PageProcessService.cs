using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using MaxBox.Core.Extensions;
using MaxBox.Core.Services;

namespace MaxBox.MVC.Models
{
    public class PageProcessService
    {
        private readonly ICacheService<string, PropertyFilter> _cacheService;
        public TimeSpan CacheTime = new TimeSpan(0, 24, 0, 0);

        public PageProcessService(ICacheService<string, PropertyFilter> cacheService)
        {
            _cacheService = cacheService;
            Data = new PageProcessData();
        }

        public bool PagingIsSet { get; set; }

        public PageProcessData Data { get; set; }

        public void SetPaging(int pagingSize, int maxPagination)
        {
            Data.PagingSize = pagingSize;
            Data.MaxPagination = maxPagination;
            PagingIsSet = true;
        }


        //public PropertyFilter MakeFilter<T, TKey>(IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector, string label = null) where T : class
        //{
        //    var member = keySelector.Body as MemberExpression;
        //    string propertyName = member.Member.Name;
        //    string keyname = typeof(T) + "," + propertyName;
        //    label = label ?? propertyName;
        //    PropertyFilter PropertyFilter;
        //    if (_cacheService.TryGetValue(keyname, out PropertyFilter))
        //    {
        //        if (PropertyFilter.CreatedTime.Add(CacheTime) < DateTime.Now)
        //        {
        //            Debug.WriteLine("Got {0} out of cache.", new object[] { keyname });
        //            Data.Filters.Add(PropertyFilter);
        //        }
        //        else
        //        {
        //            _cacheService.Remove(keyname);
        //            Debug.WriteLine("Putting {0} in the cache.", new object[] { keyname });
        //            Func<T, TKey> function = keySelector.Compile();
        //            List<string> filterData = queryable.DistinctBy(function).Select(function).Cast<string>().ToList();
        //            PropertyFilter = new PropertyFilter(filterData, label, propertyName, keySelector);
        //            Data.Filters.Add(PropertyFilter);
        //            _cacheService.Add(keyname, PropertyFilter);
        //        }
        //    }
        //    else
        //    {
        //        Debug.WriteLine("Putting {0} in the cache.", new object[] { keyname });
        //        Func<T, TKey> function = keySelector.Compile();
        //        List<string> filterData = queryable.DistinctBy(function).Select(function).Cast<string>().ToList();
        //        PropertyFilter = new PropertyFilter(filterData, label, propertyName, keySelector);
        //        Data.Filters.Add(PropertyFilter);
        //        _cacheService.Add(keyname, PropertyFilter);
        //    }
        //    return PropertyFilter;
        //}
        public PropertyFilter MakeFilter<T, TKey, SecondKey>(IQueryable<T> queryable,
            Expression<Func<T, TKey>> keySelector, Expression<Func<T, SecondKey>> enumSelecteor,
            string label = null) where T : class
        {
            var member = keySelector.Body as MemberExpression;
            string propertyName = member.Member.Name;
            string keyname = typeof (T) + "," + propertyName;
            label = label ?? propertyName;
            PropertyFilter propertyFilter;
            if (_cacheService.TryGetValue(keyname, out propertyFilter))
            {
                Debug.WriteLine("Got {0} out of cache.", new object[] {keyname});
                Data.Filters.Add(propertyFilter);
            }
            else
            {
                Debug.WriteLine("Putting {0} in the cache.", new object[] {keyname});
                var enummember = enumSelecteor.Body as MemberExpression;
                var list = (from Enum d in Enum.GetValues(enummember.Type)
                    select
                        new
                        {
                            Id = (int) Enum.Parse(enummember.Type, Enum.GetName(enummember.Type, d)),
                            Name = d.ToString()
                        }).ToList();
                propertyFilter = new PropertyFilter(new SelectList(list, "Id", "Name"), label, propertyName, keySelector);
                Data.Filters.Add(propertyFilter);
                _cacheService.Add(keyname, propertyFilter);
            }
            return propertyFilter;
        }

        public PropertyFilter MakeFilter<T, TKey, Y>(IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector,
            IEnumerable<Y> filterlist, string valuefield = "Id", string displayfield = "Name", string label = null)
            where Y : class
            where T : class
        {
            var member = keySelector.Body as MemberExpression;
            string propertyName = member.Member.Name;
            string keyname = typeof (T) + "," + propertyName;
            label = label ?? propertyName;
            PropertyFilter propertyFilter;
            if (_cacheService.TryGetValue(keyname, out propertyFilter))
            {
                Debug.WriteLine("Got {0} out of cache.", new object[] {keyname});
                Data.Filters.Add(propertyFilter);
            }
            else
            {
                Debug.WriteLine("Putting {0} in the cache.", new object[] {keyname});
                propertyFilter = new PropertyFilter(new SelectList(filterlist.ToList(), valuefield, displayfield), label,
                    propertyName, keySelector);
                Data.Filters.Add(propertyFilter);
                _cacheService.Add(keyname, propertyFilter);
            }
            return propertyFilter;
        }


        public void Save(dynamic viewBag)
        {
            viewBag.PagingVM = Data;
        }

        public IQueryable<T> ProcessPaging<T>(IQueryable<T> queryable)
        {
            Data.IsPaged = true;
            Data.ItemsCount = queryable.Count();
            Data.AuditPages = (Data.ItemsCount/Data.PagingSize) + 1;
            if (Data.CurrentPage > 1)
            {
                queryable = queryable.Skip((Data.CurrentPage - 1)*Data.PagingSize);
            }
            return queryable.Take(Data.PagingSize);
        }

        public IQueryable<T> ProcessFilters<T>(IQueryable<T> queryable)
        {
            Data.Filters.ForEach(filter => { queryable = ProcessFilter(queryable, filter); });
            return queryable;
        }

        public IQueryable<T> ProcessFilter<T>(IQueryable<T> queryable, PropertyFilter propertyFilter)
        {
            if (Data.IsPaged)
            {
                throw new Exception("Paging must be done on the last step.");
                    // how else can you know the paging if you later are going to add the filtering ....
            }
            string thevalue = Data.Query[propertyFilter.PropertyName];
            if (!String.IsNullOrWhiteSpace(thevalue))
            {
                var stringfilter = propertyFilter.KeySelector as Expression<Func<T, string>>;
                if (stringfilter != null)
                {
                    return queryable.Where(stringfilter.Compose(x => x == thevalue));
                }
                var intfilter = propertyFilter.KeySelector as Expression<Func<T, int>>;
                if (intfilter != null)
                {
                    int intvalue = Convert.ToInt32(thevalue);
                    return queryable.Where(intfilter.Compose(x => x == intvalue));
                }
                var intnullfilter = propertyFilter.KeySelector as Expression<Func<T, int?>>;
                if (intnullfilter != null)
                {
                    int intvalue = Convert.ToInt32(thevalue);
                    return queryable.Where(intnullfilter.Compose(x => x == intvalue));
                }
            }
            return queryable;
        }
    }
}