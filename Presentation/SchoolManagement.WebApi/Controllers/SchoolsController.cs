using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Features.Commands;
using SchoolManagement.Application.Features.Commands.SchoolCreate;
using SchoolManagement.Application.Features.Commands.SchoolDelete;
using SchoolManagement.Application.Features.Commands.SchoolUpdate;
using SchoolManagement.Application.Features.Commands.StudentUpdate;
using SchoolManagement.Application.Features.Queries.GetAllSchools;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System.Net;

namespace SchoolManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        readonly IMediator mediator;

        public SchoolsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            List<School> schools = await mediator.Send(new GetAllSchoolsQueryRequest());

            SchoolIndexVM schoolIndexVM = new();
            schoolIndexVM.Schools = schools;

            return Ok(schoolIndexVM);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] SchoolCreateDto schoolCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CommandResponse commandResponse = await mediator.Send(new SchoolCreateCommandRequest(schoolCreateDto,Request));
            if (commandResponse.Check == false) return BadRequest();
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);
            return StatusCode((int)HttpStatusCode.Created);
        }
        #region MediatRden önce
        //public async Task<IActionResult> Post([FromForm] SchoolCreateDto schoolCreateDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    School school = new();
        //    school.Name = schoolCreateDto.Name;

        //    bool check = await schoolWriteRepository.AddAsync(school);
        //    if (!check) return StatusCode((int)HttpStatusCode.InternalServerError);
        //    int dbCheck = await schoolWriteRepository.SaveAsync();

        //    return dbCheck > 0 ? Ok(school) : BadRequest();
        //} 
        #endregion

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] SchoolUpdateDto schoolUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CommandResponse commandResponse = await mediator.Send(new SchoolUpdateCommandRequest(schoolUpdateDto, Request));
            if (commandResponse.Found == false) return NotFound();
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);
            return Ok();
        }
        #region MediatR'den önce
        //public async Task<IActionResult> Update([FromBody] SchoolUpdateDto schoolUpdateDto)
        //{
        //    //update ve delete işlerinde data döndürülmemesi tercih edilir..

        //    //backendde elde ettiğim verileri servis ediyorum.api services(datayı servis etmek)

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    School school = await schoolReadRepository.GetById(schoolUpdateDto.SchoolId);
        //    if (school == null)
        //    {
        //        return NotFound();
        //    }
        //    school.Name = schoolUpdateDto.Name;

        //    int dbCheck = await schoolWriteRepository.SaveAsync();

        //    return dbCheck >0 ? Ok(school) : StatusCode((int)HttpStatusCode.InternalServerError);
        //} 
        #endregion
        [HttpDelete]
        public async Task<IActionResult> Delete (Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            CommandResponse commandResponse = await mediator.Send(new SchoolDeleteCommandRequest(id));
            if (commandResponse.Check == false) return BadRequest();
            if (commandResponse.DbCheck < 1) return StatusCode((int)HttpStatusCode.InternalServerError);
            return StatusCode((int)HttpStatusCode.Accepted);
        }
        #region MediatR'den önce
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    School school = await schoolReadRepository.GetById(id);
        //    if (school == null)
        //    {
        //        return NotFound();
        //    }
        //    schoolWriteRepository.Remove(school);

        //    //bool check = await schoolWriteRepository.RemoveAsync(id);
        //    //if (!check) return StatusCode((int)HttpStatusCode.InternalServerError);
        //    int dbCheck = await schoolWriteRepository.SaveAsync();

        //    return dbCheck > 0 ? Ok() : StatusCode((int)HttpStatusCode.InternalServerError);
        //}
        #endregion
    }
}
