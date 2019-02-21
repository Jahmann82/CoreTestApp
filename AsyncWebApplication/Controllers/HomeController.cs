using System.Web.Mvc;
using AsyncWebApplication.Services;

namespace AsyncWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var contentService = new ContentService();
            var resultContent = contentService.DoCurlAsync().Result;

            ViewBag.Message = resultContent;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}