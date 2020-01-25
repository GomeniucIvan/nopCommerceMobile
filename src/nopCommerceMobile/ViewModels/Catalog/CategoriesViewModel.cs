using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Catalog
{
    public class CategoriesViewModel : BaseViewModel
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion

        #region Ctor

        public CategoriesViewModel()
        {
            if (_catalogService == null)
                _catalogService = LocatorViewModel.Resolve<ICatalogService>();
        }

        #endregion

        private CategoryModel _category;
        public CategoryModel Category
        {
            get => _category;
            set
            {
                _category = value;
                RaisePropertyChanged(()=> Category);
            }
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            return await _catalogService.GetCategoryByIdAsync(categoryId);
        }
    }
}
