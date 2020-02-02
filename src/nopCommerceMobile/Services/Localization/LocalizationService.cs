using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.RequestProvider;
using SQLite;

namespace nopCommerceMobile.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        #region Fields

        private static readonly string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nopCommerce.db");
        private static readonly SQLiteAsyncConnection database = new SQLiteAsyncConnection(databasePath);
        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/localization";
        private readonly IRequestProvider _requestProvider;

        #endregion

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

        public async Task CreateOrUpdateLocales()
        {
            var localeResourceTable = await database.GetTableInfoAsync(nameof(LocaleResource));
            if (localeResourceTable.Count == 0)
            {
                await database.CreateTableAsync<LocaleResource>();
            }
            var anyLocaleResource = await database.Table<LocaleResource>().CountAsync();
            if (anyLocaleResource == 0)
            {
                App.LocaleResources = await GetLocaleResourcesByLanguageCultureAsync("en-US");
                foreach (var localeResource in App.LocaleResources)
                {
                    await database.InsertAsync(new LocaleResource()
                    {
                        LanguageId = localeResource.LanguageId,
                        ResourceName = localeResource.ResourceName,
                        ResourceValue = localeResource.ResourceValue
                    });
                }
            }
            else
            {
                var dbLocaleResources = await database.Table<LocaleResource>().ToListAsync().ConfigureAwait(true);
                App.LocaleResources = dbLocaleResources.Select(v => new LocaleResourceModel()
                {
                    LanguageId = v.LanguageId,
                    ResourceName = v.ResourceName,
                    ResourceValue = v.ResourceValue
                }).ToList();
            }
        }
    }
}
