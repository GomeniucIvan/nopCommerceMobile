using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.RequestProvider;

namespace nopCommerceMobile.Services.Catalog
{
    public class CatalogService : ICatalogService
    {
        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/catalog";

        private readonly IRequestProvider _requestProvider;

        public CatalogService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<CategoryModel>> GetHomeCategoriesAsync()
        {
            var uri = $"{ApiUrlBase}/homecategories";

            List<CategoryModel> categories = await _requestProvider.GetAsync<List<CategoryModel>>(uri);

            if (categories != null)
                return categories.ToObservableCollection();

            else
                return new ObservableCollection<CategoryModel>();
        }
    }
}
