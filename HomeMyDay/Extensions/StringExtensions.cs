using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMyDay.Extensions
{
    public static class StringExtensions
    {
        public static string TrimControllerName(this String str)
        {
            return str.Replace("Controller", "");
        }
    }
}
