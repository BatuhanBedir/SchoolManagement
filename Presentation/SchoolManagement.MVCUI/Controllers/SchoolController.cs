using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.MVCUI.Controllers
{
    public class SchoolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
