using OrganizationManagement.Organization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace OrganizationManagement;

/// <summary>
/// 组织管理应用模块
/// </summary>
[DependsOn(
    typeof(OrganizationManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationModule)
)]
public class OrganizationManagementApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<OrganizationManagementApplicationModule>();
        });

        Configure<AbpPermissionOptions>(options =>
        {
            options.ValueProviders.Add<OrganizationUnitPermissionValueProvider>();
        });

        // 注册组织单元权限管理提供者，供权限管理界面按 providerName=OU 获取/设置组织权限
        Configure<PermissionManagementOptions>(options =>
        {
            options.ManagementProviders.Add<OrganizationUnitPermissionManagementProvider>();
        });
    }
}

