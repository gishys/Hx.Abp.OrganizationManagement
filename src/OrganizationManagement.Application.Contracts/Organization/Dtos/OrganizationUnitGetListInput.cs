using Volo.Abp.Application.Dtos;

namespace OrganizationManagement.Organization.Dtos;

/// <summary>
/// 获取组织单元列表输入参数
/// </summary>
public class OrganizationUnitGetListInput : IPagedAndSortedResultRequest
{
    /// <summary>
    /// 过滤条件
    /// </summary>
    public string? Filter { get; set; }

    /// <summary>
    /// 跳过数量
    /// </summary>
    public int SkipCount { get; set; }

    /// <summary>
    /// 最大返回数量
    /// </summary>
    public int MaxResultCount { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    public string? Sorting { get; set; }
}

