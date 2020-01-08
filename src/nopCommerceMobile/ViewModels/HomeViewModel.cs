using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion

        #region Ctor

        public HomeViewModel()
        {
            _catalogService = ViewModelLocator.Resolve<ICatalogService>();
        }

        #endregion

        private ObservableCollection<CategoryModel> _categories;
        public ObservableCollection<CategoryModel> Categories
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
            Categories = await _catalogService.GetHomeCategoriesAsync();

            IsBusy = false;
            IsDataLoaded = true;
        }
    }
}
