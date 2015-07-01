using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace PlacesIR.Extentions
{
    public static class Enumerables
    {
        public delegate Result Silver<in T, out Result>(T arg);

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> array)
        {
            return array == null || !array.Any();
        }

        public static bool IsNullOrEmpty(this NameValueCollection array)
        {
            return array == null || array.Count == 0;
        }
    }
}