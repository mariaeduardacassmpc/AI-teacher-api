using Microsoft.AspNetCore.Mvc;

namespace AITeacher.Controllers
{
    public class JournalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
