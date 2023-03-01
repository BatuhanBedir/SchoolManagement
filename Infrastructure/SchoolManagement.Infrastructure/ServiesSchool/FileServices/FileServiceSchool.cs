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
    public class FileServiceSchool : IFileServiceSchool
    {
        readonly IBaseFileServicesSchool baseFileServices;

        public FileServiceSchool(IBaseFileServicesSchool baseFileServices)
        {
            this.baseFileServices = baseFileServices;
        }

        public void DeleteSchoolOldPhoto(School school, SchoolUpdateDto schoolUpdateDto)
        {
            baseFileServices.DeleteSchoolOldPhoto(school, schoolUpdateDto);
        }

        public void DeleteSchoolPhoto(School school)
        {
            baseFileServices.DeleteSchoolPhoto(school);
        }

        public async Task SaveSchoolPhotoToRootAsync(SchoolCreateDto schoolCreateDto, School school, HttpRequest httpRequest)
        {
            await baseFileServices.SaveSchoolPhotoToRootAsync(schoolCreateDto, school, httpRequest);
        }
    }
}
