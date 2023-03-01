using Microsoft.AspNetCore.Hosting;
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
    public class LocalFileService : ILocalFileService
    {
        readonly IWebHostEnvironment webHostEnvironment;

        public LocalFileService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public void DeleteStudentOldPhoto(Student student, StudentUpdateDto studentUpdateDto)
        {

            if (student.PhotoPath != null && studentUpdateDto.Photo != null)
            {
                //string path = Path.Combine(@"C:\Yeni klasör\SchoolManagement\Presentation\SchoolManagement.WebApi\wwwroot\images", Path.GetFileName(student.PhotoPath));
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", Path.GetFileName(student.PhotoPath));
                    File.Delete(path);
            }
        }
        public void DeleteStudentPhoto(Student student)
        {
            if (student.PhotoPath != null)
            {
                //string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\SchoolManagement.WebApi\wwwroot\images", Path.GetFileName(student.PhotoPath));
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", Path.GetFileName(student.PhotoPath));

                File.Delete(path);
            }
        }

        public async Task SaveStudentPhotoToRootAsync(StudentCreateDTO studentCreateDto, Student student, HttpRequest request)
        {
            if (studentCreateDto.Photo == null)
            {
                return;
            }
            string ticks = DateTime.Now.Ticks.ToString();
            var path = webHostEnvironment.WebRootPath + @"\images\" + ticks + Path.GetExtension(studentCreateDto.Photo.FileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await studentCreateDto.Photo.CopyToAsync(stream);
            }
            student.PhotoPath = "https://" + request.Host + @"/images/" + ticks + Path.GetExtension(studentCreateDto.Photo.FileName);
        }
    }
}
