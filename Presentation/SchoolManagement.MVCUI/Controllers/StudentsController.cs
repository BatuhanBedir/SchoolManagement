using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain;

namespace SchoolManagement.MVCUI.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetStudentsListPartial(StudentsIndexVM studentsIndexVM)
        {
            HttpContext.Session.SetString("email",studentsIndexVM.LoggedInUserEmail);
            return PartialView("_StudentListPartial", studentsIndexVM);
        }
        [HttpPost]
        public IActionResult GetStudentCreatePartial(SchoolIndexVM schoolIndexVM)
        {
            ViewBag.SchoolId = new SelectList(schoolIndexVM.Schools, "Id", "Name");
            return PartialView("_CreateStudentPartial");
        }
        public IActionResult GetUpdateStudentPartial(StudentUpdateDto studentUpdateDto,SchoolIndexVM schoolIndexVM)
        {
            ViewBag.SchoolId = new SelectList(schoolIndexVM.Schools, "Id", "Name");
            return PartialView("_UpdateStudentPartial", studentUpdateDto);
        }
       
    }
}
