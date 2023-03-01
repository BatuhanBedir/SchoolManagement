using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.IServicesSchool;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.ServiesSchool.FileServices
{
    public class LocalFileServiceSchool : ILocalFileServiceSchool
    {
        readonly IWebHostEnvironment webHostEnvironment;

        public LocalFileServiceSchool(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public void DeleteSchoolOldPhoto(School school, SchoolUpdateDto schoolUpdateDto)
        {
            if (school.PhotoPath != null && schoolUpdateDto.PhotoPath!=null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"..\SchoolManagement.WebApi\wwwroot\images", Path.GetFileName(school.PhotoPath));
                File.Delete(path);
            }
        }

        public void DeleteSchoolPhoto(School school)
        {
            if (school.PhotoPath != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", Path.GetFileName(school.PhotoPath));
                File.Delete(path);
            }
        }

        public async Task SaveSchoolPhotoToRootAsync(SchoolCreateDto schoolCreateDto, School school, HttpRequest httpRequest)
        {
            if (schoolCreateDto.Photo == null)
            {
                return;
            }
            string ticks = DateTime.Now.Ticks.ToString();
            var path = webHostEnvironment.WebRootPath + @"\images\" + ticks + Path.GetExtension(schoolCreateDto.Photo.FileName);
            await using(var stream = new FileStream(path, FileMode.Create))
            {
                await schoolCreateDto.Photo.CopyToAsync(stream);
            }
            school.PhotoPath = "https://" + httpRequest.Host + @"/images/" + ticks + Path.GetExtension(schoolCreateDto.Photo.FileName);
        }
    }
}
