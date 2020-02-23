using nopCommerceMobile.Models.Base;

namespace nopCommerceMobile.Models.Common
{
    public class CurrencyModel : BaseModel
    {
        public string Name { get; set; }

        public string CurrencySymbol { get; set; }
    }
}
