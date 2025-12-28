using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using OrganizationManagement.Organization;
using OrganizationManagement.Organization.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace OrganizationManagement.Controllers;

/// <summary>
/// 组织单元控制器
/// </summary>
[RemoteService(Name = "OrganizationManagement")]
[Area("organization")]
[ControllerName("OrganizationUnit")]
[Route("api/organization/organization-units")]
public class OrganizationUnitController(IOrganizationUnitAppService organizationUnitAppService) : AbpControllerBase, IOrganizationUnitAppService
{
    private readonly IOrganizationUnitAppService _organizationUnitAppService = organizationUnitAppService;

    /// <summary>
    /// 创建组织单元
    /// </summary>
    [HttpPost]
    public Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
    {
        return _organizationUnitAppService.CreateAsync(input);
    }

    /// <summary>
    /// 删除组织单元
    /// </summary>
    [HttpDelete]
    [Route("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return _organizationUnitAppService.DeleteAsync(id);
    }

    /// <summary>
    /// 获取组织单元
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    public Task<OrganizationUnitDto> GetAsync(Guid id)
    {
        return _organizationUnitAppService.GetAsync(id);
    }

    /// <summary>
    /// 获取组织单元列表
    /// </summary>
    [HttpGet]
    public Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(OrganizationUnitGetListInput input)
    {
        return _organizationUnitAppService.GetListAsync(input);
    }

    /// <summary>
    /// 更新组织单元
    /// </summary>
    [HttpPut]
    [Route("{id}")]
    public Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
    {
        return _organizationUnitAppService.UpdateAsync(id, input);
    }

    /// <summary>
    /// 获取所有组织单元列表
    /// </summary>
    [HttpGet]
    [Route("all")]
    public Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
    {
        return _organizationUnitAppService.GetAllListAsync();
    }

    /// <summary>
    /// 获取最后一个子组织单元或null
    /// </summary>
    [HttpGet]
    [Route("last-child")]
    public Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId)
    {
        return _organizationUnitAppService.GetLastChildOrNullAsync(parentId);
    }

    /// <summary>
    /// 移动组织单元
    /// </summary>
    [HttpPut]
    [Route("{id}/move")]
    public Task MoveAsync(Guid id, OrganizationUnitMoveDto input)
    {
        return _organizationUnitAppService.MoveAsync(id, input);
    }

    /// <summary>
    /// 获取根组织单元
    /// </summary>
    [HttpGet]
    [Route("root")]
    public Task<ListResultDto<OrganizationUnitDto>> GetRootAsync(bool recursive = false)
    {
        return _organizationUnitAppService.GetRootAsync(recursive);
    }

    /// <summary>
    /// 查找子组织单元
    /// </summary>
    [HttpPost]
    [Route("children")]
    public Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(OrganizationUnitGetChildrenDto input)
    {
        return _organizationUnitAppService.FindChildrenAsync(input);
    }

    /// <summary>
    /// 获取组织单元的角色名称列表
    /// </summary>
    [HttpGet]
    [Route("{id}/role-names")]
    public Task<ListResultDto<string>> GetRoleNamesAsync(Guid id)
    {
        return _organizationUnitAppService.GetRoleNamesAsync(id);
    }

    /// <summary>
    /// 获取未添加到组织单元的角色列表
    /// </summary>
    [HttpGet]
    [Route("{id}/unadded-roles")]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, OrganizationUnitGetUnaddedRoleListInput input)
    {
        return _organizationUnitAppService.GetUnaddedRolesAsync(id, input);
    }

    /// <summary>
    /// 获取组织单元的角色列表
    /// </summary>
    [HttpGet]
    [Route("{id}/roles")]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input)
    {
        return _organizationUnitAppService.GetRolesAsync(id, input);
    }

    /// <summary>
    /// 添加角色到组织单元
    /// </summary>
    [HttpPost]
    [Route("{id}/roles")]
    public Task AddRolesAsync(Guid id, OrganizationUnitAddRoleDto input)
    {
        return _organizationUnitAppService.AddRolesAsync(id, input);
    }

    /// <summary>
    /// 从组织单元删除角色
    /// </summary>
    [HttpDelete]
    [Route("{id}/roles/{roleId}")]
    public Task DeleteRoleAsync(Guid id, Guid roleId)
    {
        return _organizationUnitAppService.DeleteRoleAsync(id, roleId);
    }

    /// <summary>
    /// 获取未添加到组织单元的用户列表
    /// </summary>
    [HttpGet]
    [Route("{id}/unadded-users")]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityUserDto>> GetUnaddedUsersAsync(Guid id, OrganizationUnitGetUnaddedUserListInput input)
    {
        return _organizationUnitAppService.GetUnaddedUsersAsync(id, input);
    }

    /// <summary>
    /// 获取组织单元的用户列表
    /// </summary>
    [HttpGet]
    [Route("{id}/users")]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityUserDto>> GetUsersAsync(Guid id, Volo.Abp.Identity.GetIdentityUsersInput input)
    {
        return _organizationUnitAppService.GetUsersAsync(id, input);
    }

    /// <summary>
    /// 添加用户到组织单元
    /// </summary>
    [HttpPost]
    [Route("{id}/users")]
    public Task AddUsersAsync(Guid id, OrganizationUnitAddUserDto input)
    {
        return _organizationUnitAppService.AddUsersAsync(id, input);
    }

    /// <summary>
    /// 从组织单元删除用户
    /// </summary>
    [HttpDelete]
    [Route("{id}/users/{userId}")]
    public Task DeleteUserAsync(Guid id, Guid userId)
    {
        return _organizationUnitAppService.DeleteUserAsync(id, userId);
    }

    /// <summary>
    /// 获取用户的组织行政区代码列表
    /// </summary>
    [HttpGet]
    [Route("users/{userId}/xzqdm")]
    public Task<System.Collections.Generic.ICollection<string>?> GetOrganizationXzqdmAsync(Guid userId)
    {
        return _organizationUnitAppService.GetOrganizationXzqdmAsync(userId);
    }
}

