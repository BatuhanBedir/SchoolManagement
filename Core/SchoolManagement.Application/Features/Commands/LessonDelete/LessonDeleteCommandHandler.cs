using MediatR;
using SchoolManagement.Application.IRepositories;
using SchoolManagement.Application.IRepositories.Generic;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Commands.LessonDelete
{
    public class LessonDeleteCommandHandler : IRequestHandler<LessonDeleteCommandRequest, CommandResponse>
    {
        ILessonReadRepository lessonReadRepository;
        ILessonWriteRepository lessonWriteRepository;

        public LessonDeleteCommandHandler(ILessonReadRepository lessonReadRepository, ILessonWriteRepository lessonWriteRepository)
        {
            this.lessonReadRepository = lessonReadRepository;
            this.lessonWriteRepository = lessonWriteRepository;
        }

        public async Task<CommandResponse> Handle(LessonDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            CommandResponse commandResponse = new();
            Lesson lesson =await lessonReadRepository.GetByIdAsync(request.Id);
            if (lesson==null)
            {
                commandResponse.DbCheck = 0;
                return commandResponse;
            }
            bool check = await lessonWriteRepository.RemoveAsync(request.Id);
            if (!check)
            {
                commandResponse.Check = check;
                return commandResponse;
            }
            int dbCheck = await lessonWriteRepository.SaveAsync();
            if (dbCheck < 0)
            {
                commandResponse.DbCheck = dbCheck;
            }
            return commandResponse;
        }
    }
}
