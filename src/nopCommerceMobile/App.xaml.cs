using System.Collections.Generic;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Localization;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.Views;
using nopCommerceMobile.Views.Common;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace nopCommerceMobile
{
    //fix translator on first loading TODO
    //add default image(for CachedImage plugin) TODO
    //add web slider functionality TODO
    //implement pagination TODO
    //add web locale resources version (when value is updated or added update mobile db) TODO
    //in cs files use styles/variables from app.xaml TODO
    //change popup notification - stack layout to absolute layout, to be able to scroll behind content TODO

    public partial class App : Application
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ICustomerService _customerService;
        public static CustomerModel CurrentCostumer;
        public static IList<LocaleResourceModel> LocaleResources;
        public static CustomerSettingModel CurrentCostumerSettings;

        #endregion

        #region Ctor

        public App()
        {
            InitializeComponent();

            //check if device is connected to internet
            //https://docs.microsoft.com/en-us/xamarin/essentials/connectivity?context=xamarin%2Fxamarin-forms&tabs=ios
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                MainPage = new NoInternetPage();
                return;
            }

            if (_localizationService == null)
                _localizationService = LocatorViewModel.Resolve<ILocalizationService>();

            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();

            CurrentCostumerSettings = new CustomerSettingModel();

            _customerService.SetCurrentCustomer(true, true);
            _localizationService.CreateOrUpdateLocales();

            InitApp();
        }

        #endregion

        private void InitApp()
        {
            MainPage = GetMainPage();
        }

        public static void SetMainPage()
        {
            (Application.Current).MainPage = GetMainPage();
        }

        public static Page GetMainPage()
        {
            var appNavigationPage = new AppNavigationPage()
            {
                BindingContext = new AppNavigationBaseViewModel()
            };
            var page = new NavigationPage(appNavigationPage);
            return page;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
