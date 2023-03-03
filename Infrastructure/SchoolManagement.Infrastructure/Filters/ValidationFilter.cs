using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter  //actiona gelen isteklerde devreye giren filter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState.Where(x => x.Value.Errors.Any()).ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage)).ToArray();

                context.Result = new BadRequestObjectResult(error);
                return;
            }
            //next bir sonraki filterı delegete eder. her filter kendi içerisinde yukardakini çalıştıracak. next delegeti bir sonraki filterı temsil ediyor. işlemi bitirdikten sonra tetiklemem gerekiyor.
            await next();
        }
    }
}
