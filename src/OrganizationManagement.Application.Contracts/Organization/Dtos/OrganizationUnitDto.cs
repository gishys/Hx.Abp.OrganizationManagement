using System;
using Volo.Abp.Application.Dtos;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 组织单元数据传输对象
/// </summary>
public class OrganizationUnitDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// 父组织单元ID
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 组织单元编码
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// 组织单元显示名称
    /// </summary>
    public required string DisplayName { get; set; }
}

