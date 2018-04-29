using AutoMapper;
using BookSpace.Models;
using BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSpace.Web.Configuration.ObjectMapping
{
    public class ObjectMappingProfile : Profile
    {
        public ObjectMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>();
        }
    }
}
