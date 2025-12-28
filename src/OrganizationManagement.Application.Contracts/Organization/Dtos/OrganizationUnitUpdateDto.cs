using Volo.Abp.ObjectExtending;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 更新组织单元数据传输对象
/// </summary>
public class OrganizationUnitUpdateDto : ExtensibleObject
{
    /// <summary>
    /// 组织单元显示名称
    /// </summary>
    public required string DisplayName { get; set; }
}

