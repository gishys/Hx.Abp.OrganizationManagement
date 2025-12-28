using AutoMapper;
using OrganizationManagement.Organization.Dtos;
using Volo.Abp.Identity;

namespace OrganizationManagement;

/// <summary>
/// 组织管理应用层AutoMapper配置
/// </summary>
public class OrganizationManagementApplicationAutoMapperProfile : Profile
{
    public OrganizationManagementApplicationAutoMapperProfile()
    {
        CreateMap<OrganizationUnit, OrganizationUnitDto>(MemberList.Destination);
        CreateMap<OrganizationUnit, OrganizationUnitUsersDto>(MemberList.Destination);
    }
}

