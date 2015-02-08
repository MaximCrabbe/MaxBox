using System;
using System.Linq.Expressions;

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