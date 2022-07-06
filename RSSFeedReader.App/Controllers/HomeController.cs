using Microsoft.AspNetCore.Mvc;

namespace RSSFeedReader.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
