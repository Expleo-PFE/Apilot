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
        CreateMap<WorkSpaceEntity , WorkSpaceDto>();
        CreateMap<CollectionEntity , CollectionDto>();
        CreateMap<FolderEntity , FolderDto>();
        CreateMap<RequestEntity , RequestDto>();
        CreateMap<AuthenticationEntity , AuthenticationDto>();
        CreateMap<ResponseEntity , ResponseDto>();
        CreateMap<ResponseCookiesEntity , ResponseCookiesDto>();
        CreateMap<EnvironementEntity , EnvironmentDto>();
        CreateMap<HistoryEntity , HistoryDto>();
        
    }
}