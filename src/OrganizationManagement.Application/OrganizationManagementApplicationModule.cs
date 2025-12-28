using OrganizationManagement.Organization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

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
    }
}

