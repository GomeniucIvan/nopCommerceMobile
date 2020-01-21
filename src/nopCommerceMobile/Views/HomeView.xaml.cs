using System;
using FFImageLoading.Forms;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using Xamarin.Forms;

namespace nopCommerceMobile.Views
{
    public abstract class HomeViewXaml : ModelBoundContentView<HomeBaseViewModel> { }
    public partial class HomeView : HomeViewXaml
    {
        public static HomeView View;
        public HomeView()
        {
            InitializeComponent();
            View = this;
            if (BindingContext == null)
                BindingContext = new HomeBaseViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            await ViewModel.InitializeAsync();
        }

        //protected override async void OnAppearing()
        //{
        //    base.OnAppearing();
        //    await ViewModel.InitializeAsync();
        //}

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

        private void Slider_OnClick(object sender, EventArgs e)
        {

        }

        private void SliderView_OnCurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            var view = (CarouselView)sender;
            var slider = (SliderModel)view.CurrentItem;

            SliderBackground.Source = slider.Image;
        }
    }

}