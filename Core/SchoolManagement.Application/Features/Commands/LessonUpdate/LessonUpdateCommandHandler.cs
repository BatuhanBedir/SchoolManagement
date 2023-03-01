using MediatR;
using SchoolManagement.Application.Features.Commands.LessonCreate;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Application.IRepositories.Generic;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.LessonUpdate
{
    public class LessonUpdateCommandHandler : IRequestHandler<LessonUpdateCommandRequest, CommandResponse>
    {
        ILessonReadRepository lessonReadRepository;
        ILessonWriteRepository lessonWriteRepository;

        public LessonUpdateCommandHandler(ILessonReadRepository lessonReadRepository, ILessonWriteRepository lessonWriteRepository)
        {
            this.lessonReadRepository = lessonReadRepository;
            this.lessonWriteRepository = lessonWriteRepository;
        }

        public async Task<CommandResponse> Handle(LessonUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            Lesson lesson = await lessonReadRepository.GetByIdAsync(request.LessonUpdateDto.Id);
            lesson.Name = request.LessonUpdateDto.Name;
            int dbCheck = await lessonWriteRepository.SaveAsync();
            if (dbCheck < 0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
