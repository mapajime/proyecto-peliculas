using Microsoft.AspNetCore.Mvc;

namespace Movies.Web.Controllers
{
    public class MovieGenderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
