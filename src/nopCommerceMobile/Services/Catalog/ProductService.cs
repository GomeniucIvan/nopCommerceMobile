using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.RequestProvider;

namespace nopCommerceMobile.Services.Catalog
{
    public class ProductService : IProductService
    {
        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/product";

        private readonly IRequestProvider _requestProvider;

        public ProductService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public Task<ProductDetailsModel> GetProductByIdAsync(int productId)
        {
            var uri = $"{ApiUrlBase}/{productId}";

            return _requestProvider.GetAsync<ProductDetailsModel>(uri);
        }
    }
}
