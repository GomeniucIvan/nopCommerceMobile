using System;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Navigation;
using nopCommerceMobile.Views.Customer;
using Xamarin.Forms;

namespace nopCommerceMobile.Views
{
    public abstract class NavigationPageXaml : ModelBoundContentPage<AppNavigationBaseViewModel> { }
    public partial class AppNavigationPage : NavigationPageXaml
    {
        public static AppNavigationPage Page;
        public static AppNavigationBaseViewModel Vm;

        public AppNavigationPage()
        {
            InitializeComponent();
            Page = this;
            if (BindingContext == null)
                BindingContext = new AppNavigationBaseViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Vm = ViewModel;

            await ViewModel.InitializeAsync();

            if (!ViewModel.SelectedNavigationPage.HasValue)
                SetHomePage();
            else
                Page.SetPageContentPage();

        }

        private void SetHomePage()
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.Home;
        }

        public void SetPageContentPage()
        {
            var primaryColor = Color.FromHex("#1e5474");

            //set selected/unselected colors
            ViewModel.HomePageTabColor = ViewModel.SelectedNavigationPage == NavigationPageEnum.Home ? primaryColor : Color.Default;
            ViewModel.AccountTabColor = ViewModel.SelectedNavigationPage == NavigationPageEnum.Account ? primaryColor : Color.Default;

            if (ViewModel.SelectedNavigationPage == NavigationPageEnum.Home)
                PageContainer.Content = new HomeView();

            if (ViewModel.SelectedNavigationPage == NavigationPageEnum.Account)
                PageContainer.Content = new CustomerView();
        }

        private void HomePageTabTapped(object sender, EventArgs e)
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.Home;
        }

        private void AccountTabTapped(object sender, EventArgs e)
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.Account;
        }
    }
}