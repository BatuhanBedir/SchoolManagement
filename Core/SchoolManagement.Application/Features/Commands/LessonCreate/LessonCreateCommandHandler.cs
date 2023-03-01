using MediatR;
using Microsoft.EntityFrameworkCore.Update.Internal;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.LessonCreate
{
    public class LessonCreateCommandHandler : IRequestHandler<LessonCreateCommandRequest, CommandResponse>
    {
        ILessonWriteRepository lessonWriteRepository;

        public LessonCreateCommandHandler(ILessonWriteRepository lessonWriteRepository)
        {
            this.lessonWriteRepository = lessonWriteRepository;
        }

        public async Task<CommandResponse> Handle(LessonCreateCommandRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            Lesson lesson = new();
            lesson.Name = request.LessonCreateDto.Name;

            bool check =await lessonWriteRepository.AddAsync(lesson);
            if (!check)
            {
                commandResponse.Check = check;
                return commandResponse;
            }
            int dbCheck = await lessonWriteRepository.SaveAsync();
            if (dbCheck<0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
