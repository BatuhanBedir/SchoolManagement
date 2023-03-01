using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Features.Commands.ApplicationUserCreate;
using SchoolManagement.Application.Features.Commands.ApplicationUserLogin;
using SchoolManagement.Application.IToken;
using SchoolManagement.Domain.Identity;
using System.Security.Claims;

namespace SchoolManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase        //out controller
    {
        readonly IMediator mediator;

        public ApplicationUsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm]ApplicationUserCreateDto applicationUserCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ApplicationUserCreateCommandResponse applicationUserCreateCommandResponse = await mediator.Send(new ApplicationUserCreateCommandRequest(applicationUserCreateDto));

            if (applicationUserCreateCommandResponse.Succeeded == false)
            {
                return new BadRequestObjectResult(applicationUserCreateCommandResponse.Message);
            }
            return Ok(applicationUserCreateCommandResponse.Message);
        }
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] ApplicationUserLoginDto applicationUserLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            string? token = await mediator.Send(new ApplicationUserLoginCommandRequest(applicationUserLoginDto));
            if (token == null)
            {
                return NotFound();
            }
            return Ok(token);
        }
        //[Authorize]
        //[HttpPost("Logout")]
        //public async Task<IActionResult> Logout([FromForm] ApplicationUserLoginDto applicationUserLoginDto)
        //{
        //    //await signInManager.SignOutAsync();
        //    //logger.LogInformation("User logged out");

        //    //return Ok();
        //    HttpContext.SignOutAsync();

        //    var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        //    // Token geçersizleştirme hizmetine çağrı yapın
        //    var tokenService = HttpContext.RequestServices.GetService<ITokenHandler>();
        //    tokenService.InvalidateToken(token);

        //    // Başarılı yanıt döndürün
        //    return Ok();
        //}

        /*
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] yetkilendirme şeması olarak JWT Bearer kimlik doğrulamasını kullanır. Bu, yalnızca geçerli bir JWT kimlik doğrulama belirteci (token) olan kullanıcıların API isteklerine erişebileceği anlamına gelir.
         */
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetLoggedInUserRole()
        {
            /*
              JWT token kullanarak kimlik doğrulama yapan bir ASP.NET Core web API'sinde kullanıcının rolünü döndürmek için yazılmış bir controller action'dır. İlgili HTTP GET isteği yapıldığında, User özelliği üzerinden kullanıcının rolü alınır ve 200 OK cevabı ile geri döndürülür.

                Kullanıcı rolü, token oluşturulurken SecurityTokenDescriptor sınıfında ClaimsIdentity nesnesi içinde Role adı altında bir Claim olarak eklenir. Böylece herhangi bir controller action içinde User.FindFirstValue("Role") şeklinde kullanarak kullanıcının rolüne erişilebilir.
             */
            var role = User.FindFirstValue("Role");
            return Ok(role);
        }
    }
}
