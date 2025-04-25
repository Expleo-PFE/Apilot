using Apilot.Application.DTOs.Authentication;
using Apilot.Application.DTOs.Collection;
using Apilot.Application.DTOs.Envirenoment;
using Apilot.Application.DTOs.Folder;
using Apilot.Application.DTOs.History;
using Apilot.Application.DTOs.Request;
using Apilot.Application.DTOs.Response;
using Apilot.Application.DTOs.WorkSpace;
using Apilot.Domain.Entities;
using AutoMapper;

namespace Apilot.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WorkSpaceEntity, WorkSpaceDto>().ReverseMap();
        CreateMap<CollectionEntity, CollectionDto>().ReverseMap();
        CreateMap<FolderEntity, FolderDto>().ReverseMap();
        CreateMap<RequestEntity, RequestDto>().ReverseMap();
        CreateMap<AuthenticationEntity, AuthenticationDto>().ReverseMap();
        CreateMap<ResponseEntity, ResponseDto>().ReverseMap();
        CreateMap<ResponseCookiesEntity, ResponseCookiesDto>().ReverseMap();
        CreateMap<EnvironementEntity, EnvironmentDto>().ReverseMap();
        CreateMap<HistoryEntity, HistoryDto>().ReverseMap();

        
    }
}