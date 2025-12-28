using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;

namespace OrganizationManagement.Organization;

/// <summary>
/// 组织单元权限管理提供者
/// </summary>
public class OrganizationUnitPermissionManagementProvider(
    IPermissionGrantRepository permissionGrantRepository,
    IGuidGenerator guidGenerator,
    ICurrentTenant currentTenant) : PermissionManagementProvider(
        permissionGrantRepository,
        guidGenerator,
        currentTenant)
{
    public override string Name => OrganizationUnitPermissionValueProvider.ProviderName;
}

