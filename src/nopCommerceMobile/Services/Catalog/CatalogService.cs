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
        private static readonly string HomeApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/home";

        private readonly IRequestProvider _requestProvider;

        public CatalogService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<ObservableCollection<CategoryModel>> GetHomeCategoriesAsync()
        {
            var uri = $"{HomeApiUrlBase}/categories";

            List<CategoryModel> categories = await _requestProvider.GetAsync<List<CategoryModel>>(uri);

            if (categories != null)
                return categories.ToObservableCollection();

            else
                return new ObservableCollection<CategoryModel>();
        }

        public async Task<ObservableCollection<ProductModel>> GetHomeProductsAsync()
        {
            var uri = $"{HomeApiUrlBase}/products";

            List<ProductModel> products = await _requestProvider.GetAsync<List<ProductModel>>(uri);

            if (products != null)
                return products.ToObservableCollection();

            else
                return new ObservableCollection<ProductModel>();
        }
    }
}
