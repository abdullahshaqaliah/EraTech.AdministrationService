using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace EraTech.AdministrationService.EntityFrameworkCore;

[DependsOn(
        typeof(AdministrationServiceDomainModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule)
        )]


public class AdministrationServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<AdministrationServiceDbContext>(options =>
        {
            options.ReplaceDbContext<IPermissionManagementDbContext>();
            options.ReplaceDbContext<ISettingManagementDbContext>();
            options.ReplaceDbContext<IAuditLoggingDbContext>();
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        AbpCommonDbProperties.DbTablePrefix = "";
        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure<AdministrationServiceDbContext>(c =>
            {
                c.UseNpgsql(b =>
                {
                    b.MigrationsHistoryTable("__AdministrationService_Migrations");
                });
            });
        });
    }
}
