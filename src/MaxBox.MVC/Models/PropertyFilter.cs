using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MaxBox.MVC.Models
{
    public abstract class PropertyFilter
    {
        public PropertyFilter(string label, string propertyName, Expression keySelector)
        {
            Label = label;
            PropertyName = propertyName;
            KeySelector = keySelector;
        }
        public DateTime CreatedTime { get; set; }
        public bool HasStringkeys { get; set; }
        public string Label { get; set; }
        public string PropertyName { get; set; }
        public Expression KeySelector { get; set; }
        public bool IsChecked { get; set; }
    }
}
