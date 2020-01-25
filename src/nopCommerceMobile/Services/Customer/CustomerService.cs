using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
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

        public async Task<RegisterModel> GetRegisterModelAsync()
        {
            var uri = $"{ApiUrlBase}/register";

            var registerModel = await _requestProvider.GetAsync<RegisterModel>(uri);

            if (registerModel != null)
                return registerModel;

            return new RegisterModel();
        }

        public async Task<CustomerModel> GetCurrentCustomerModelAsync()
        {
            var uri = $"{ApiUrlBase}/currentcustomer";

            var customerModel = await _requestProvider.GetAsync<CustomerModel>(uri);

            if (customerModel != null)
                return customerModel;

            return new CustomerModel();
        }

        public async Task<CustomerModel> LoginAsync(LoginModel model)
        {
            var uri = $"{ApiUrlBase}/login";

            return await _requestProvider.PostAsync<CustomerModel,LoginModel>(uri, model);
        }

        public void LogoutCustomer()
        {
            var uri = $"{ApiUrlBase}/logout";
            _requestProvider.PostAsync<CustomerModel>(uri, App.CurrentCostumer);

            DeleteCurrentCustomer();
            SetCurrentCustomer();
        }

        public async void SetCurrentCustomer()
        {
            //customer table
            var customerTable = await database.GetTableInfoAsync(nameof(Models.Customer.Customer));
            if (customerTable.Count == 0)
            {
                await database.CreateTableAsync<Models.Customer.Customer>();
            }
            var customer = await database.Table<Models.Customer.Customer>().CountAsync();
            if (customer == 0)
            {
                App.CurrentCostumer = await GetCurrentCustomerModelAsync();
                await database.InsertAsync(new Models.Customer.Customer()
                {
                    CustomerGuid = App.CurrentCostumer.CustomerGuid,
                    Email = App.CurrentCostumer.Email,
                    FirstName = App.CurrentCostumer.FirstName,
                    LastName = App.CurrentCostumer.LastName
                });
            }
            else
            {
                var dbCustomer = await database.Table<Models.Customer.Customer>().FirstOrDefaultAsync();
                App.CurrentCostumer = new CustomerModel()
                {
                    CustomerGuid = dbCustomer.CustomerGuid,
                    Email = dbCustomer.Email,
                    FirstName = dbCustomer.FirstName,
                    LastName = dbCustomer.LastName
                };
            }

            //customer role table
            var customerRoleTable = await database.GetTableInfoAsync(nameof(CustomerRole));
            if (customerRoleTable.Count == 0)
            {
                await database.CreateTableAsync<CustomerRole>();
            }
            var customerRole = await database.Table<CustomerRole>().CountAsync();
            if (customerRole == 0)
            {
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
            else
            {
                var dbCustomerRoles = await database.Table<CustomerRole>().ToListAsync();
                App.CurrentCostumer.CustomerRoles = dbCustomerRoles.Select(v => new CustomerRoleModel()
                {
                    Name = v.Name,
                    SystemName = v.SystemName,
                    Active = v.Active
                }).ToList();
            }
        }

        private async void DeleteCurrentCustomer()
        {
            await database.DeleteAllAsync<Models.Customer.Customer>();
            await database.DeleteAllAsync<CustomerRole>();
        }
    }
}
