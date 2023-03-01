using AutoMapper;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.MappingProfiles
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserCreateDto>().ReverseMap().ForAllMembers(x=>x.UseDestinationValue());
            /*
             **!CreateMap<ApplicationUser, ApplicationUserCreateDto>() -->  ApplicationUser ve ApplicationUserCreateDto nesneleri arasındaki eşleştirmeyi yapılandırır. Yani, ApplicationUser nesnesindeki özellikler, ApplicationUserCreateDto nesnesindeki özelliklere eşleştirilecektir
             
            **!.ReverseMap() -->bu eşleştirme işleminin çift yönlü olacağını belirtir. Yani, ApplicationUserCreateDto nesnesindeki özellikler de ApplicationUser nesnesindeki özelliklere eşleştirilecektir.
            
            **!.ForAllMembers(x=>x.UseDestinationValue) --> nesne özelliklerinin eşleştirilmesi sırasında hedef nesnede bulunan özelliklerin değerlerinin kullanılacağını belirtir. Yani, kaynak nesnedeki özelliklerin değerleri, hedef nesnede zaten mevcut olan değerleri değiştirmeyecek ve sadece null olmayan değerler eşleştirilecektir. Bu, özellikle güncelleme işlemlerinde kullanışlıdır, çünkü mevcut değerlerin korunmasına yardımcı olur.
           

             */
        }
    }
}
