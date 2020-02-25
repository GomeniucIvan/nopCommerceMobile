using System;
using nopCommerceMobile.Helpers;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.Catalog
{
    public abstract class ProductPageXaml : ModelBoundContentPage<ProductViewModel> { }
    public partial class ProductPage : ProductPageXaml
    {
        public static ProductPage Page;

        public ProductPage()
        {
            InitializeComponent();
            Page = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.InitializeAsync();

            //initialize stack layout on first click fade to not working
            var mainStackLayout = new StackLayout()
            {
                HeightRequest = Height - 150
            };
            AppearingFrame.Content = mainStackLayout;
            HideFrame();
            RefreshToolbarItems();
        }

        private async void ProductSpec_OnTapped(object sender, EventArgs e)
        {
            var mainStackLayout = new StackLayout()
            {
                HeightRequest = Height - 150
            };

            var closeLabel = new IoniconsLabel()
            {
                Text = IoniconsIcon.IosCloseRound,
                Padding = new Thickness(0,0,45,0),
                FontSize = 30,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, oe) => { HideFrame();  };
            closeLabel.GestureRecognizers.Add(tapGestureRecognizer);

            mainStackLayout.Children.Add(closeLabel);

            foreach (var specificationAttributeModel in ViewModel.Product.SpecificationAttributeModels)
            {
                var stackLayout = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };

                var specLabel = new Label()
                {
                    Text = specificationAttributeModel.SpecificationAttributeName,
                    FontSize = 18,
                    TextColor = Color.FromHex("#f3f3f3")
                };

                var specValue = new Label()
                {
                    Text = specificationAttributeModel.ValueRaw,
                    FontSize = 18,
                    TextColor = Color.FromHex("#f3f3f3"),
                    HorizontalOptions = LayoutOptions.EndAndExpand
                };

                var boxView = new BoxView()
                {
                    Color = Color.Gainsboro,
                    HeightRequest = 1
                };

                stackLayout.Children.Add(specLabel);
                stackLayout.Children.Add(specValue);
                stackLayout.Children.Add(boxView);

                mainStackLayout.Children.Add(stackLayout);
            }

            AppearingFrame.Content = mainStackLayout;
            ViewModel.IsBottomModelVisible = true;
            await AppearingFrame.TranslateTo(0, 0);
        }

        private async void FullDescription_OnTapped(object sender, EventArgs e)
        {
            var mainStackLayout = new StackLayout()
            {
                HeightRequest = Height - 150
            };

            var closeLabel = new IoniconsLabel()
            {
                Text = IoniconsIcon.IosCloseRound,
                Padding = new Thickness(0, 0, 45, 0),
                FontSize = 30,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, oe) => { HideFrame(); };
            closeLabel.GestureRecognizers.Add(tapGestureRecognizer);

            mainStackLayout.Children.Add(closeLabel);

            var fullDescriptionLabel = new Label()
            {
                Text = ViewModel.Product.FullDescription,
                TextType = TextType.Html,
                FontSize = 18
            };

            mainStackLayout.Children.Add(fullDescriptionLabel);

            AppearingFrame.Content = mainStackLayout;
            ViewModel.IsBottomModelVisible = true;
            await AppearingFrame.TranslateTo(0, 0);
        }

        private void HideFrame()
        {
            AppearingFrame.TranslateTo(0, Height + 100);
            ViewModel.IsBottomModelVisible = false;
        }

        private async void AddProductToCart_OnClicked(object sender, EventArgs e)
        {
            await ViewModel.AddProductToCartAsync();
        }

        public void RefreshToolbarItems()
        {
            ToolbarItems.Children.Clear();
            ToolbarItems.Children.Add(new Helpers.ShoppingCart());
            ToolbarItems.Children.Add(new ToolbarMenuButton());
        }

        private void ShoppingCart_OnClicked(object sender, EventArgs e)
        {
        }
    }
}