using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Models.News;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels
{
    public class HomeBaseViewModel : BaseViewModel
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion

        #region Ctor

        public HomeBaseViewModel()
        {
            if (_catalogService == null)
                _catalogService = LocatorViewModel.Resolve<ICatalogService>();
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

        private ObservableCollection<ProductModel> _products;
        public ObservableCollection<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                RaisePropertyChanged(() => Products);
            }
        }

        private ObservableCollection<ProductModel> _bestSellers;
        public ObservableCollection<ProductModel> BestSellers
        {
            get => _bestSellers;
            set
            {
                _bestSellers = value;
                RaisePropertyChanged(() => BestSellers);
            }
        }

        private ObservableCollection<NewsItemModel> _news;
        public ObservableCollection<NewsItemModel> News
        {
            get => _news;
            set
            {
                _news = value;
                RaisePropertyChanged(() => News);
            }
        }

        private ObservableCollection<SliderModel> _sliders;
        public ObservableCollection<SliderModel> Sliders
        {
            get => _sliders;
            set
            {
                _sliders = value;
                RaisePropertyChanged(() => Sliders);
            }
        }

        private ObservableCollection<ProductModel> _recentlyViewedProducts;
        public ObservableCollection<ProductModel> RecentlyViewedProducts
        {
            get => _recentlyViewedProducts;
            set
            {
                _recentlyViewedProducts = value;
                RaisePropertyChanged(() => RecentlyViewedProducts);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            Categories = await _catalogService.GetHomeCategoriesAsync();
            Products = await _catalogService.GetHomeProductsAsync();
            BestSellers = await _catalogService.GetHomeBestSellersAsync();
            News = await _catalogService.GetHomeNewsAsync();
            RecentlyViewedProducts = await _catalogService.GetRecentlyViewedProductsAsync();

            Sliders = new ObservableCollection<SliderModel>()
            {
                new SliderModel(){ Image = "http://sermonspiceuploads.s3.amazonaws.com/3265/fp_68126/colourbackground4hd_full.jpg"},
                new SliderModel(){ Image = "https://image.freepik.com/free-photo/colour-smoke-background_71163-196.jpg"},
                new SliderModel(){ Image = "https://image.freepik.com/foto-gratis/movimiento-humo-colores-humo-rojo-abstracto-fondo-negro_36326-2576.jpg"}
            };

            IsBusy = false;
            IsDataLoaded = true;
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(int categoryId)
        {
            return await _catalogService.GetCategoryByIdAsync(categoryId);
        }
    }
}
