using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
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
            var error = filterContext.Exception;

            Logging(baseDirectory + @"\Content\logging.txt", error);

            if (error.GetType().Name == "RecordException")
            {
                filterContext.Result =
                    new ViewResult
                    {
                        ViewName = "Error",
                        ViewData = new ViewDataDictionary() { 
                            {"message", error.Message},
                            {"status", 404}
                        }
                    };
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

            }
            else
            {
                filterContext.Result =
                    new ViewResult
                    {
                        ViewName = "Error",
                        ViewData = new ViewDataDictionary() {
                            {"message", error.Message},
                            {"status", 500}
                        }
                    };
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            filterContext.ExceptionHandled = true;
        }

        protected async Task Logging(string path, Exception error)
        {
            using (var writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(DateTime.Now.ToString() + " : " + error.Message + error.StackTrace);
            }
        }
    }
}