using AutoMapper;
using BilgeBlog.Application.DTOs.CategoryDtos.Commands;
using BilgeBlog.Application.DTOs.CommentDtos.Commands;
using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.DTOs.RoleDtos.Commands;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.WebApi.Requests.CategoryRequests;
using BilgeBlog.WebApi.Requests.CommentRequests;
using BilgeBlog.WebApi.Requests.PostRequests;
using BilgeBlog.WebApi.Requests.RoleRequests;
using BilgeBlog.WebApi.Requests.UserRequests;

namespace BilgeBlog.WebApi.Mappings
{
    public class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            CreateMap<LoginUserRequest, LoginUserCommand>();

            CreateMap<RegisterUserRequest, RegisterUserCommand>();

            CreateMap<ChangePasswordRequest, ChangePasswordCommand>();

            CreateMap<RefreshTokenRequest, RefreshTokenCommand>();

            CreateMap<CreateCommentRequest, CreateCommentCommand>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCommentId, opt => opt.Ignore());

            CreateMap<UpdateCommentRequest, UpdateCommentCommand>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserId, opt => opt.Ignore());

            CreateMap<CreatePostRequest, CreatePostCommand>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<UpdatePostRequest, UpdatePostCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<CreateCategoryRequest, CreateCategoryCommand>();

            CreateMap<UpdateCategoryRequest, UpdateCategoryCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateRoleRequest, UpdateRoleCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserId, opt => opt.Ignore());

            CreateMap<UpdateUserRequest, UpdateUserCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CurrentUserId, opt => opt.Ignore());
        }
    }
}

