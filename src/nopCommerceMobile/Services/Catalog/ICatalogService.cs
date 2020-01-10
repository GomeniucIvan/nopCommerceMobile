using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;

namespace nopCommerceMobile.Services.Catalog
{
    public interface ICatalogService
    {
        Task<ObservableCollection<CategoryModel>> GetHomeCategoriesAsync();
        Task<ObservableCollection<ProductModel>> GetHomeProductsAsync();
    }
}
