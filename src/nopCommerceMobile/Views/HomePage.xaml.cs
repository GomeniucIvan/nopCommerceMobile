using System;
using System.Threading.Tasks;
using nopCommerceMobile.Helpers;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using Xamarin.Forms;

namespace nopCommerceMobile.Views
{
    public abstract class HomePageXaml : ModelBoundContentPage<HomeViewModel> { }
    public partial class HomePage : HomePageXaml
    {
        public static HomePage _page;
        public HomePage()
        {
            InitializeComponent();
            _page = this;
            BindingContext = new HomeViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!ViewModel.IsDataLoaded)
            {
                await ViewModel.InitializeAsync();
            }
        }

        public async Task InitializeAsync(bool initializeData = false)
        {
            if (!ViewModel.IsDataLoaded || initializeData)
            {
                await ViewModel.InitializeAsync();
            }
        }

        private void Category_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var category = (CategoryModel)view.BindingContext;

            //just test
            //ToDo redirect to category page
        }
    }

}