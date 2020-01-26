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

        private async void Filter_OnClick(object sender, EventArgs e)
        {
            var mainStackLayout = new StackLayout()
            {
                WidthRequest = Width - 50,
                Padding = 0
            };

            var bottomButtonsStackLayout = new Grid();
            bottomButtonsStackLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
            bottomButtonsStackLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });

            //var resetLabel = new Label()
            //{
            //    Text = "Reset"
            //};

            //bottomButtonsStackLayout.Children.Add(topLeft, 0, 0);
            //bottomButtonsStackLayout.Children.Add(topRight, 1, 0);

            AppearingFrame.Content = mainStackLayout;
            ViewModel.IsRightModalVisible = true;
            await AppearingFrame.TranslateTo(0, 0);
        }
    }
}