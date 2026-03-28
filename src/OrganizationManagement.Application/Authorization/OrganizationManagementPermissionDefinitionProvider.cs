using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OrganizationManagement.Authorization;

/// <summary>
/// 组织管理权限定义。
/// 确保 AbpIdentity.OrganizationUnits 权限已注册，供 PermissionManagementOptions.ProviderPolicies["OU"] 使用，
/// 否则授权校验会报 "No policy found: AbpIdentity.OrganizationUnits"。
/// 注意：AddPermission 必须使用完整命名空间名 "AbpIdentity.OrganizationUnits"，
/// 不能使用裸名 "OrganizationUnits"，否则会在 AbpIdentity 组下生成重复且未本地化的冗余条目。
/// </summary>
public class OrganizationManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public const string OrganizationUnitsPermissionName = "AbpIdentity.OrganizationUnits";

    public override void Define(IPermissionDefinitionContext context)
    {
        // 若 ABP Identity 模块或其他 Provider 已注册该权限，则跳过，避免重复定义
        if (context.GetPermissionOrNull(OrganizationUnitsPermissionName) != null)
            return;

        var identityGroup = context.GetGroupOrNull("AbpIdentity")
            ?? context.AddGroup("AbpIdentity", L("Permission:AbpIdentity"));

        // 使用完整命名空间权限名，与 AbpIdentityOrganizationUnitPermissionDefinitionProvider 保持一致
        identityGroup.AddPermission(OrganizationUnitsPermissionName, L("Permission:AbpIdentity:OrganizationUnits"));
    }

    // 使用 Geo 本地化资源，zh-Hans.json 中已有 "Permission:AbpIdentity:OrganizationUnits" 的翻译
    private static LocalizableString L(string name) => new(name, "Geo");
}
