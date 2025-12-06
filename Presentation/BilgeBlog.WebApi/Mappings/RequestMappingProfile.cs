using AutoMapper;
using BilgeBlog.Application.DTOs.CommentDtos.Commands;
using BilgeBlog.Application.DTOs.PostDtos.Commands;
using BilgeBlog.Application.DTOs.UserDtos.Commands;
using BilgeBlog.WebApi.Requests.CommentRequests;
using BilgeBlog.WebApi.Requests.PostRequests;
using BilgeBlog.WebApi.Requests.UserRequests;

namespace BilgeBlog.WebApi.Mappings
{
    public class RequestMappingProfile : Profile
    {
        public RequestMappingProfile()
        {
            CreateMap<ChangePasswordRequest, ChangePasswordCommand>();

            CreateMap<CreateCommentRequest, CreateCommentCommand>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentCommentId, opt => opt.Ignore());

            CreateMap<CreatePostRequest, CreatePostCommand>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<UpdatePostRequest, UpdatePostCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());
        }
    }
}

