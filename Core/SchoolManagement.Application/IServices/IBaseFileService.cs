using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.IServices
{
    public interface IBaseFileService
    {
        Task SaveStudentPhotoToRootAsync(StudentCreateDTO studentCreateDto,Student student, HttpRequest request);
        void DeleteStudentOldPhoto(Student student, StudentUpdateDto studentUpdateDto);
        void DeleteStudentPhoto(Student student);
    }
}
