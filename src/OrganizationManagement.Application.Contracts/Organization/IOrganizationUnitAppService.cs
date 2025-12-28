using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrganizationManagement.Organization.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace OrganizationManagement.Organization;

/// <summary>
/// 组织单元应用服务接口
/// </summary>
public interface IOrganizationUnitAppService :
    ICrudAppService<
        OrganizationUnitDto,
        Guid,
        OrganizationUnitGetListInput,
        OrganizationUnitCreateDto,
        OrganizationUnitUpdateDto>
{
    /// <summary>
    /// 获取所有组织单元列表
    /// </summary>
    Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync();

    /// <summary>
    /// 获取最后一个子组织单元或null
    /// </summary>
    Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId);

    /// <summary>
    /// 移动组织单元
    /// </summary>
    Task MoveAsync(Guid id, OrganizationUnitMoveDto input);

    /// <summary>
    /// 获取根组织单元
    /// </summary>
    Task<ListResultDto<OrganizationUnitDto>> GetRootAsync(bool recursive = false);

    /// <summary>
    /// 查找子组织单元
    /// </summary>
    Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(OrganizationUnitGetChildrenDto input);

    /// <summary>
    /// 获取组织单元的角色名称列表
    /// </summary>
    Task<ListResultDto<string>> GetRoleNamesAsync(Guid id);

    /// <summary>
    /// 获取未添加到组织单元的角色列表
    /// </summary>
    Task<PagedResultDto<IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, OrganizationUnitGetUnaddedRoleListInput input);

    /// <summary>
    /// 获取组织单元的角色列表
    /// </summary>
    Task<PagedResultDto<IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input);

    /// <summary>
    /// 添加角色到组织单元
    /// </summary>
    Task AddRolesAsync(Guid id, OrganizationUnitAddRoleDto input);

    /// <summary>
    /// 获取未添加到组织单元的用户列表
    /// </summary>
    Task<PagedResultDto<IdentityUserDto>> GetUnaddedUsersAsync(Guid id, OrganizationUnitGetUnaddedUserListInput input);

    /// <summary>
    /// 获取组织单元的用户列表
    /// </summary>
    Task<PagedResultDto<IdentityUserDto>> GetUsersAsync(Guid id, GetIdentityUsersInput input);

    /// <summary>
    /// 添加用户到组织单元
    /// </summary>
    Task AddUsersAsync(Guid id, OrganizationUnitAddUserDto input);

    /// <summary>
    /// 从组织单元删除角色
    /// </summary>
    Task DeleteRoleAsync(Guid organizationUnitId, Guid roleId);

    /// <summary>
    /// 从组织单元删除用户
    /// </summary>
    Task DeleteUserAsync(Guid organizationUnitId, Guid userId);

    /// <summary>
    /// 获取用户的组织行政区代码列表
    /// </summary>
    Task<ICollection<string>?> GetOrganizationXzqdmAsync(Guid userId);
}

