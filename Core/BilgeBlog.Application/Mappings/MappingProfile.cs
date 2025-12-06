using AutoMapper;
using BilgeBlog.Application.DTOs.CategoryDtos.Commands;
using BilgeBlog.Application.DTOs.CategoryDtos.Results;
using BilgeBlog.Application.DTOs.CommentDtos.Commands;
using BilgeBlog.Application.DTOs.CommentDtos.Results;
using BilgeBlog.Application.DTOs.PostCategoryDtos.Results;
using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.DTOs.PostDtos.Results;
using BilgeBlog.Application.DTOs.PostLikeDtos.Results;
using BilgeBlog.Application.DTOs.PostTagDtos.Results;
using BilgeBlog.Application.DTOs.RoleDtos.Commands;
using BilgeBlog.Application.DTOs.RoleDtos.Results;
using BilgeBlog.Application.DTOs.TagDtos.Commands;
using BilgeBlog.Application.DTOs.TagDtos.Results;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
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

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<RegisterUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.RoleId, opt => opt.Ignore());
            CreateMap<Role, RoleResult>();
            CreateMap<CreateRoleCommand, Role>();

            CreateMap<Post, PostResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags != null && src.PostTags.Any() ? src.PostTags.Where(pt => pt.Tag != null).Select(pt => pt.Tag!).ToList() : new List<Tag>()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.PostCategories != null && src.PostCategories.Any() ? src.PostCategories.Where(pc => pc.Category != null).Select(pc => pc.Category!).FirstOrDefault() : null))
                .ForMember(dest => dest.TotalLikeCount, opt => opt.MapFrom(src => src.Likes != null ? src.Likes.Count : 0))
                .ForMember(dest => dest.TotalCommentCount, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.Count : 0));

            CreateMap<Post, PostListItemResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags != null && src.PostTags.Any() ? src.PostTags.Where(pt => pt.Tag != null).Select(pt => pt.Tag!).ToList() : new List<Tag>()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.PostCategories != null ? src.PostCategories.Where(pc => pc.Category != null).Select(pc => pc.Category!).FirstOrDefault(): null))
                .ForMember(dest => dest.TotalLikeCount, opt => opt.MapFrom(src => src.Likes != null ? src.Likes.Count : 0))
                .ForMember(dest => dest.TotalCommentCount, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.Count : 0));

            CreateMap<CreatePostCommand, Post>()
                .ForMember(dest => dest.Slug, opt => opt.Ignore());

            CreateMap<Category, CategoryResult>();
            CreateMap<CreateCategoryCommand, Category>();

            CreateMap<Tag, TagResult>();
            CreateMap<CreateTagCommand, Tag>();

            CreateMap<Comment, CommentResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));

            CreateMap<Comment, CommentListItemResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));

            CreateMap<CreateCommentCommand, Comment>();

            CreateMap<PostCategory, PostCategoryResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));

            CreateMap<PostTag, PostTagResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Tag != null ? src.Tag.Name : string.Empty));

            CreateMap<PostLike, PostLikeResult>()
                .ForMember(dest => dest.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : string.Empty))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));

            CreateMap<PostLike, PostLikeListItemResult>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? $"{src.User.FirstName} {src.User.LastName}" : string.Empty));
        }
    }
}

