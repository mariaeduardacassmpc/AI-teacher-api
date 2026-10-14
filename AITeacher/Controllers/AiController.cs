using Microsoft.AspNetCore.Mvc;

namespace AITeacher.Controllers
{
    public class AiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
