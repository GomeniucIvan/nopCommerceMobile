using System;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using Xamarin.Forms;

namespace nopCommerceMobile.Views
{
    public abstract class HomePageXaml : ModelBoundContentPage<HomeBaseViewModel> { }
    public partial class HomePage : HomePageXaml
    {
        public static HomePage _page;
        public HomePage()
        {
            InitializeComponent();
            _page = this;
            BindingContext = new HomeBaseViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.InitializeAsync();
        }

        private void Category_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var category = (CategoryModel)view.BindingContext;

            //just test
            //ToDo redirect to category page
        }

        private void Product_OnClick(object sender, EventArgs e)
        {
        }

        private void News_OnClick(object sender, EventArgs e)
        {
           
        }
    }

}