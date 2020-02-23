using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Models.News;
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

            var categories = await _requestProvider.GetAsync<List<CategoryModel>>(uri);

            if (categories != null)
                return categories.ToObservableCollection();

            else
                return new ObservableCollection<CategoryModel>();
        }

        public async Task<ObservableCollection<ProductModel>> GetHomeProductsAsync()
        {
            var uri = $"{HomeApiUrlBase}/products";

            var products = await _requestProvider.GetAsync<List<ProductModel>>(uri);

            if (products != null)
                return products.ToObservableCollection();

            else
                return new ObservableCollection<ProductModel>();
        }

        public async Task<ObservableCollection<ProductModel>> GetHomeBestSellersAsync()
        {
            var uri = $"{HomeApiUrlBase}/bestsellers";

            var bestSellers = await _requestProvider.GetAsync<List<ProductModel>>(uri);

            if (bestSellers != null)
                return bestSellers.ToObservableCollection();

            else
                return new ObservableCollection<ProductModel>();
        }

        public async Task<ObservableCollection<NewsItemModel>> GetHomeNewsAsync()
        {
            var uri = $"{HomeApiUrlBase}/news";

            var news = await _requestProvider.GetAsync<List<NewsItemModel>>(uri);

            if (news != null)
                return news.ToObservableCollection();

            else
                return new ObservableCollection<NewsItemModel>();
        }

        public Task<CategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            var uri = $"{ApiUrlBase}/categories/{categoryId}";

            return _requestProvider.GetAsync<CategoryModel>(uri);
        }

        public async Task<ObservableCollection<CategorySimpleModel>> PrepareCategoryNavigationModel(int currentCategoryId = 0, int currentProductId = 0)
        {
            var uri = $"{ApiUrlBase}/navigation?currentCategoryId={currentCategoryId}&currentProductId={currentProductId}";

            var categoryModel = await _requestProvider.GetAsync<CategoryNavigationModel>(uri);

            if (categoryModel.Categories != null)
                return categoryModel.Categories.ToObservableCollection();

            else
                return new ObservableCollection<CategorySimpleModel>();
        }
    }
}
