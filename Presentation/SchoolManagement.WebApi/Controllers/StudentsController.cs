using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Features.Commands;
using SchoolManagement.Application.Features.Commands.StudentAddLesson;
using SchoolManagement.Application.Features.Commands.StudentCreate;
using SchoolManagement.Application.Features.Commands.StudentDelete;
using SchoolManagement.Application.Features.Commands.StudentUpdate;
using SchoolManagement.Application.Features.Queries.GetAllAndIncludeStudentLesson;
using SchoolManagement.Application.Features.Queries.GetAllLessons;
using SchoolManagement.Application.Features.Queries.GetAllStudentsWithSchools;
using SchoolManagement.Application.Features.Queries.GetByStudentId;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace SchoolManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            List<Student> students = await mediator.Send(new GetAllStudentsQueryRequest());
            var role = User.FindFirstValue("Role");
            var email = User.FindFirstValue("Email");
            StudentsIndexVM studentsIndexVM = new();
            studentsIndexVM.Students = students;
            studentsIndexVM.LoggedInUserRole = role;
            studentsIndexVM.LoggedInUserEmail = email;

            return Ok(studentsIndexVM);
        }


        #region mediatR'den önce
        //public async Task<IActionResult> Get()
        //{
        //    List<Student> students = await studentReadRepository.GetAllAsync(false);
        //    return Ok(students);
        //} 
        #endregion


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            Student student = await mediator.Send(new GetByStudentIdQueryRequest(id));
            StudentUpdateDto studentUpdateDto = new StudentUpdateDto();
            studentUpdateDto.Id = student.Id;
            studentUpdateDto.FirstName = student.FirstName;
            studentUpdateDto.LastName = student.LastName;
            studentUpdateDto.PhotoPath = student.PhotoPath;
            studentUpdateDto.SchoolId = student.SchoolId.ToString();
            return Ok(studentUpdateDto);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromForm] StudentCreateDTO studentCreateDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    //direk request yazılabiliyor.
        //    CommandResponse commandResponse = await mediator.Send(new StudentCreateCommandsRequest(studentCreateDTO,Request));
        //    if (commandResponse.Check == false) return BadRequest();
        //    if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);

        //    return StatusCode((int)HttpStatusCode.Created);
        //}

        #region mediatR'dan önce
        //public async Task<IActionResult> Post([FromForm]StudentCreateDTO studentCreateDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    Student student = new();
        //    student.FirstName = studentCreateDTO.FirstName;
        //    student.LastName = studentCreateDTO.LastName;
        //    if (studentCreateDTO.SchoolId != null)
        //    {
        //        student.SchoolId = Guid.Parse(studentCreateDTO.SchoolId);
        //    }
        //    bool check = await studentWriteRepository.AddAsync(student);//takibe koyduk

        //    if (!check) return StatusCode((int)HttpStatusCode.InternalServerError);

        //    int dbCheck = await studentWriteRepository.SaveAsync();
        //    return dbCheck>0 ? await Task.FromResult(RedirectToAction("Get","Students")) : BadRequest();//tamamlanmış task olarak dönüştürülmesini sağlar. 

        //    //Ok(student) student gönderdiğimi< için response body de görünüyor
        //} 
        #endregion
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] StudentUpdateDto studentUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CommandResponse commandResponse = await mediator.Send(new StudentUpdateCommandsRequest(studentUpdateDto, Request));
            if (commandResponse.Found == false) return NotFound();
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok();
        }
        #region mediatR'dan önce
        //public async Task<IActionResult> Update([FromForm] StudentCreateDTO studentCreateDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    Student student = await studentReadRepository.GetById((Guid)studentCreateDTO.StudentId);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    student.FirstName = studentCreateDTO.FirstName;
        //    student.LastName = studentCreateDTO.LastName;
        //    if (studentCreateDTO.SchoolId != null)
        //    {
        //        student.SchoolId = Guid.Parse(studentCreateDTO.SchoolId);
        //    }

        //    int dbCheck = await studentWriteRepository.SaveAsync();
        //    return dbCheck > 0 ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        //}
        #endregion

        [HttpDelete("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            CommandResponse commandResponse = await mediator.Send(new StudentDeleteCommandsRequest(id));
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);

            return Ok();
        }
        #region mediatR'dan önce
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    Student student = await studentReadRepository.GetById(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    studentWriteRepository.Remove(student);
        //    int dbCheck = await studentWriteRepository.SaveAsync();
        //    return dbCheck > 0 ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        //} 
        #endregion

        [HttpGet]
        [Route("Lesson/{id:guid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Lesson(Guid id)
        {
            GetAllIncludeStudentLessonQueryResponse getAllIncludeStudentLessonQueryResponse = await mediator.Send(new GetAllIncludeStudentLessonQueryRequest(id));
            StudentLessonVM studentLessonVM = new StudentLessonVM();
            studentLessonVM.Id = id;
            studentLessonVM.Lessons = getAllIncludeStudentLessonQueryResponse.Lessons;
            studentLessonVM.Student = getAllIncludeStudentLessonQueryResponse.Student;
            return Ok(studentLessonVM);
        }
        //[HttpPost("[controller]")]
        //[HttpPost("[action]")]
        //[Route("Lesson/{id}")]
        //[HttpPost]
        [HttpPost("{id}")]
        public async Task<IActionResult> Lesson(Guid id,Guid[] ids)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CommandResponse commandResponse = await mediator.Send(new StudentAddLessonCommandRequest(id, ids));
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok();
        }

    }
}
