using System.Collections.Generic;
using Volo.Abp.Identity;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 包含用户列表的组织单元数据传输对象
/// </summary>
public class OrganizationUnitUsersDto : OrganizationUnitDto
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public required ICollection<IdentityUserDto> Users { get; set; }
}

