using Microsoft.AspNetCore.Mvc;
using SchoolManagement.MVCUI.Models;
using System.Diagnostics;

namespace SchoolManagement.MVCUI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}