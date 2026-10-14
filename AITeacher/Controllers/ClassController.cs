using Microsoft.AspNetCore.Mvc;

namespace AITeacher.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
