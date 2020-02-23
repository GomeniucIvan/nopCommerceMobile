using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Catalog
{
    public class CategoryNavigationViewModel : BaseViewModel 
    {

        #region Fields

        private ICatalogService _catalogService;

        #endregion

        #region Ctor

        public CategoryNavigationViewModel()
        {
            if (_catalogService == null)
                _catalogService = LocatorViewModel.Resolve<ICatalogService>();
        }

        #endregion

        private ObservableCollection<CategorySimpleModel> _categories;
        public ObservableCollection<CategorySimpleModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                RaisePropertyChanged(() => Categories);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            Categories = await _catalogService.PrepareCategoryNavigationModel();

            IsBusy = false;
            IsDataLoaded = true;
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            return await _catalogService.GetCategoryByIdAsync(categoryId);
        }
    }
}
