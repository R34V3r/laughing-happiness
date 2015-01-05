using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.webapp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
