using System;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 移动组织单元数据传输对象
/// </summary>
public class OrganizationUnitMoveDto
{
    /// <summary>
    /// 新的父组织单元ID
    /// </summary>
    public Guid? ParentId { get; set; }
}

