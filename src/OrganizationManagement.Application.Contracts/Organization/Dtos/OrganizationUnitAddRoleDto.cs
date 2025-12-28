using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 添加角色到组织单元数据传输对象
/// </summary>
public class OrganizationUnitAddRoleDto
{
    /// <summary>
    /// 角色ID列表
    /// </summary>
    [Required]
    public required List<Guid> RoleIds { get; set; }
}

