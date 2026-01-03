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
/// 提供组织单元的创建、查询、更新、删除以及组织单元与用户、角色的关联管理功能
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
    /// <param name="input">组织单元创建信息</param>
    /// <returns>创建的组织单元信息</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrganizationUnitDto), 200)]
    [ProducesResponseType(400)]
    public Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
    {
        return _organizationUnitAppService.CreateAsync(input);
    }

    /// <summary>
    /// 删除组织单元
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <returns>无返回值</returns>
    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Task DeleteAsync(Guid id)
    {
        return _organizationUnitAppService.DeleteAsync(id);
    }

    /// <summary>
    /// 根据ID获取组织单元详情
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <returns>组织单元信息</returns>
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(OrganizationUnitDto), 200)]
    [ProducesResponseType(404)]
    public Task<OrganizationUnitDto> GetAsync(Guid id)
    {
        return _organizationUnitAppService.GetAsync(id);
    }

    /// <summary>
    /// 分页获取组织单元列表
    /// </summary>
    /// <param name="input">查询条件，包含排序、分页等信息</param>
    /// <returns>分页的组织单元列表</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<OrganizationUnitDto>), 200)]
    public Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(OrganizationUnitGetListInput input)
    {
        return _organizationUnitAppService.GetListAsync(input);
    }

    /// <summary>
    /// 更新组织单元信息
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="input">组织单元更新信息</param>
    /// <returns>更新后的组织单元信息</returns>
    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(OrganizationUnitDto), 200)]
    [ProducesResponseType(404)]
    public Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
    {
        return _organizationUnitAppService.UpdateAsync(id, input);
    }

    /// <summary>
    /// 获取所有组织单元列表（不分页）
    /// </summary>
    /// <returns>所有组织单元列表</returns>
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(ListResultDto<OrganizationUnitDto>), 200)]
    public Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
    {
        return _organizationUnitAppService.GetAllListAsync();
    }

    /// <summary>
    /// 获取指定父组织单元的最后一个子组织单元
    /// </summary>
    /// <param name="parentId">父组织单元ID，为空时获取根组织单元的最后一个子组织单元</param>
    /// <returns>最后一个子组织单元，如果不存在则返回null</returns>
    [HttpGet]
    [Route("last-child")]
    [ProducesResponseType(typeof(OrganizationUnitDto), 200)]
    public Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId)
    {
        return _organizationUnitAppService.GetLastChildOrNullAsync(parentId);
    }

    /// <summary>
    /// 移动组织单元到新的父组织单元下
    /// </summary>
    /// <param name="id">要移动的组织单元ID</param>
    /// <param name="input">移动信息，包含新的父组织单元ID</param>
    /// <returns>无返回值</returns>
    [HttpPut]
    [Route("{id}/move")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Task MoveAsync(Guid id, OrganizationUnitMoveDto input)
    {
        return _organizationUnitAppService.MoveAsync(id, input);
    }

    /// <summary>
    /// 获取根组织单元列表
    /// </summary>
    /// <param name="recursive">是否递归获取所有子组织单元，默认为false</param>
    /// <returns>根组织单元列表</returns>
    [HttpGet]
    [Route("root")]
    [ProducesResponseType(typeof(ListResultDto<OrganizationUnitDto>), 200)]
    public Task<ListResultDto<OrganizationUnitDto>> GetRootAsync(bool recursive = false)
    {
        return _organizationUnitAppService.GetRootAsync(recursive);
    }

    /// <summary>
    /// 查找指定组织单元的子组织单元列表
    /// </summary>
    /// <param name="input">查询条件，包含父组织单元ID和是否递归查询</param>
    /// <returns>子组织单元列表</returns>
    [HttpPost]
    [Route("children")]
    [ProducesResponseType(typeof(ListResultDto<OrganizationUnitDto>), 200)]
    public Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(OrganizationUnitGetChildrenDto input)
    {
        return _organizationUnitAppService.FindChildrenAsync(input);
    }

    /// <summary>
    /// 获取组织单元中的角色列表（不分页）
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <returns>角色列表</returns>
    [HttpGet]
    [Route("{id}/roles-list")]
    [ProducesResponseType(typeof(ListResultDto<Volo.Abp.Identity.IdentityRoleDto>), 200)]
    [ProducesResponseType(404)]
    public Task<ListResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetRoleNamesAsync(Guid id)
    {
        return _organizationUnitAppService.GetRoleNamesAsync(id);
    }

    /// <summary>
    /// 获取未添加到指定组织单元的角色列表（分页）
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="input">查询条件，包含排序、分页、过滤等信息</param>
    /// <returns>未添加到组织单元的角色列表</returns>
    [HttpGet]
    [Route("{id}/unadded-roles")]
    [ProducesResponseType(typeof(PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>), 200)]
    [ProducesResponseType(404)]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, OrganizationUnitGetUnaddedRoleListInput input)
    {
        return _organizationUnitAppService.GetUnaddedRolesAsync(id, input);
    }

    /// <summary>
    /// 获取组织单元中的角色列表（分页）
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="input">查询条件，包含排序、分页等信息</param>
    /// <returns>组织单元的角色列表</returns>
    [HttpGet]
    [Route("{id}/roles")]
    [ProducesResponseType(typeof(PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>), 200)]
    [ProducesResponseType(404)]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input)
    {
        return _organizationUnitAppService.GetRolesAsync(id, input);
    }

    /// <summary>
    /// 添加角色到组织单元
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="input">要添加的角色ID列表</param>
    /// <returns>无返回值</returns>
    [HttpPost]
    [Route("{id}/roles")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Task AddRolesAsync(Guid id, OrganizationUnitAddRoleDto input)
    {
        return _organizationUnitAppService.AddRolesAsync(id, input);
    }

    /// <summary>
    /// 从组织单元中删除角色
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="roleId">要删除的角色ID</param>
    /// <returns>无返回值</returns>
    [HttpDelete]
    [Route("{id}/roles/{roleId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Task DeleteRoleAsync(Guid id, Guid roleId)
    {
        return _organizationUnitAppService.DeleteRoleAsync(id, roleId);
    }

    /// <summary>
    /// 获取未添加到指定组织单元的用户列表（分页）
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="input">查询条件，包含排序、分页、过滤等信息</param>
    /// <returns>未添加到组织单元的用户列表</returns>
    [HttpGet]
    [Route("{id}/unadded-users")]
    [ProducesResponseType(typeof(PagedResultDto<Volo.Abp.Identity.IdentityUserDto>), 200)]
    [ProducesResponseType(404)]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityUserDto>> GetUnaddedUsersAsync(Guid id, OrganizationUnitGetUnaddedUserListInput input)
    {
        return _organizationUnitAppService.GetUnaddedUsersAsync(id, input);
    }

    /// <summary>
    /// 获取组织单元中的用户列表（分页）
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="input">查询条件，包含排序、分页、过滤等信息</param>
    /// <returns>组织单元的用户列表</returns>
    [HttpGet]
    [Route("{id}/users")]
    [ProducesResponseType(typeof(PagedResultDto<Volo.Abp.Identity.IdentityUserDto>), 200)]
    [ProducesResponseType(404)]
    public Task<PagedResultDto<Volo.Abp.Identity.IdentityUserDto>> GetUsersAsync(Guid id, Volo.Abp.Identity.GetIdentityUsersInput input)
    {
        return _organizationUnitAppService.GetUsersAsync(id, input);
    }

    /// <summary>
    /// 添加用户到组织单元
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="input">要添加的用户ID列表</param>
    /// <returns>无返回值</returns>
    [HttpPost]
    [Route("{id}/users")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Task AddUsersAsync(Guid id, OrganizationUnitAddUserDto input)
    {
        return _organizationUnitAppService.AddUsersAsync(id, input);
    }

    /// <summary>
    /// 从组织单元中删除用户
    /// </summary>
    /// <param name="id">组织单元ID</param>
    /// <param name="userId">要删除的用户ID</param>
    /// <returns>无返回值</returns>
    [HttpDelete]
    [Route("{id}/users/{userId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public Task DeleteUserAsync(Guid id, Guid userId)
    {
        return _organizationUnitAppService.DeleteUserAsync(id, userId);
    }

    /// <summary>
    /// 根据用户ID获取该用户所属的所有组织单元列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户所属的组织单元列表</returns>
    [HttpGet]
    [Route("users/{userId}/organization-units")]
    [ProducesResponseType(typeof(ListResultDto<OrganizationUnitDto>), 200)]
    [ProducesResponseType(404)]
    public Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsByUserIdAsync(Guid userId)
    {
        return _organizationUnitAppService.GetOrganizationUnitsByUserIdAsync(userId);
    }

    /// <summary>
    /// 根据角色ID获取该角色所属的所有组织单元列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色所属的组织单元列表</returns>
    [HttpGet]
    [Route("roles/{roleId}/organization-units")]
    [ProducesResponseType(typeof(ListResultDto<OrganizationUnitDto>), 200)]
    [ProducesResponseType(404)]
    public Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsByRoleIdAsync(Guid roleId)
    {
        return _organizationUnitAppService.GetOrganizationUnitsByRoleIdAsync(roleId);
    }
}
