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
               var registerResult = await ViewModel.RegisterCustomer();

               if (!registerResult.IsSuccessStatusCode)
                   ViewModel.DisplayToastNotification(registerResult.ErrorMessage, NotificationTypeEnum.Warning);

               else
               {
                   App.CurrentCostumer.CustomerGuid = registerResult.Data;
                   await ViewModel.SetCurrentCustomer();

                   await Navigation.PopModalAsync();
               }
            }
        }
    }
}