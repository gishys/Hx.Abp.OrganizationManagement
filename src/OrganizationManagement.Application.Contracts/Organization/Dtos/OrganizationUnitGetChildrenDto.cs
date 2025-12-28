using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 获取子组织单元输入参数
/// </summary>
public class OrganizationUnitGetChildrenDto : IEntityDto<Guid>
{
    /// <summary>
    /// 组织单元ID
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// 是否递归获取所有子节点
    /// </summary>
    public bool Recursive { get; set; }
}

