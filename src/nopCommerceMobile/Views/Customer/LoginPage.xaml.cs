using System;
using System.IO;
using System.Linq;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Customer;
using SQLite;

namespace nopCommerceMobile.Views.Customer
{
    public abstract class LoginPageXaml : ModelBoundContentPage<LoginViewModel> { }
    public partial class LoginPage : LoginPageXaml
    {
        public LoginPage()
        {
            InitializeComponent();
            if (BindingContext == null)
                BindingContext = new LoginViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.InitializeAsync();
        }

        private void RememberMeLabel_OnTapped(object sender, EventArgs e)
        {
            ViewModel.LoginModel.RememberMe = !ViewModel.LoginModel.RememberMe;
        }

        private async void Login_OnClicked(object sender, EventArgs e)
        {
            //check if email or password is not null TODO
            if (!ViewModel.IsBusy)
            {
                ViewModel.IsBusy = true;
                var response = await ViewModel.LoginCustomer();

                //add popup with success message
                await Navigation.PopModalAsync();
            }
        }

        private async void Close_OnTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}