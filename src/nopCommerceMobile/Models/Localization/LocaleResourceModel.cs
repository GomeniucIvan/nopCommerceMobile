using nopCommerceMobile.Models.Base;

namespace nopCommerceMobile.Models.Localization
{
    public class LocaleResourceModel : BaseModel
    {
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public int LanguageId { get; set; }
    }

    public class LocaleResource
    {
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
        public int LanguageId { get; set; }
    }
}
