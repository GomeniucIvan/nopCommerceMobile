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

                App.CurrentCostumer = response;

                //make interface for database TODO
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nopCommerce.db");
                var database = new SQLiteAsyncConnection(databasePath);

                //customer table
                await database.UpdateAsync(new Models.Customer.Customer()
                {
                    CustomerGuid = App.CurrentCostumer.CustomerGuid,
                    Email = App.CurrentCostumer.Email,
                    CustomerRoles = App.CurrentCostumer.CustomerRoles,
                    FirstName = App.CurrentCostumer.FirstName,
                    LastName = App.CurrentCostumer.LastName,
                });

                //customer role table
                await database.DeleteAllAsync<CustomerRole>();
                foreach (var currentCustomerRole in App.CurrentCostumer.CustomerRoles)
                {
                    await database.InsertAsync(new CustomerRole()
                    {
                        Name = currentCustomerRole.Name,
                        SystemName = currentCustomerRole.SystemName,
                        Active = currentCustomerRole.Active
                    });
                }

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