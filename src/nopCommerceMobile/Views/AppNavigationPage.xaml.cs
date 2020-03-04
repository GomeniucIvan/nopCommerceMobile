using System;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Navigation;
using nopCommerceMobile.Views.Catalog;
using nopCommerceMobile.Views.Customer;
using nopCommerceMobile.Views.ShoppingCart;
using Xamarin.Forms;

//https://camo.githubusercontent.com/55ac2d5716d72391b2b125a6608cb081f2be6063/68747470733a2f2f666c75747465722e67736b696e6e65722e636f6d2f70726576696577732f6c69717569645f6e61765f6564697465645f736d2e6769663f
//TODO

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
            ViewModel.CategoryNavigationTabColor = ViewModel.SelectedNavigationPage == NavigationPageEnum.CategoryNavigation ? primaryColor : Color.Default;
            ViewModel.CartTabColor = ViewModel.SelectedNavigationPage == NavigationPageEnum.ShoppingCart ? primaryColor : Color.Default;
            ViewModel.WishListTabColor = ViewModel.SelectedNavigationPage == NavigationPageEnum.WishList ? primaryColor : Color.Default;

            if (ViewModel.SelectedNavigationPage == NavigationPageEnum.Home)
                PageContainer.Content = new HomeView();

            if (ViewModel.SelectedNavigationPage == NavigationPageEnum.Account)
                PageContainer.Content = new CustomerView();

            if (ViewModel.SelectedNavigationPage == NavigationPageEnum.CategoryNavigation)
                PageContainer.Content = new CategoryNavigationView();

            if (ViewModel.SelectedNavigationPage == NavigationPageEnum.ShoppingCart)
                PageContainer.Content = new CartView();

            if (ViewModel.SelectedNavigationPage == NavigationPageEnum.WishList)
                PageContainer.Content = new CartView();
        }

        private void HomePageTabTapped(object sender, EventArgs e)
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.Home;
        }

        private void AccountTabTapped(object sender, EventArgs e)
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.Account;
        }

        private void CatalogTab_Tapped(object sender, EventArgs e)
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.CategoryNavigation;
        }

        private void ShoppingCart_Tapped(object sender, EventArgs e)
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.ShoppingCart;
        }

        private void WishList_Tapped(object sender, EventArgs e)
        {
            ViewModel.SelectedNavigationPage = NavigationPageEnum.WishList;
        }
    }
}