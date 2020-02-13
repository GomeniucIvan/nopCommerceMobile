using System;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Helpers;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.Catalog
{
    public abstract class CategoryPageXaml : ModelBoundContentPage<CategoryViewModel> { }
    public partial class CategoryPage : CategoryPageXaml
    {
        public static CategoryPage Page;

        public CategoryPage()
        {
            InitializeComponent();
            Page = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var mainStackLayout = new StackLayout()
            {
                WidthRequest = Width - 50,
                Padding = 0,
            };
            AppearingFrame.Content = mainStackLayout;
            HideFrame();

            ViewModel.ListViewModel = App.CurrentCostumerSettings.ViewMode == "list";
            RefreshToolbarItems();
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
            //on background click or on swipe hide filter content, now use close icon TODO

            var mainStackLayout = new StackLayout()
            {
                WidthRequest = Width - 50,
                Padding = 0,
            };

            //add web filter options base on category implement spec attr TODO

            #region Close icon

            var closeLabel = new IoniconsLabel()
            {
                Text = IoniconsIcon.IosCloseRound,
                Padding = new Thickness(0, 10, 15, 0),
                FontSize = 30,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };

            var closeTapGestureRecognizer = new TapGestureRecognizer();
            closeTapGestureRecognizer.Tapped += (s, ea) =>
            {
                AppearingFrame.TranslateTo(450, 0);
                ViewModel.IsRightModalVisible = false;
            };
            closeLabel.GestureRecognizers.Add(closeTapGestureRecognizer);

            mainStackLayout.Children.Add(closeLabel);

            #endregion

            #region Bottom buttons

            var bottomButtonsStackLayout = new Grid()
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                Padding = 0,
                ColumnSpacing = 0
            };

            var resetLabel = new Label()
            {
                Text = TranslateExtension.Translate("Reset"),
                BackgroundColor = Color.LightPink,
                TextColor = Color.Red,
                FontSize = 18,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 50
            };

            var resetTapGestureRecognizer = new TapGestureRecognizer();
            resetTapGestureRecognizer.Tapped += (s, ea) => { ResetFilter(); };
            resetLabel.GestureRecognizers.Add(resetTapGestureRecognizer);


            var filterLabel = new Label()
            {
                Text = TranslateExtension.Translate("Filter"),
                BackgroundColor = Color.Red,
                TextColor = Color.White,
                FontSize = 18,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 50
            };


            var filterTapGestureRecognizer = new TapGestureRecognizer();
            filterTapGestureRecognizer.Tapped += (s, ea) => { Filter(); };
            filterLabel.GestureRecognizers.Add(filterTapGestureRecognizer);

            bottomButtonsStackLayout.Children.Add(resetLabel, 0, 0);
            bottomButtonsStackLayout.Children.Add(filterLabel, 1, 0);

            mainStackLayout.Children.Add(bottomButtonsStackLayout);

            #endregion

            AppearingFrame.Content = mainStackLayout;
            ViewModel.IsRightModalVisible = true;
            await AppearingFrame.TranslateTo(0, 0);

            AppearingFrame.IsVisible = true; // on appearing frame is opened than close, set is visible false to xaml
        }

        private void Filter()
        {
            //TODO

            HideFrame();
        }

        private void ResetFilter()
        {
            //TODO

            HideFrame();
        }

        private void HideFrame()
        {
            AppearingFrame.TranslateTo(450, 0);
            ViewModel.IsRightModalVisible = false;
        }

        private void Grid_OnClick(object sender, EventArgs e)
        {
            App.CurrentCostumerSettings.ViewMode = "grid";
            ViewModel.ListViewModel = false;
            ViewModel.UpdateViewMode(false);
        }

        private void List_OnClick(object sender, EventArgs e)
        {
            App.CurrentCostumerSettings.ViewMode = "list";
            ViewModel.ListViewModel = true;
            ViewModel.UpdateViewMode(true);
        }

        public void RefreshToolbarItems()
        {
            ToolbarItems.Children.Clear();
            ToolbarItems.Children.Add(new ShoppingCart());
        }
    }
}