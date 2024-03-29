﻿using EraTech.AdministrationService.Localization;
using Volo.Abp.Application.Services;

namespace EraTech.AdministrationService;

public abstract class AdministrationServiceAppService : ApplicationService
{
    protected AdministrationServiceAppService()
    {
        LocalizationResource = typeof(AdministrationServiceResource);
        ObjectMapperContext = typeof(AdministrationServiceApplicationModule);
    }
}
