using System;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Customer;

namespace nopCommerceMobile.Views.Customer
{
    public abstract class CustomerViewXaml : ModelBoundContentView<CustomerViewModel> { }
    public partial class CustomerView : CustomerViewXaml
    {
        public CustomerView()
        {
            InitializeComponent();

            if (BindingContext == null)
                BindingContext = new CustomerViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            await ViewModel.InitializeAsync();
        }

        private void Login_OnClick(object sender, EventArgs e)
        {
        }

        private void Register_OnClick(object sender, EventArgs e)
        {

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
    }
}