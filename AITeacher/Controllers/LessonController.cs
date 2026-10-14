using Microsoft.AspNetCore.Mvc;

namespace AITeacher.Controllers
{
    public class LessonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
