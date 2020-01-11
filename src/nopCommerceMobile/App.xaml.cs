using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.Views;
using Xamarin.Forms;

namespace nopCommerceMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitApp();
        }

        private void InitApp()
        {
            MainPage = GetMainPage();
        }

        public static Page GetMainPage()
        {
            var page = new NavigationTabbedPage()
            {
                BindingContext = new NavigationBaseViewModel()
                {
                    //TODO add count of favorite/card products
                }
            };
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
