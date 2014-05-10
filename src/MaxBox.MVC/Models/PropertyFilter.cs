using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MaxBox.MVC.Models
{
    public class PropertyFilter
    {
        public PropertyFilter(IEnumerable<string> options, string label, string propertyName, Expression keySelector)
            : this(new SelectList(options), label, propertyName, keySelector)
        {
        }
        public PropertyFilter(SelectList list, string label, string propertyName, Expression keySelector)
        {
            Label = label;
            PropertyName = propertyName;
            KeySelector = keySelector;
            SelectList = list;
            CreatedTime = DateTime.Now;
        }
        public DateTime CreatedTime { get; set; }
        public bool HasStringkeys { get; set; }
        public string Label { get; set; }
        public string PropertyName { get; set; }
        public Expression KeySelector { get; set; }
        public SelectList SelectList { get; set; }
    }
}
