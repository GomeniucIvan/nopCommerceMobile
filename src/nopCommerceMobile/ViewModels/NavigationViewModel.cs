using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Localization;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Navigation;
using nopCommerceMobile.Views;
using Xamarin.Forms;

namespace nopCommerceMobile.ViewModels
{
    public class AppNavigationBaseViewModel : BaseViewModel
    {
        private ICustomerService _customerService;
        private ILocalizationService _localizationService;

        public AppNavigationBaseViewModel()
        {
            if (_customerService == null && App.CurrentCostumer == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();

            if (_localizationService == null)
                _localizationService = LocatorViewModel.Resolve<ILocalizationService>();
        }

        private bool _isRegisteredCustomer;
        public bool IsRegisteredCustomer
        {
            get => _isRegisteredCustomer;
            set
            {
                _isRegisteredCustomer = value;
                RaisePropertyChanged(() => IsRegisteredCustomer);
            }
        }

        private Color _homePageTabColor;
        public Color HomePageTabColor
        {
            get => _homePageTabColor;
            set
            {
                _homePageTabColor = value;
                RaisePropertyChanged(() => HomePageTabColor);
            }
        }

        private Color _accountTabColor;
        public Color AccountTabColor
        {
            get => _accountTabColor;
            set
            {
                _accountTabColor = value;
                RaisePropertyChanged(() => AccountTabColor);
            }
        }

        private NavigationPageEnum? _selectedNavigationPage;
        public NavigationPageEnum? SelectedNavigationPage
        {
            get => _selectedNavigationPage;
            set
            {
                _selectedNavigationPage = value;
                RaisePropertyChanged(() => SelectedNavigationPage);

                AppNavigationPage.Page.SetPageContentPage();
            }
        }

        private int _cartCount;
        public int CartCount
        {
            get => _cartCount;
            set
            {
                _cartCount = value;
                RaisePropertyChanged(()=> CartCount);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            if (!App.AppInitialized)
            {
                await InitializeDataBase();
                App.AppInitialized = true;
            }

            IsRegisteredCustomer = App.CurrentCostumer.IsRegistered();
            CartCount = App.CurrentCostumer == null ? 0 : App.CurrentCostumer.ShoppingCartItems.Where(v => v.ShoppingCartTypeId == 1).Sum(v => v.Quantity);

            IsBusy = false;
        }

        private async Task InitializeDataBase()
        {
            await _customerService.SetCurrentCustomer(true);
            await _localizationService.CreateOrUpdateLocales();
        }
    }
}
