using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OrganizationManagement.Authorization;

/// <summary>
/// 组织管理权限定义。
/// 定义 AbpIdentity.OrganizationUnits 权限，供 PermissionManagementOptions.ProviderPolicies["OU"] 使用，
/// 否则授权校验会报 "No policy found: AbpIdentity.OrganizationUnits"。
/// </summary>
public class OrganizationManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public const string OrganizationUnitsPermissionName = "AbpIdentity.OrganizationUnits";

    public override void Define(IPermissionDefinitionContext context)
    {
        // 若 ABP Identity 未定义组织单元权限（如使用自定义组织模块时），则在此定义
        if (context.GetPermissionOrNull(OrganizationUnitsPermissionName) != null)
            return;

        var identityGroup = context.GetGroupOrNull("AbpIdentity")
            ?? context.AddGroup("AbpIdentity", L("Permission:AbpIdentity"));

        identityGroup.AddPermission("OrganizationUnits", L("Permission:OrganizationUnits"));
    }

    private static LocalizableString L(string name)
    {
        return new LocalizableString(name, "AbpIdentity");
    }
}
