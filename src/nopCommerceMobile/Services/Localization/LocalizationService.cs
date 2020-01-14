using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.RequestProvider;

namespace nopCommerceMobile.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/localization";

        private readonly IRequestProvider _requestProvider;

        public LocalizationService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<IList<LocaleResourceModel>> GetLocaleResourcesByLanguageCultureAsync(string languageCulture)
        {
            var uri = $"{ApiUrlBase}/{languageCulture}";

           var localeResources = await _requestProvider.GetAsync<List<LocaleResourceModel>>(uri);

            if (localeResources != null && localeResources.Any())
                return localeResources;

            return new List<LocaleResourceModel>();
        }
    }
}
