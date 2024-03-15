using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EraTech.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpSettingManagementHttpApiClientModule),
    typeof(AbpFeatureManagementHttpApiClientModule)
)]
public class AdministrationServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(AdministrationServiceApplicationContractsModule).Assembly,
            AdministrationServiceRemoteServiceConsts.RemoteServiceName
        );

        //Configure<AbpVirtualFileSystemOptions>(options =>
        //{
        //    options.FileSets.AddEmbedded<AdministrationServiceHttpApiClientModule>();
        //});

    }
}
