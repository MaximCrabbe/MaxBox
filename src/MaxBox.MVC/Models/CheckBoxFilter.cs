using System.Linq.Expressions;

namespace MaxBox.MVC.Models
{
    public class CheckBoxFilter : PropertyFilter
    {
        public CheckBoxFilter(string label, string propertyName, Expression keySelector)
            : base(label, propertyName, keySelector)
        {
        }

        public bool IsSelected { get; set; }
    }
}