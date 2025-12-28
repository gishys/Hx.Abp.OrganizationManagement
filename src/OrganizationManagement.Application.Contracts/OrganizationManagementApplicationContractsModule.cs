using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectExtending.Modularity;
using Volo.Abp.Threading;

namespace OrganizationManagement;

[DependsOn(
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpObjectExtendingModule)
)]
public class OrganizationManagementApplicationContractsModule : AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // 可以在这里配置DTO扩展
    }

    public override void PostConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ModuleExtensionConfigurationHelper.ApplyEntityConfigurationToApi(
                IdentityModuleExtensionConsts.ModuleName,
                IdentityModuleExtensionConsts.EntityNames.OrganizationUnit,
                getApiTypes: [typeof(Organization.Dtos.OrganizationUnitDto)],
                createApiTypes: [typeof(Organization.Dtos.OrganizationUnitCreateDto)],
                updateApiTypes: [typeof(Organization.Dtos.OrganizationUnitUpdateDto)]
            );
        });
    }
}

