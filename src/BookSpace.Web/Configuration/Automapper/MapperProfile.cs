using AutoMapper;
using BookSpace.Factories.ResponseModels;
using BookSpace.Models;
using BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels;
using BookSpace.Web.Models.BookViewModels;

namespace BookSpace.Web.Configuration.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
            CreateMap<Book, SimpleBookViewModel>().ReverseMap();
            CreateMap<Book, BookResponseModel>();

        }

    }
}
