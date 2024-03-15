using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using EraTech.AdministrationService.DbMigrations;
using EraTech.AdministrationService.EntityFrameworkCore;
using EraTech.Shared.Hosting.Settings.Microservices;
using Volo.Abp;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

namespace EraTech.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceHttpApiModule),
    typeof(AdministrationServiceApplicationModule),
    typeof(AdministrationServiceEntityFrameworkCoreModule),
    typeof(EraTechSharedHostingSettingsMicroservicesModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpIdentityHttpApiClientModule)
)]
public class AdministrationServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, configuration["AuthServer:ApiName"]);


        Configure<PermissionManagementOptions>(options => { options.IsDynamicPermissionStoreEnabled = true; });

        Configure<SettingManagementOptions>(options => { options.IsDynamicSettingStoreEnabled = true; });

        context.Services.AddHealthChecks()
                        .AddNpgSql(
                            configuration["ConnectionStrings:AdministrationService"],
                            name: "AdministrationService Database",
                            tags: new string[] { "Database", "AdministrationService" });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        base.OnApplicationInitialization(context);

        var app = context.GetApplicationBuilder();

        app.UseEndpoints(config =>
        {
            config.MapHealthChecks("/eratech/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false,
            });
        });
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<AdministrationServiceDatabaseMigrationChecker>()
            .CheckAndApplyDatabaseMigrationsAsync();
    }
}
