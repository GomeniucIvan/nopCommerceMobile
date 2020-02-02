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
using nopCommerceMobile.Views;
using SQLite;
using Xamarin.Forms;

namespace nopCommerceMobile
{
    //fix translator on first loading TODO
    //add default image(for CachedImage plugin) TODO
    //add web slider functionality TODO
    //implement baerer auth TODO
    //implement pagination TODO
    //add web locale resources version (when value is updated or added update mobile db) TODO
    //add web customer claims based on email/password TODO
    //in cs files use styles/variables from app.xaml TODO
    //change popup notification - stack layout to absolute layout, to be able to scroll behind content TODO

    public partial class App : Application
    {
        #region Fields

        public static CustomerModel CurrentCostumer;
        public static IList<LocaleResourceModel> LocaleResources;
        public static bool AppInitialized;

        #endregion

        #region Ctor

        public App()
        {
            InitializeComponent();

            InitApp();
        }

        #endregion

        private void InitApp()
        {
            MainPage = GetMainPage();
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
