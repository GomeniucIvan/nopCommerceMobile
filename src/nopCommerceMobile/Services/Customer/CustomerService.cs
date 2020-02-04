using System;
using System.IO;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Orders;
using nopCommerceMobile.Services.RequestProvider;
using SQLite;

namespace nopCommerceMobile.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        #region Fields

        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/customer";
        private readonly IRequestProvider _requestProvider;

        private static string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nopCommerce.db");
        private SQLiteAsyncConnection database = new SQLiteAsyncConnection(databasePath);

        #endregion

        public CustomerService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<LoginModel> GetLoginModelAsync()
        {
            var uri = $"{ApiUrlBase}/login";

            var loginModel = await _requestProvider.GetAsync<LoginModel>(uri);

            if (loginModel != null)
                return loginModel;

            return new LoginModel();
        }

        public async Task<CustomerModel> GetCurrentCustomerModelAsync()
        {
            var uri = $"{ApiUrlBase}/currentcustomer";

            var customerModel = await _requestProvider.GetAsync<CustomerModel>(uri);

            if (customerModel != null)
                return customerModel;

            return new CustomerModel();
        }

        public async Task<RegisterModel> GetRegisterModelAsync()
        {
            var uri = $"{ApiUrlBase}/register";

            var registerModel = await _requestProvider.GetAsync<RegisterModel>(uri);

            if (registerModel != null)
                return registerModel;

            return new RegisterModel();
        }

        public async Task<CustomerModel> LoginAsync(LoginModel model)
        {
            var uri = $"{ApiUrlBase}/login";

            var result = await _requestProvider.PostAsync<CustomerModel,LoginModel>(uri, model);
            await SetCurrentCustomer(true);
            return result;
        }

        public async void LogoutCustomer()
        {
            var uri = $"{ApiUrlBase}/logout";
           await _requestProvider.PostAsync<CustomerModel>(uri, App.CurrentCostumer);

            await DeleteCurrentCustomer();
            await SetCurrentCustomer(true);
        }

        public async Task SetCurrentCustomer(bool refreshData = false)
        {
            await CreateOrUpdateCustomer(refreshData);
            await CreateOrUpdateCustomerSettings();
            await CreateOrUpdateCustomerRoles();
            await CreateOrUpdateShoppingCartItems();
        }

        private async Task CreateOrUpdateCustomer(bool refreshData = false)
        {
            if (refreshData)
            {
                App.CurrentCostumer = await GetCurrentCustomerModelAsync();
            }

            var customerTable = await database.GetTableInfoAsync(nameof(Models.Customer.Customer));
            if (customerTable.Count == 0)
            {
                await database.CreateTableAsync<Models.Customer.Customer>();
            }
            var customer = await database.Table<Models.Customer.Customer>().CountAsync();
            if (customer == 0)
            {
                await database.InsertAsync(new Models.Customer.Customer()
                {
                    CustomerGuid = App.CurrentCostumer.CustomerGuid,
                    Email = App.CurrentCostumer.Email,
                    FirstName = App.CurrentCostumer.FirstName,
                    LastName = App.CurrentCostumer.LastName,
                });
            }
            else
            {
                await database.UpdateAsync(new Models.Customer.Customer()
                {
                    CustomerGuid = App.CurrentCostumer.CustomerGuid,
                    Email = App.CurrentCostumer.Email,
                    FirstName = App.CurrentCostumer.FirstName,
                    LastName = App.CurrentCostumer.LastName
                });
            }
        }

        public async Task CreateOrUpdateCustomerSettings(bool updateTable = false)
        {
            var customerSettingsTable = await database.GetTableInfoAsync(nameof(CustomerSettings));
            if (customerSettingsTable.Count == 0)
            {
                await database.CreateTableAsync<CustomerSettings>();
            }
            var customerSettings = await database.Table<CustomerSettings>().CountAsync();
            if (customerSettings == 0)
            {
                await database.InsertAsync(new CustomerSettings()
                {
                    CurrentLanguage = "en-US",
                    ViewMode = "grid"
                });
            }
            else
            {
                var dbCustomerSettings = await database.Table<CustomerSettings>().FirstOrDefaultAsync();
                App.CurrentCostumer.ViewMode = dbCustomerSettings.ViewMode;
                App.CurrentCostumer.CurrentLanguage = dbCustomerSettings.CurrentLanguage;
            }

            if (updateTable)
            {
                await database.UpdateAsync(new CustomerSettings()
                {
                    CurrentLanguage = App.CurrentCostumer.CurrentLanguage,
                    ViewMode = App.CurrentCostumer.ViewMode,
                });
            }

        }

        private async Task CreateOrUpdateCustomerRoles()
        {
            var customerRoleTable = await database.GetTableInfoAsync(nameof(CustomerRole));
            if (customerRoleTable.Count == 0)
            {
                await database.CreateTableAsync<CustomerRole>();
            }
            else
            {
                await database.DeleteAllAsync<CustomerRole>();
            }

            foreach (var currentCustomerRole in App.CurrentCostumer.CustomerRoles)
            {
                await database.InsertAsync(new CustomerRole()
                {
                    Name = currentCustomerRole.Name,
                    SystemName = currentCustomerRole.SystemName,
                    Active = currentCustomerRole.Active
                });
            }
        }

        public async Task CreateOrUpdateShoppingCartItems()
        {
            var shoppingCartItemTable = await database.GetTableInfoAsync(nameof(ShoppingCartItem));
            if (shoppingCartItemTable.Count == 0)
            {
                await database.CreateTableAsync<ShoppingCartItem>();
            }
            else
            {
                await database.DeleteAllAsync<ShoppingCartItem>();
            }
            foreach (var cartItem in App.CurrentCostumer.ShoppingCartItems)
            {
                await database.InsertAsync(new ShoppingCartItem()
                {
                    ProductId = cartItem.ProductId,
                    ShoppingCartTypeId = cartItem.ShoppingCartTypeId,
                    Quantity = cartItem.Quantity,
                    ShoppingCartItemId = Guid.NewGuid()
                });
            }
        }

        public async Task RegisterAsync(RegisterModel model)
        {
            var uri = $"{ApiUrlBase}/register";

            await _requestProvider.PostAsync<RegisterModel>(uri, model);
            await SetCurrentCustomer(true);
        }

        private async Task DeleteCurrentCustomer()
        {
            await database.DeleteAllAsync<Models.Customer.Customer>();
            await database.DeleteAllAsync<CustomerRole>();
            await database.DeleteAllAsync<ShoppingCartItem>();
        }
    }
}
