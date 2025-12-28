using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;

namespace OrganizationManagement.Organization;

/// <summary>
/// 组织单元权限值提供者
/// </summary>
public class OrganizationUnitPermissionValueProvider(
    IPermissionStore permissionStore) : PermissionValueProvider(permissionStore)
{
    public const string ProviderName = "O";

    public override string Name => ProviderName;

    public override async Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
    {
        var organizationUnits = context.Principal?.FindAll(AbpOrganizationUnitClaimTypes.OrganizationUnit).Select(c => c.Value).ToArray();

        if (organizationUnits.IsNullOrEmpty())
        {
            return PermissionGrantResult.Undefined;
        }
        if (organizationUnits != null)
        {
            foreach (var organizationUnit in organizationUnits.Distinct())
            {
                if (string.IsNullOrEmpty(organizationUnit)) continue;
                var orgIds = organizationUnit.Split(',');
                foreach (var orgId in orgIds)
                {
                    if (await PermissionStore.IsGrantedAsync(context.Permission.Name, Name, orgId))
                    {
                        return PermissionGrantResult.Granted;
                    }
                }
            }
        }
        return PermissionGrantResult.Undefined;
    }

    public override async Task<MultiplePermissionGrantResult> CheckAsync(PermissionValuesCheckContext context)
    {
        var permissionNames = context.Permissions.Select(x => x.Name).Distinct().ToList();

        Check.NotNullOrEmpty(permissionNames, nameof(permissionNames));

        var result = new MultiplePermissionGrantResult([.. permissionNames]);

        var organizationUnits = context.Principal?.FindAll(AbpOrganizationUnitClaimTypes.OrganizationUnit).Select(c => c.Value).ToArray();

        if (organizationUnits.IsNullOrEmpty())
        {
            return result;
        }
        if (organizationUnits != null)
        {
            foreach (var organizationUnit in organizationUnits.Distinct())
            {
                var orgIds = organizationUnit.Split(',');
                foreach (var orgId in orgIds)
                {
                    var multipleResult = await PermissionStore.IsGrantedAsync([.. permissionNames], Name, orgId);

                    foreach (var grantResult in multipleResult.Result.Where(grantResult =>
                        result.Result.ContainsKey(grantResult.Key) &&
                        result.Result[grantResult.Key] == PermissionGrantResult.Undefined &&
                        grantResult.Value != PermissionGrantResult.Undefined))
                    {
                        result.Result[grantResult.Key] = grantResult.Value;
                        permissionNames.RemoveAll(x => x == grantResult.Key);
                    }

                    if (result.AllGranted || result.AllProhibited)
                    {
                        break;
                    }

                    if (permissionNames.IsNullOrEmpty())
                    {
                        break;
                    }
                }
            }
        }
        return result;
    }
}

