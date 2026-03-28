using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OrganizationManagement.Authorization;

/// <summary>
/// 组织管理权限定义。
/// 在 AbpIdentity 组下注册 AbpIdentity.OrganizationUnits 及其子权限，
/// 供 PermissionManagementOptions.ProviderPolicies["OU"] 使用。
/// 必须使用完整命名空间名 "AbpIdentity.OrganizationUnits"，不能使用裸名 "OrganizationUnits"，
/// 否则会在 AbpIdentity 组下生成重复且未本地化的冗余条目。
/// </summary>
public class OrganizationManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public const string GroupName               = "AbpIdentity";
    public const string Default                 = "AbpIdentity.OrganizationUnits";
    public const string Create                  = Default + ".Create";
    public const string Update                  = Default + ".Update";
    public const string Delete                  = Default + ".Delete";
    public const string ManageMembers           = Default + ".ManageMembers";

    public override void Define(IPermissionDefinitionContext context)
    {
        // 若已由 ABP Identity 模块或其他 Provider 注册，则跳过，避免重复定义
        if (context.GetPermissionOrNull(Default) != null)
            return;

        var identityGroup = context.GetGroupOrNull(GroupName)
            ?? context.AddGroup(GroupName, L("Permission:AbpIdentity"));

        var ouPermission = identityGroup.AddPermission(Default, L("Permission:AbpIdentity:OrganizationUnits"));
        ouPermission.AddChild(Create,        L("Permission:AbpIdentity:OrganizationUnits:Create"));
        ouPermission.AddChild(Update,        L("Permission:AbpIdentity:OrganizationUnits:Update"));
        ouPermission.AddChild(Delete,        L("Permission:AbpIdentity:OrganizationUnits:Delete"));
        ouPermission.AddChild(ManageMembers, L("Permission:AbpIdentity:OrganizationUnits:ManageMembers"));
    }

    // 引用 Geo 本地化资源（zh-Hans.json 中包含所有 AbpIdentity:OrganizationUnits 相关翻译）
    private static LocalizableString L(string name) => new(name, "Geo");
}
