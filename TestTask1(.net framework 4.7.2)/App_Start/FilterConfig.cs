using System.Web;
using System.Web.Mvc;

namespace TestTask1_.net_framework_4._7._2_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
