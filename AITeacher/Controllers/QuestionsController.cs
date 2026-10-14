using Microsoft.AspNetCore.Mvc;

namespace AITeacher.Controllers
{
    public class QuestionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
