namespace EraTech.AdministrationService;

public static class AdministrationServiceErrorCodes
{
    //Add your business exception error codes here...
    public static class Validation
    {
        public const string NameLength = "CMSService:Validation:NameLength";
        public const string ArabicSignupPageImageIsRequired = "AdministrationService:Validation:ArabicSignupPageImageIsRequired";
        public const string EnglishSignupPageImageIsRequired = "AdministrationService:Validation:EnglishSignupPageImageIsRequired";
    }
}
