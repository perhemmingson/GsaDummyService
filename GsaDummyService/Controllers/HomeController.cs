using System.Web.Mvc;

namespace GsaDummyService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Electrolux GSA Fake";

            return View();
        }
    }
}
