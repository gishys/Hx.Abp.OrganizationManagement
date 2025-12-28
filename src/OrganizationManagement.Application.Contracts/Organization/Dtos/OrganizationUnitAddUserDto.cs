using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 添加用户到组织单元数据传输对象
/// </summary>
public class OrganizationUnitAddUserDto
{
    /// <summary>
    /// 用户ID列表
    /// </summary>
    [Required]
    public required List<Guid> UserIds { get; set; }
}

