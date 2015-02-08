using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
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

        public void SetPaging(int itemsPerPage, int maxPagination)
        {
            Data.ItemsPerPage = itemsPerPage;
            Data.MaxPagination = maxPagination;
            PagingIsSet = true;
        }

        public DropDownFilter MakeFilter<T, TKey, SecondKey>(IQueryable<T> queryable,
            Expression<Func<T, TKey>> keySelector, Expression<Func<T, SecondKey>> enumSelecteor,
            string label = null) where T : class
        {
            var enummember = enumSelecteor.Body as MemberExpression;
            var list =
                ((IEnumerable<Enum>) Enum.GetValues(enummember.Type)).Select(
                    x =>
                        new
                        {
                            Id = (int) Enum.Parse(enummember.Type, Enum.GetName(enummember.Type, x)),
                            Name = x.ToString()
                        });
            return GenerateDropDownFilter(queryable, keySelector, new SelectList(list, "Id", "Name"), label);
            ;
        }

        public DropDownFilter MakeFilter<T, TKey, Y>(IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector,
            IEnumerable<Y> filterlist, string valuefield = "Id", string displayfield = "Name", string label = null)
            where Y : class
            where T : class
        {
            return GenerateDropDownFilter(queryable, keySelector,
                new SelectList(filterlist.ToList(), valuefield, displayfield), label);
        }

        public DropDownFilter MakeBoolFilter<T>(IQueryable<T> queryable, Expression<Func<T, bool>> keySelector,
            string label = null)
            where T : class
        {
            var trueandfalses =
                new Dictionary<string, string> {{"true", "True"}, {"false", "False"}}.Select(
                    x => new {Id = x.Key, Name = x.Value});
            return GenerateDropDownFilter(queryable, keySelector, new SelectList(trueandfalses, "Id", "Name"), label);
            //return GenerateCheckBoxFilter(queryable, keySelector, label); checkbox only has 2 states :/
        }

        public DropDownFilter MakeEnumFilter<T, TKey>(IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector,
            string label = null)
            where T : class
        {
            var enummember = keySelector.Body as MemberExpression;
            var list = (from Enum d in Enum.GetValues(enummember.Type)
                select
                    new
                    {
                        Id = (int) Enum.Parse(enummember.Type, Enum.GetName(enummember.Type, d)),
                        Name = d.ToString()
                    }).ToList();
            return GenerateDropDownFilter(queryable, keySelector, new SelectList(list, "Id", "Name"), label);
        }

        public CheckBoxFilter GenerateCheckBoxFilter<T, TKey>(IQueryable<T> queryable,
            Expression<Func<T, TKey>> keySelector, string label = null) where T : class
        {
            var member = keySelector.Body as MemberExpression;
            string propertyName = member.Member.Name;
            string keyname = typeof (T) + "," + propertyName;
            PropertyFilter filter;
            if (_cacheService.TryGetValue(keyname, out filter))
            {
                Debug.WriteLine("Got {0} out of cache.", new object[] {keyname});
                Data.Filters.Add(filter);
            }
            else
            {
                Debug.WriteLine("Putting {0} in the cache.", new object[] {keyname});
                label = label ?? propertyName;
                filter = new CheckBoxFilter(label, propertyName, keySelector);
                filter.Label = label;
                Data.Filters.Add(filter);
                _cacheService.Add(keyname, filter);
            }
            return (CheckBoxFilter) filter;
        }

        public DropDownFilter GenerateDropDownFilter<T, TKey>(IQueryable<T> queryable,
            Expression<Func<T, TKey>> keySelector, SelectList selectList, string label = null) where T : class
        {
            var member = keySelector.Body as MemberExpression;
            string propertyName = member.Member.Name;
            string keyname = typeof (T) + "," + propertyName;
            PropertyFilter filter;
            if (_cacheService.TryGetValue(keyname, out filter))
            {
                Debug.WriteLine("Got {0} out of cache.", new object[] {keyname});
                Data.Filters.Add(filter);
            }
            else
            {
                Debug.WriteLine("Putting {0} in the cache.", new object[] {keyname});
                label = label ?? propertyName;
                filter = new DropDownFilter(selectList, label, propertyName, keySelector);
                filter.Label = label;
                Data.Filters.Add(filter);
                _cacheService.Add(keyname, filter);
            }
            return (DropDownFilter) filter;
        }

        public void Save(dynamic viewBag)
        {
            viewBag.PageData = Data;
        }

        public IQueryable<T> ProcessPaging<T>(IQueryable<T> queryable)
        {
            Data.IsPaged = true;
            Data.ItemsCount = queryable.Count() - 1;
            Data.AuditPages = (Data.ItemsCount/Data.ItemsPerPage) + 1;
            if (Data.CurrentPage > 1)
            {
                queryable = queryable.Skip((Data.CurrentPage - 1)*Data.ItemsPerPage);
            }
            return queryable.Take(Data.ItemsPerPage);
        }

        public IQueryable<T> ProcessFilter<T, TKey>(IQueryable<T> queryable, Expression<Func<T, TKey>> keySelector)
        {
            if (Data.IsPaged)
            {
                throw new Exception("Paging must be done on the last step.");
            }
            var member = keySelector.Body as MemberExpression;
            string propertyName = member.Member.Name;
            string thevalue = Data.Query[propertyName];
            if (!String.IsNullOrWhiteSpace(thevalue))
            {
                return queryable.Where(CreateWhere(keySelector, thevalue));
            }
            return queryable;
        }

        public Expression<Func<T, bool>> CreateWhere<T, TKey>(Expression<Func<T, TKey>> keySelector,
            string parameterValue)
        {
            TKey value;
            if ((typeof (TKey)).IsEnum)
            {
                value = (TKey) Enum.Parse(typeof (TKey), parameterValue);
            }
            else
            {
                value = (TKey) Convert.ChangeType(parameterValue, typeof (TKey));
            }
            ParameterExpression param = keySelector.Parameters[0];
            Expression body = Expression.Equal(keySelector.Body, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}