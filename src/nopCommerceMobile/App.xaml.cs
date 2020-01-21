using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using Akavache.Sqlite3;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Localization;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
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
        private Lazy<IBlobCache> _LazyBlob;
        private IBlobCache _BlobCache => _LazyBlob.Value;

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
            InitializeDataBase(); //move initialization after "GetCurrentCustomer" to load resources based on customer culture TODO
            GetCurrentCustomer();
            MainPage = GetMainPage();
        }

        private void InitializeDataBase()
        {
            SQLitePCL.Batteries_V2.Init();
            BlobCache.ApplicationName = "nopCommerce";
            var fs = Splat.Locator.Current.GetService(typeof(IFilesystemProvider)) as IFilesystemProvider;

            _ = fs ?? throw new ArgumentNullException(nameof(fs));
            _LazyBlob = new Lazy<IBlobCache>(() => new SQLitePersistentBlobCache(Path.Combine(fs.GetDefaultLocalMachineCacheDirectory(), "nopCommerce.db"), BlobCache.TaskpoolScheduler));

            _BlobCache.GetObject<IList<LocaleResourceModel>>("en-US")
                .Catch((KeyNotFoundException ke) => Observable.Return<List<LocaleResourceModel>>(null))
                .SelectMany(_ => _BlobCache.Flush())
                .SelectMany(_ => _BlobCache.GetObject<List<LocaleResourceModel>>("en-US"))
                .Subscribe(localeResources =>
                    {
                        LocaleResources = localeResources;

                        if (!LocaleResources.Any())
                        {
                            Task.Run(async () =>
                            {
                                LocaleResources = await _localizationService.GetLocaleResourcesByLanguageCultureAsync("en-US");
                                await _BlobCache.InsertObject("en-US", LocaleResources);
                            }).GetAwaiter();
                        }
                    }
                );
        }

        public async void GetCurrentCustomer()
        {
            if (CurrentCostumer == null || CurrentCostumer.Id == 0)
                CurrentCostumer = await _customerService.GetCurrentCustomerModelAsync();
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
