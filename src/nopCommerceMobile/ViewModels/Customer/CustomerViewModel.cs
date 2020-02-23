using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Models.Common;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Customer
{
    public class CustomerViewModel : BaseViewModel
    {
        #region Fields

        private ICustomerService _customerService;
        private ICatalogService _catalogService;

        #endregion

        #region Ctor

        public CustomerViewModel()
        {
            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();

            if (_catalogService == null)
                _catalogService = LocatorViewModel.Resolve<ICustomerService>();
        }

        #endregion

        private CustomerModel _customerModel;
        public CustomerModel CustomerModel
        {
            get => _customerModel;
            set
            {
                _customerModel = value;
                RaisePropertyChanged(() => CustomerModel);
            }
        }

        private ObservableCollection<LanguageModel> _languages;
        public ObservableCollection<LanguageModel> Languages
        {
            get => _languages;
            set
            {
                _languages = value;
                RaisePropertyChanged(() => Languages);
            }
        }

        private ObservableCollection<CurrencyModel> _currencies;
        public ObservableCollection<CurrencyModel> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                RaisePropertyChanged(() => Currencies);
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

        private bool _isRegistered;
        public bool IsRegistered
        {
            get => _isRegistered;
            set
            {
                _isRegistered = value;
                RaisePropertyChanged(()=> IsRegistered);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            CustomerModel = App.CurrentCostumer;
            IsRegistered = CustomerModel.IsRegistered();

            Languages = await _customerService.GetLanguagesAsync();
            Currencies = await _customerService.GetCurrenciesAsync();
            RecentlyViewedProducts = await _catalogService.GetRecentlyViewedProductsAsync();

            IsBusy = false;
            IsDataLoaded = true;
        }
    }
}
