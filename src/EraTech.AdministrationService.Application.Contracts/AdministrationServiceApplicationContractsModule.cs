using EraTech.AspNetCore.Themes.Localization;
using EraTech.NotificationService;
using EraTech.StorageService;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
namespace EraTech.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceDomainSharedModule),
    typeof(AbpPermissionManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    //typeof(TenTimeIdentityApplicationContractsModule),
    //typeof(TenTimeIdentityServerApplicationContractsModule),
    typeof(NotificationServiceApplicationContractsModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(EraTechAspNetCoreThemesLocalizationModule),
    typeof(StorageServiceApplicationContractsModule)
    
    )]
public class AdministrationServiceApplicationContractsModule : AbpModule
{

}
