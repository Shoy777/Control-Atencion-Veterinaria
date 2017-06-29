using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CustomDateRangeAttribute : RangeAttribute
    {
        public CustomDateRangeAttribute()
            : base(typeof(DateTime), DateTime.Now.ToString(), DateTime.Now.AddYears(1).ToString())
        { }
    }
}
