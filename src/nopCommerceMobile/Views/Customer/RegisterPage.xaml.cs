using System;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Customer;

namespace nopCommerceMobile.Views.Customer
{
    public abstract class RegisterPageXaml : ModelBoundContentPage<RegisterViewModel>{ }
    public partial class RegisterPage : RegisterPageXaml
    {
        public RegisterPage()
        {
            InitializeComponent();

            if (BindingContext == null)
                BindingContext = new RegisterViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.InitializeAsync();
        }

        private async void Close_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void NewsletterLabel_OnTapped(object sender, EventArgs e)
        {
        }

        private async void Register_OnClicked(object sender, EventArgs e)
        {
            if (!ViewModel.IsBusy)
            {
                ViewModel.IsBusy = true;
                await ViewModel.RegisterCustomer();
                await CustomerView.View.InitializeAsync();

                //add popup with success message
                await Navigation.PopModalAsync();
            }
        }
    }
}