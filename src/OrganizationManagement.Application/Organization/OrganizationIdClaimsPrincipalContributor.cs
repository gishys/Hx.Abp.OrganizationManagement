using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;

namespace OrganizationManagement.Organization;

/// <summary>
/// 组织ID声明主体贡献者
/// </summary>
public class OrganizationIdClaimsPrincipalContributor : IAbpClaimsPrincipalContributor, ITransientDependency
{
    public async Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
    {
        var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
        var userId = identity?.FindAll(d => d.Type == "sub").FirstOrDefault();
        if (userId != null)
        {
            var userService = context.ServiceProvider.GetRequiredService<OrganizationUnitAppService>();
            var orgsResult = await userService.GetOrganizationUnitsByUserIdAsync(new Guid(userId.Value));
            var value = orgsResult?.Items == null || orgsResult.Items.Count == 0 
                ? "" 
                : string.Join(",", orgsResult.Items.Select(d => d.Id.ToString()));
            identity?.AddClaim(new Claim(AbpOrganizationUnitClaimTypes.OrganizationUnit, value));
        }
    }
}

/// <summary>
/// 当前用户扩展方法
/// </summary>
public static class CurrentUserExtensions
{
    /// <summary>
    /// 获取当前用户的组织ID
    /// </summary>
    public static string? GetOrganizationId(this ICurrentUser currentUser)
    {
        return currentUser.FindClaimValue(AbpOrganizationUnitClaimTypes.OrganizationUnit);
    }
}

