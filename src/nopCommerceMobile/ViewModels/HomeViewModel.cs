using System.Collections.ObjectModel;
using System.Linq;
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
                OnPropertyChanged(nameof(Categories));
            }
        }

        private ObservableCollection<ProductModel> _products;
        public ObservableCollection<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private bool _anyCategories;
        public bool AnyCategories
        {
            get => _anyCategories;
            set
            {
                _anyCategories = value;
                OnPropertyChanged(nameof(AnyCategories));
            }
        }

        private bool _anyProducts;
        public bool AnyProducts
        {
            get => _anyProducts;
            set
            {
                _anyProducts = value;
                OnPropertyChanged(nameof(AnyProducts));
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            Categories = await _catalogService.GetHomeCategoriesAsync();
            AnyCategories = Categories.Any();

            Products = await _catalogService.GetHomeProductsAsync();
            AnyProducts = Products.Any();

            IsBusy = false;
            IsDataLoaded = true;
        }
    }
}
