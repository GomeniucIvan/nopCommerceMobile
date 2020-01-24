using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Localization;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using SQLite;
using Xamarin.Forms;
using NavigationPage = nopCommerceMobile.Views.NavigationPage;

namespace nopCommerceMobile
{
    public partial class App : Application
    {

        #region Fields

        private ICustomerService _customerService;
        private ILocalizationService _localizationService;
        public static CustomerModel CurrentCostumer;
        public static IList<LocaleResourceModel> LocaleResources;
        public static string CustomerAppCulture { get; set; }

        #endregion

        #region Ctor

        public App()
        {
            InitializeComponent();

            if (_customerService == null && CurrentCostumer == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();

            if (_localizationService == null)
                _localizationService = LocatorViewModel.Resolve<ILocalizationService>();

            InitApp();
        }

        #endregion

        private void InitApp()
        {
            InitializeDataBase(); //fix locale resources on first load, initialize database from service TODO
            MainPage = GetMainPage();
        }

        private async void InitializeDataBase()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nopCommerce.db");
            var database = new SQLiteAsyncConnection(databasePath);

            //customer table
            var customerTable = await database.GetTableInfoAsync(nameof(Customer));
            if (customerTable.Count == 0)
            {
                await database.CreateTableAsync<Customer>();
            }
            var customer = await database.Table<Customer>().CountAsync();
            if (customer == 0)
            {
                CurrentCostumer = await _customerService.GetCurrentCustomerModelAsync();
                await database.InsertAsync(new Customer()
                {
                    CustomerGuid = CurrentCostumer.CustomerGuid,
                    Email = CurrentCostumer.Email,
                    CustomerRoles = CurrentCostumer.CustomerRoles,
                    FirstName = CurrentCostumer.FirstName,
                    LastName = CurrentCostumer.LastName
                });
            }
            else
            {
                var dbCustomer = await database.Table<Customer>().FirstOrDefaultAsync();
                CurrentCostumer = new CustomerModel()
                {
                    CustomerGuid = dbCustomer.CustomerGuid,
                    Email = dbCustomer.Email,
                    CustomerRoles = dbCustomer.CustomerRoles
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
                foreach (var currentCustomerRole in CurrentCostumer.CustomerRoles)
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
                CurrentCostumer.CustomerRoles = dbCustomerRoles.Select(v => new CustomerRoleModel()
                {
                    Name = v.Name,
                    SystemName = v.SystemName
                }).ToList();
            }

            //locale resource table
            var localeResourceTable = await database.GetTableInfoAsync(nameof(LocaleResource));
            if (localeResourceTable.Count == 0)
            {
               await database.CreateTableAsync<LocaleResource>();
            }
            var anyLocaleResource = await database.Table<LocaleResource>().CountAsync();
            if (anyLocaleResource == 0)
            {
                LocaleResources = await _localizationService.GetLocaleResourcesByLanguageCultureAsync("en-US");
                foreach (var localeResource in LocaleResources)
                {
                   await database.InsertAsync(new LocaleResource()
                    {
                        LanguageId = localeResource.LanguageId,
                        ResourceName = localeResource.ResourceName,
                        ResourceValue = localeResource.ResourceValue
                    });
                }
            }
            else
            {
                var dbLocaleResources = await database.Table<LocaleResource>().ToListAsync();
                LocaleResources = dbLocaleResources.Select(v => new LocaleResourceModel()
                {
                    LanguageId = v.LanguageId,
                    ResourceName = v.ResourceName,
                    ResourceValue = v.ResourceValue
                }).ToList();
            }
        }

        public static Page GetMainPage()
        {
            var page = new NavigationPage() { BindingContext = new NavigationBaseViewModel() };
            return page;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
