using System;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.Catalog
{
    public abstract class CategoryPageXaml : ModelBoundContentPage<CategoryViewModel> { }
    public partial class CategoryPage : CategoryPageXaml
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        private async void Product_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var selectedProduct = (ProductModel)view.BindingContext;

            var productPage = new ProductPage()
            {
                Title = selectedProduct.Name,
                BindingContext = new ProductViewModel()
                {
                    ProductId = selectedProduct.Id
                }
            };

            await Navigation.PushAsync(productPage);
        }

        private void Filter_OnClick(object sender, EventArgs e)
        {
            //add website filters from product spec, than add filter modal page
        }
    }
}