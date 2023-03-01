using Microsoft.AspNetCore.Http;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.IServicesSchool
{
    public interface IBaseFileServicesSchool
    {
        Task SaveSchoolPhotoToRootAsync(SchoolCreateDto schoolCreateDto, School school, HttpRequest httpRequest);
        void DeleteSchoolOldPhoto(School school, SchoolUpdateDto schoolUpdateDto);
        void DeleteSchoolPhoto(School school);
    }
}
