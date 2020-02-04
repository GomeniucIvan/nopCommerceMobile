using System;
using System.IO;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Customer;
using SQLite;

namespace nopCommerceMobile.Views.Customer
{
    public abstract class CustomerViewXaml : ModelBoundContentView<CustomerViewModel> { }
    public partial class CustomerView : CustomerViewXaml
    {
        #region Fields

        private ICustomerService _customerService;
        public static CustomerView View;

        #endregion

        #region Ctor

        public CustomerView()
        {
            InitializeComponent();
            View = this;

            if (BindingContext == null)
                BindingContext = new CustomerViewModel();

            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();
        }

        #endregion

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            await ViewModel.InitializeAsync();
        }

        private void Login_OnClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage(new LoginPage()));
        }

        private void Register_OnClick(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Xamarin.Forms.NavigationPage(new RegisterPage()));
        }

        private void MyAccount_OnTapped(object sender, EventArgs e)
        {
        }

        private void Orders_OnTapped(object sender, EventArgs e)
        {
        }

        private void Addresses_OnTapped(object sender, EventArgs e)
        {
        }

        private void ShoppingCart_OnTapped(object sender, EventArgs e)
        {
        }

        private void Wishlist_OnTapped(object sender, EventArgs e)
        {
        }

        private void News_OnTapped(object sender, EventArgs e)
        {
        }

        private void Blog_OnTapped(object sender, EventArgs e)
        {
        }

        private void RecentlyViewedProducts_OnTapped(object sender, EventArgs e)
        {
        }

        private async void Logout_OnTapped(object sender, EventArgs e)
        {
            //add popup yes/no TODO
            _customerService.LogoutCustomer();

            await ViewModel.InitializeAsync();
        }

        public async Task InitializeAsync()
        {
            await ViewModel.InitializeAsync();
        }
    }
}