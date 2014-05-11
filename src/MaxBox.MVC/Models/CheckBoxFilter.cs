using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MaxBox.MVC.Models
{
    public class CheckBoxFilter : PropertyFilter
    {
        public CheckBoxFilter(string label, string propertyName, Expression keySelector) : base(label, propertyName, keySelector)
        {

        }
        public bool IsSelected { get; set; }
    }
}
