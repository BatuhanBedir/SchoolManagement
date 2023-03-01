using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SchoolManagement.MVCUI.Controllers
{
    public class ApplicationUsersController : Controller
    {
        //one page application.
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }
    }
}
