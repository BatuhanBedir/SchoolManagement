using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs
{
    public class StudentUpdateDto : StudentCreateDTO
    {
        //readonly IWebHostEnvironment webHostEnvironment;
        
        public Guid Id { get; set; }
        public string? PhotoPath { get; set; }

        //private string photoPath;

        //public string? PhotoPath
        //{
        //    get { return photoPath; }
        //    set
        //    {
        //        if (value == null) return;
        //        string[] arr = value.Split("image/");
        //        photoPath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Presentation/SchoolManagement.WebApi/images/", arr[1]);
        //    }
        //}

    }
}
