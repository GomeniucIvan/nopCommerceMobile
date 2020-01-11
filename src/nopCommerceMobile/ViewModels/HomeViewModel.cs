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

        private bool _anyCategories;
        public bool AnyCategories
        {
            get => _anyCategories;
            set
            {
                _anyCategories = value;
                RaisePropertyChanged(() => AnyCategories);
            }
        }

        private bool _anyProducts;
        public bool AnyProducts
        {
            get => _anyProducts;
            set
            {
                _anyProducts = value;
                RaisePropertyChanged(() => AnyProducts);
            }
        }

        private bool _anyBestSellers;
        public bool AnyBestSellers
        {
            get => _anyBestSellers;
            set
            {
                _anyBestSellers = value;
                RaisePropertyChanged(() => AnyBestSellers);
            }
        }

        private bool _anyNews;
        public bool AnyNews
        {
            get => _anyNews;
            set
            {
                _anyNews = value;
                RaisePropertyChanged(() => AnyNews);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            Categories = await _catalogService.GetHomeCategoriesAsync();
            AnyCategories = Categories.Any();

            Products = await _catalogService.GetHomeProductsAsync();
            AnyProducts = Products.Any();

            BestSellers = await _catalogService.GetHomeBestSellersAsync();
            AnyBestSellers = BestSellers.Any();

            News = await _catalogService.GetHomeNewsAsync();
            AnyNews = News.Any();

            IsBusy = false;
            IsDataLoaded = true;
        }
    }
}
