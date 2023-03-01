using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Features.Commands;
using SchoolManagement.Application.Features.Commands.LessonCreate;
using SchoolManagement.Application.Features.Commands.LessonDelete;
using SchoolManagement.Application.Features.Commands.LessonUpdate;
using SchoolManagement.Application.Features.Queries.GetAllLessons;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System.Net;

namespace SchoolManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        readonly IMediator mediator;

        public LessonsController(ILessonReadRepository lessonReadRepository, IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Lesson> lessons = await mediator.Send(new GetAllLessonsQueryRequest());
            return Ok(lessons);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]LessonCreateDto lessonCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CommandResponse commandResponse = await mediator.Send(new LessonCreateCommandRequest(lessonCreateDto));
            if(commandResponse.Check ==false)  return BadRequest();
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromForm]LessonUpdateDto lessonUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CommandResponse commandResponse = await mediator.Send(new LessonUpdateCommandRequest(lessonUpdateDto));
            if (commandResponse.Check ==false) return BadRequest();
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);

            return StatusCode((int)HttpStatusCode.Accepted);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CommandResponse commandResponse = await mediator.Send(new LessonDeleteCommandRequest(id));
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);

            return Ok();
        }
    }
}
