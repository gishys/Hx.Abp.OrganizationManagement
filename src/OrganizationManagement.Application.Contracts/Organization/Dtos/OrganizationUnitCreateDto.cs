using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 创建组织单元数据传输对象
/// </summary>
public class OrganizationUnitCreateDto : ExtensibleObject
{
    /// <summary>
    /// 组织单元显示名称
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(OrganizationUnitConsts), nameof(OrganizationUnitConsts.MaxDisplayNameLength))]
    public required string DisplayName { get; set; }

    /// <summary>
    /// 父组织单元ID
    /// </summary>
    public Guid? ParentId { get; set; }
}

