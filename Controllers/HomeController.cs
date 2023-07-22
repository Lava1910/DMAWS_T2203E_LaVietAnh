using Microsoft.AspNetCore.Mvc;

namespace Exam_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
