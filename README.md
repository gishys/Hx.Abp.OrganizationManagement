# OrganizationManagement

组织管理模块 - 基于 ABP vNext 框架的最佳实践实现

## 项目结构

```
OrganizationManagement/
├── src/
│   ├── OrganizationManagement.Application.Contracts/    # 应用协议层
│   │   └── Organization/
│   │       ├── Dtos/                                    # 数据传输对象
│   │       └── IOrganizationUnitAppService.cs           # 应用服务接口
│   │
│   ├── OrganizationManagement.Application/               # 应用层
│   │   └── Organization/
│   │       ├── OrganizationUnitAppService.cs              # 应用服务实现
│   │       ├── OrganizationIdClaimsPrincipalContributor.cs # Claims贡献者
│   │       ├── OrganizationUnitPermissionValueProvider.cs # 权限值提供者
│   │       ├── OrganizationUnitPermissionManagementProvider.cs # 权限管理提供者
│   │       └── AbpOrganizationUnitClaimTypes.cs          # 声明类型常量
│   │
│   ├── OrganizationManagement.HttpApi/                   # HttpApi层
│   │   ├── Controllers/
│   │   │   └── OrganizationUnitController.cs            # API控制器
│   │   └── OrganizationManagementHttpApiModule.cs
│   │
│   └── OrganizationManagement.HttpApi.Host/              # Web API启动项目
│       ├── Program.cs                                    # 程序入口
│       ├── appsettings.json                              # 配置文件
│       └── OrganizationManagementHttpApiHostModule.cs   # Host模块
│
└── common.props                                          # 公共项目属性
```

## 功能特性

### 核心功能

1. **组织单元管理**
   - 创建、更新、删除组织单元
   - 获取组织单元列表（分页、排序、过滤）
   - 获取根组织单元
   - 查找子组织单元（支持递归）
   - 移动组织单元

2. **用户管理**
   - 添加用户到组织单元
   - 从组织单元移除用户
   - 获取组织单元的用户列表
   - 获取未添加到组织单元的用户列表

3. **角色管理**
   - 添加角色到组织单元
   - 从组织单元移除角色
   - 获取组织单元的角色列表
   - 获取未添加到组织单元的角色列表

4. **权限管理**
   - 基于组织单元的权限值提供者
   - 组织单元权限管理提供者
   - 支持在权限系统中基于组织单元进行权限控制

5. **Claims集成**
   - 自动将用户的组织单元信息添加到Claims中
   - 提供扩展方法获取当前用户的组织ID

## 快速开始

### 1. 运行 Web API 项目

```bash
cd src/OrganizationManagement.HttpApi.Host
dotnet run
```

项目将在 `http://localhost:5000` 启动。

### 2. 访问 Swagger UI

打开浏览器访问：`http://localhost:5000/swagger`

### 3. API 端点

所有组织管理相关的 API 端点都在 `/api/organization/organization-units` 路径下：

- `GET /api/organization/organization-units` - 获取组织单元列表
- `GET /api/organization/organization-units/{id}` - 获取单个组织单元
- `POST /api/organization/organization-units` - 创建组织单元
- `PUT /api/organization/organization-units/{id}` - 更新组织单元
- `DELETE /api/organization/organization-units/{id}` - 删除组织单元
- `GET /api/organization/organization-units/root` - 获取根组织单元
- `POST /api/organization/organization-units/children` - 查找子组织单元
- `PUT /api/organization/organization-units/{id}/move` - 移动组织单元
- `GET /api/organization/organization-units/{id}/users` - 获取组织单元的用户列表
- `POST /api/organization/organization-units/{id}/users` - 添加用户到组织单元
- `DELETE /api/organization/organization-units/{id}/users/{userId}` - 从组织单元移除用户
- `GET /api/organization/organization-units/{id}/roles` - 获取组织单元的角色列表
- `POST /api/organization/organization-units/{id}/roles` - 添加角色到组织单元
- `DELETE /api/organization/organization-units/{id}/roles/{roleId}` - 从组织单元移除角色

## 配置说明

### 数据库配置

在 `appsettings.json` 中配置数据库连接字符串：

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=OrganizationManagement;Uid=root;Pwd=root;Port=3306;"
  }
}
```

**注意**：当前项目需要引用 EntityFrameworkCore 层才能连接数据库。如果只是测试 API 功能，可以：

1. 使用内存数据库进行测试
2. 或者引用你现有的 EntityFrameworkCore 项目

### 认证配置

在 `appsettings.json` 中配置认证服务器：

```json
{
  "AuthServer": {
    "Authority": "http://localhost:5000",
    "RequireHttpsMetadata": "false"
  }
}
```

### CORS 配置

在 `appsettings.json` 中配置允许的跨域来源：

```json
{
  "App": {
    "CorsOrigins": "http://localhost:3000,http://localhost:5000"
  }
}
```

## 使用说明

### 1. 安装依赖

确保你的项目引用了以下ABP包：
- `Volo.Abp.Identity.Application` (8.1.1)
- `Volo.Abp.Identity.Application.Contracts` (8.1.1)
- `Volo.Abp.PermissionManagement.Application` (8.1.1)
- `Volo.Abp.ObjectExtending` (8.1.1)
- `Volo.Abp.Identity.HttpApi` (8.1.1)
- `Volo.Abp.Swashbuckle` (8.1.1)

### 2. 模块依赖

在你的主应用模块中添加依赖：

```csharp
[DependsOn(typeof(OrganizationManagementApplicationModule))]
public class YourApplicationModule : AbpModule
{
    // ...
}
```

### 3. 使用服务

```csharp
public class YourAppService : ApplicationService
{
    private readonly IOrganizationUnitAppService _organizationUnitAppService;

    public YourAppService(IOrganizationUnitAppService organizationUnitAppService)
    {
        _organizationUnitAppService = organizationUnitAppService;
    }

    public async Task CreateOrganizationAsync()
    {
        var input = new OrganizationUnitCreateDto
        {
            DisplayName = "技术部",
            ParentId = null
        };
        
        var result = await _organizationUnitAppService.CreateAsync(input);
    }
}
```

### 4. 权限配置

权限提供者会自动注册，你可以在权限管理界面中为组织单元分配权限。

### 5. 获取当前用户组织

```csharp
var organizationId = CurrentUser.GetOrganizationId();
```

## 测试 API

### 使用 Swagger UI

1. 启动项目后访问 `http://localhost:5000/swagger`
2. 在 Swagger UI 中可以测试所有 API 端点
3. 需要认证的接口，先调用认证接口获取 token

### 使用 Postman 或 curl

```bash
# 获取组织单元列表
curl -X GET "http://localhost:5000/api/organization/organization-units" \
  -H "Authorization: Bearer YOUR_TOKEN"

# 创建组织单元
curl -X POST "http://localhost:5000/api/organization/organization-units" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -d '{
    "displayName": "技术部",
    "parentId": null
  }'
```

## 技术栈

- .NET 8.0
- ABP Framework 8.1.1
- AutoMapper（用于对象映射）
- Swagger/OpenAPI（API 文档）

## 最佳实践

1. **分层架构**：严格遵循ABP的分层架构，Contracts层只包含接口和DTO，Application层包含实现
2. **依赖注入**：所有服务都通过构造函数注入，遵循依赖倒置原则
3. **异步编程**：所有方法都使用async/await模式
4. **异常处理**：利用ABP框架的异常处理机制
5. **工作单元**：使用ABP的工作单元模式管理事务
6. **对象映射**：使用AutoMapper进行对象映射，减少手动映射代码
7. **扩展性**：支持通过ExtraProperties扩展组织单元属性
8. **RESTful API**：遵循RESTful API设计规范

## 扩展说明

### 扩展组织单元属性

可以通过ExtraProperties扩展组织单元属性，例如：

```csharp
var input = new OrganizationUnitCreateDto
{
    DisplayName = "技术部",
    ParentId = null
};

input.ExtraProperties["XZQDM"] = "110101,110102"; // 行政区代码
```

### 自定义权限提供者

如果需要自定义权限逻辑，可以继承`OrganizationUnitPermissionValueProvider`并重写相应方法。

## 注意事项

1. **数据库连接**：当前 HttpApi.Host 项目需要引用 EntityFrameworkCore 层才能连接数据库。如果只是测试 API，可以考虑使用内存数据库。
2. **认证配置**：确保正确配置了认证服务器，否则需要认证的 API 将无法访问。
3. **CORS 配置**：如果从不同域名访问 API，需要正确配置 CORS。
4. **权限提供者**：权限提供者需要在模块配置中注册（已自动注册）。
5. **Claims贡献者**：Claims贡献者会自动将组织信息添加到用户Claims中。

## 版本历史

- 1.0.0 - 初始版本，包含完整的组织管理功能和 Web API 测试项目
