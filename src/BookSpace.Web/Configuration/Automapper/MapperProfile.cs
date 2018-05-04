using AutoMapper;
using BookSpace.Factories;
using BookSpace.Factories.ResponseModels;
using BookSpace.Models;
using BookSpace.Web.Areas.Admin.Models.ApplicationUserViewModels;
using BookSpace.Web.Models.BookViewModels;
using BookSpace.Web.Models.CommentsViewModel;

namespace BookSpace.Web.Configuration.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap();
            CreateMap<Book, ListBookViewModel>().ReverseMap();

            CreateMap<Book, BookResponseModel>();
            CreateMap<Comment, CommentViewModel>().ForMember(c => c.Author, a => a.MapFrom(u => u.User.UserName));
            CreateMap<Genre, GenreResponseModel>();
            CreateMap<Tag, TagResponseModel>();


        }

    }
}
