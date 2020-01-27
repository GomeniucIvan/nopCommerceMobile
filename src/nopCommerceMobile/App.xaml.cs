using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Localization;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.Views;
using SQLite;
using Xamarin.Forms;

namespace nopCommerceMobile
{
    //remove from all pages default navigation TODO
    //fix translator on first loading TODO
    //add default image(for CachedImage plugin) TODO
    //add web slider functionality TODO
    //implement baerer auth TODO
    //implement pagination TODO
    //add web locale resources version (when value is updated or added update mobile db) TODO
    //add web customer claims based on email/password TODO
    //in cs files use styles/variables from app.xaml TODO

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

            await _customerService.SetCurrentCustomer();
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
                var dbLocaleResources = await database.Table<LocaleResource>().ToListAsync().ConfigureAwait(true);
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
            var appNavigationPage = new AppNavigationPage()
            {
                BindingContext = new AppNavigationBaseViewModel()
            };
            var page = new NavigationPage(appNavigationPage);
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
