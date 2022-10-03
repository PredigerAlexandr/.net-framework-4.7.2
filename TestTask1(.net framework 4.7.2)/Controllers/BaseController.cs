using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace TestTask1_.net_framework_4._7._2_.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            using (StreamWriter reader = new StreamWriter(baseDirectory + @"\Content\logging.txt", true))
            {
                reader.WriteLine(DateTime.Now.ToString() + " : " + filterContext.Exception.Message);
            }

            filterContext.Result =
                new RedirectResult("/TransportCompany/Error");
            
            filterContext.ExceptionHandled = true;
        }
    }
}