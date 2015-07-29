using NLog;
using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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
    public static class NlogInit
    {
        public static void RegisterLayoutRenderer(string name, Type type)
        {
            Type testType;
            if (!NLog.Config.ConfigurationItemFactory.Default.LayoutRenderers.TryGetDefinition(name, out testType))
            {
                NLog.Config.ConfigurationItemFactory.Default.LayoutRenderers.RegisterDefinition(name, type);
            }
        }
    }
    [LayoutRenderer("fullurl")]
    public class FullUrlLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url != null && builder != null)
            {
                builder.Append(HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.RawUrl);
            }
        }
    }
}