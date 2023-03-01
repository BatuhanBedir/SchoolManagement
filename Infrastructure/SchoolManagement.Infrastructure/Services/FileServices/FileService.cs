using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.IServices;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.Services.FileServices
{
    public class FileService : IFileService
    {
        readonly IBaseFileService baseFileServices;

        public FileService(IBaseFileService baseFileServices)
        {
            this.baseFileServices = baseFileServices;
        }

        public void DeleteStudentOldPhoto(Student student, StudentUpdateDto studentUpdateDto)
        {
            baseFileServices.DeleteStudentOldPhoto(student, studentUpdateDto);
        }

        public void DeleteStudentPhoto(Student student)
        {
            baseFileServices.DeleteStudentPhoto(student);
        }

        public async Task SaveStudentPhotoToRootAsync(StudentCreateDTO studentCreateDto, Student student, HttpRequest request)
        {
            await baseFileServices.SaveStudentPhotoToRootAsync(studentCreateDto, student, request);
        }
    }
}
