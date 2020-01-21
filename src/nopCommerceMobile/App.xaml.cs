using System.Collections.Generic;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using Xamarin.Forms;
using NavigationPage = nopCommerceMobile.Views.NavigationPage;

namespace nopCommerceMobile
{
    public partial class App : Application
    {
        #region Fields

        private ICustomerService _customerService;
        public static CustomerModel CurrentCostumer;
        public static IList<LocaleResourceModel> LocaleResources;
        public static string CustomerAppCulture { get; set; }

        #endregion

        #region Ctor

        public App()
        {
            InitializeComponent();

            if (_customerService == null && CurrentCostumer == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();

            InitApp();
        }

        #endregion

        private void InitApp()
        {
            MainPage = GetMainPage();
            GetCurrentCustomer();
        }

        public async void GetCurrentCustomer()
        {
            if (CurrentCostumer == null || CurrentCostumer.Id == 0)
                CurrentCostumer = await _customerService.GetCurrentCustomerModelAsync();
        }

        public static Page GetMainPage()
        {
            var page = new NavigationPage() { BindingContext = new NavigationBaseViewModel() };
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
