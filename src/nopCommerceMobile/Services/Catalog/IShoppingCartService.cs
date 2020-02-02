using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;

namespace nopCommerceMobile.Services.Catalog
{
    public interface IShoppingCartService
    {
        Task AddProductToCartAsync(ProductModel model, int quantity = 1, ShoppingCartType productType = ShoppingCartType.ShoppingCart);
    }
}
