using EraTech.AdministrationService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EraTech.AdministrationService;

public abstract class AdministrationServiceController : AbpControllerBase
{
    protected AdministrationServiceController()
    {
        LocalizationResource = typeof(AdministrationServiceResource);
    }
}
