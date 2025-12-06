using AutoMapper;
using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using BilgeBlog.Application.DTOs.PostCategoryDtos.Results;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using BilgeBlog.Application.DTOs.PostTagDtos.Results;
using BilgeBlog.Application.DTOs.RoleDtos.Results;
using BilgeBlog.Application.DTOs.TagDtos.Results;
using BilgeBlog.Application.DTOs.UserDtos.Results;
using BilgeBlog.Domain.Entities;

namespace BilgeBlog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResult>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : string.Empty));

            CreateMap<Role, RoleResult>();

            CreateMap<Post, PostResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));

            CreateMap<Category, CategoryResult>();

            CreateMap<Tag, TagResult>();

            CreateMap<Comment, CommentResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));

            CreateMap<PostCategory, PostCategoryResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

            CreateMap<PostTag, PostTagResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Tag != null ? src.Tag.Name : string.Empty));

            CreateMap<PostLike, PostLikeResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));
        }
    }
}

