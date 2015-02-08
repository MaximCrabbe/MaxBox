using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MaxBox.MVC.Models
{
    public class DropDownFilter : PropertyFilter
    {
        public DropDownFilter(IEnumerable<string> options, string label, string propertyName, Expression keySelector)
            : this(new SelectList(options), label, propertyName, keySelector)
        {
        }

        public DropDownFilter(SelectList list, string label, string propertyName, Expression keySelector)
            : base(label, propertyName, keySelector)
        {
            Label = label;
            PropertyName = propertyName;
            KeySelector = keySelector;
            SelectList = list;
            CreatedTime = DateTime.Now;
        }

        public SelectList SelectList { get; set; }
    }
}