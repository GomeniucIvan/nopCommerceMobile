using System;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.Services.Navigation;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.Views.Catalog;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.Partial
{
    public partial class ItemViewTemplate : ContentView
    {
        #region Fields

        private IShoppingCartService _shoppingCartService;
        private INavigationService _navigationService;

        #endregion

        public ItemViewTemplate()
        {
            InitializeComponent();

            if (_shoppingCartService == null)
                _shoppingCartService = LocatorViewModel.Resolve<IShoppingCartService>();

            if (_navigationService == null)
                _navigationService = LocatorViewModel.Resolve<INavigationService>();
        }

        public bool ListViewModel { get; set; }

        public bool IsProduct
        {
            set
            {
                if (value)
                {
                    if (ListViewModel)
                    {
                        GridViewModelPancakeView.IsVisible = false;
                        ListViewModelPancakeView.IsVisible = true;
                    }
                    else
                    {
                        //image
                        ProductImage.IsVisible = true;
                        NotProductImage.IsVisible = false;
                        //details
                        ProductDetailsContainer.IsVisible = true;
                        NotProductDetailsContainer.IsVisible = false;

                        GridViewModelPancakeView.IsVisible = true;
                        ListViewModelPancakeView.IsVisible = false;
                    }
                }
                else
                {
                    //image
                    ProductImage.IsVisible = false;
                    NotProductImage.IsVisible = true;
                    //details
                    ProductDetailsContainer.IsVisible = false;
                    NotProductDetailsContainer.IsVisible = true;

                    GridViewModelPancakeView.IsVisible = true;
                    ListViewModelPancakeView.IsVisible = false;
                }
            }
        }

        private async void AddToCart_OnClick(object sender, EventArgs e)
        {
            var label = (Label) sender;
            var productModel = (ProductModel)label.BindingContext;

            await _shoppingCartService.AddProductToCartAsync(productModel);
        }
    }
}