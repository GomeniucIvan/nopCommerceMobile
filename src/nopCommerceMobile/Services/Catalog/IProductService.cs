using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;

namespace nopCommerceMobile.Services.Catalog
{
    public interface IProductService
    {
        Task<ProductDetailsModel> GetProductByIdAsync(int productId);
    }
}
