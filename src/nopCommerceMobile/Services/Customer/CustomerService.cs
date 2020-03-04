using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Common;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Orders;
using nopCommerceMobile.Models.ShoppingCart;
using nopCommerceMobile.Services.RequestProvider;
using SQLite;

namespace nopCommerceMobile.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        #region Fields

        private readonly IRequestProvider _requestProvider;

        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/customer";
        private static string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nopCommerce.db");
        private SQLiteAsyncConnection database = new SQLiteAsyncConnection(databasePath);

        #endregion

        public CustomerService(
            IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<GenericModel<LoginModel>> GetLoginModelAsync()
        {
            var uri = $"{ApiUrlBase}/login";

            return await _requestProvider.GetAsync<GenericModel<LoginModel>>(uri);
        }

        private async Task<CustomerModel> GetCurrentCustomerModelAsync()
        {
            var uri = $"{ApiUrlBase}/currentcustomer";
            return await _requestProvider.GetAsync<CustomerModel>(uri);
        }

        public async Task<RegisterModel> GetRegisterModelAsync()
        {
            var uri = $"{ApiUrlBase}/register";

            var registerModel = await _requestProvider.GetAsync<RegisterModel>(uri);

            if (registerModel != null)
                return registerModel;

            return new RegisterModel();
        }

        public async Task<GenericModel<Guid>> LoginAsync(LoginModel model)
        {
            var uri = $"{ApiUrlBase}/login";
            return await _requestProvider.PostAsync<GenericModel<Guid>,LoginModel>(uri, model);
        }

        public async void LogoutCustomer()
        {
            var uri = $"{ApiUrlBase}/logout";

            await DeleteCurrentCustomer();
            App.CurrentCostumer = new CustomerModel();
            App.CurrentCostumerSettings = new CustomerSettingModel();
            SetCurrentCustomer(true);

            await _requestProvider.PostAsync<CustomerModel>(uri, App.CurrentCostumer);
        }

        public void SetCurrentCustomer(bool refreshData = false, bool firstInit = false)
        {
            //add all required settings than edit todo
            Task responseTask = Task.Run(async () => {
                var customerSettingsTable = await database.GetTableInfoAsync(nameof(CustomerSettings));

                if (firstInit && customerSettingsTable.Count > 0)
                    await CreateOrUpdateCustomerSettings();

                await CreateOrUpdateCustomer(refreshData);

                if (!firstInit || customerSettingsTable.Count == 0)
                    await CreateOrUpdateCustomerSettings();

                await CreateOrUpdateCustomerRoles();
                await CreateOrUpdateShoppingCartItems();
            });
            responseTask.Wait();
        }

        private async Task CreateOrUpdateCustomer(bool refreshData = false)
        {
            var customerTable = await database.GetTableInfoAsync(nameof(Models.Customer.Customer));
            if (customerTable.Count == 0)
            {
                await database.CreateTableAsync<Models.Customer.Customer>();
            }
            var customer = await database.Table<Models.Customer.Customer>().CountAsync();
            if (customer == 0)
            {
                if (refreshData)
                    App.CurrentCostumer = await GetCurrentCustomerModelAsync();

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
                if (refreshData)
                    App.CurrentCostumer = await GetCurrentCustomerModelAsync();

                await database.UpdateAsync(new Models.Customer.Customer()
                {
                    CustomerGuid = App.CurrentCostumer.CustomerGuid,
                    Email = App.CurrentCostumer.Email,
                    FirstName = App.CurrentCostumer.FirstName,
                    LastName = App.CurrentCostumer.LastName
                });
            }
        }

        private async Task<string> CreateToken()
        {
            var uri = $"{ApiUrlBase}/token";

            var tokenFilter = new GenerateTokenFilter()
            {
                CustomerGuid = App.CurrentCostumer.CustomerGuid,
                LanguageId = App.CurrentCostumerSettings.LanguageId,
                CurrencyId = App.CurrentCostumerSettings.CurrencyId,
            };

            var tokenGenericModel = await _requestProvider.PostAsyncAnonymous<GenericModel<string>, GenerateTokenFilter>(uri, tokenFilter);

            if (!tokenGenericModel.IsSuccessStatusCode)
                return tokenGenericModel.ErrorMessage;

            return tokenGenericModel.Data;
        }

        public async Task CreateOrUpdateCustomerSettings(bool updateTable = false, bool generateNewToken = false)
        {
            if (generateNewToken)
                App.CurrentCostumerSettings.Token = await CreateToken();

            if (updateTable)
            {
                await database.UpdateAsync(new CustomerSettings()
                {
                    LanguageId = App.CurrentCostumerSettings.LanguageId,
                    CurrencyId = App.CurrentCostumerSettings.CurrencyId,
                    ViewMode = App.CurrentCostumerSettings.ViewMode,
                    Token = App.CurrentCostumerSettings.Token
                });
            }
            else
            {
                var customerSettingsTable = await database.GetTableInfoAsync(nameof(CustomerSettings));
                if (customerSettingsTable.Count == 0)
                {
                    await database.CreateTableAsync<CustomerSettings>();
                }
                var customerSettings = await database.Table<CustomerSettings>().CountAsync();
                if (customerSettings == 0)
                {
                    var token = CreateToken().GetAwaiter().GetResult();
                    await database.InsertAsync(new CustomerSettings()
                    {
                        LanguageId = GlobalSettings.DefaultLanguageId,
                        CurrencyId = GlobalSettings.DefaultCurrencyId,
                        ViewMode = "grid",
                        Token = token
                    });
                    App.CurrentCostumerSettings.LanguageId = GlobalSettings.DefaultLanguageId;
                    App.CurrentCostumerSettings.CurrencyId = GlobalSettings.DefaultCurrencyId;
                    App.CurrentCostumerSettings.ViewMode = "grid";
                    App.CurrentCostumerSettings.Token = token;
                }
                else
                {
                    var dbCustomerSettings = await database.Table<CustomerSettings>().FirstOrDefaultAsync();
                    App.CurrentCostumerSettings.ViewMode = dbCustomerSettings.ViewMode;
                    App.CurrentCostumerSettings.LanguageId = dbCustomerSettings.LanguageId;
                    App.CurrentCostumerSettings.CurrencyId = dbCustomerSettings.CurrencyId;
                    App.CurrentCostumerSettings.Token = dbCustomerSettings.Token;
                }
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

        public async Task<GenericModel<Guid>> RegisterAsync(RegisterModel model)
        {
            var uri = $"{ApiUrlBase}/register";

            return await _requestProvider.PostAsync<GenericModel<Guid>, RegisterModel>(uri, model);
        }

        public async Task<ObservableCollection<LanguageModel>> GetLanguagesAsync()
        {
            var uri = $"{ApiUrlBase}/languages";

            var languages = await _requestProvider.GetAsync<List<LanguageModel>>(uri);

            if (languages != null)
                return languages.ToObservableCollection();

            else
                return new ObservableCollection<LanguageModel>();
        }

        public async Task<ObservableCollection<CurrencyModel>> GetCurrenciesAsync()
        {
            var uri = $"{ApiUrlBase}/currencies";

            var languages = await _requestProvider.GetAsync<List<CurrencyModel>>(uri);

            if (languages != null)
                return languages.ToObservableCollection();

            else
                return new ObservableCollection<CurrencyModel>();
        }

        public async Task<ShoppingCartModel> GetCartAsync()
        {
            var uri = $"{ApiUrlBase}/cart";

            return await _requestProvider.GetAsync<ShoppingCartModel>(uri);
        }

        private async Task DeleteCurrentCustomer()
        {
            await database.DropTableAsync<Models.Customer.Customer>();
            await database.DropTableAsync<CustomerRole>();
            await database.DropTableAsync<ShoppingCartItem>();
            await database.DropTableAsync<CustomerSettings>();
        }

        public async Task<WishlistModel> GetWishListAsync()
        {
            var uri = $"{ApiUrlBase}/wishlist";

            return await _requestProvider.GetAsync<WishlistModel>(uri);
        }
    }
}
