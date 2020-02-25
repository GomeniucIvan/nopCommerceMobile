using System;
using System.Linq;
using nopCommerceMobile.Extensions;
using Xamarin.Forms;

namespace nopCommerceMobile.Helpers
{
    public class ToolbarBackButton : Button
    {
        public ToolbarBackButton()
        {
            FontFamily = IoniconsFamilyName.FontFamily;
            Text = IoniconsIcon.IonArrowLeftC;
            FontSize = 30;
            Clicked+= OnClicked;
            WidthRequest = 50;
            BackgroundColor = Color.Transparent;
        }

        private async void OnClicked(object sender, EventArgs e)
        {
          await Navigation.PopAsync();
        }
    }

    public class ToolbarMenuButton : Button
    {
        public ToolbarMenuButton()
        {
            FontFamily = IoniconsFamilyName.FontFamily;
            Text = IoniconsIcon.NaviconRound;
            FontSize = 30;
            Clicked += OnClicked;
            WidthRequest = 50;
            BackgroundColor = Color.Transparent;
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            //TODO display navigation elements
        }
    }

    public class ToolbarFilterButton : Button
    {
        public ToolbarFilterButton()
        {
            FontFamily = IoniconsFamilyName.FontFamily;
            Text = IoniconsIcon.IonIosSettingsStrong;
            FontSize = 30;
            WidthRequest = 50;
            BackgroundColor = Color.Transparent;
        }
    }

    public class GridButton : Button
    {
        public GridButton()
        {
            FontFamily = IoniconsFamilyName.FontFamily;
            Text = IoniconsIcon.IonGrid;
            FontSize = 30;
            WidthRequest = 50;
            BackgroundColor = Color.Transparent;
        }
    }

    public class ListButton : Button
    {
        public ListButton()
        {
            FontFamily = IoniconsFamilyName.FontFamily;
            Text = IoniconsIcon.IonIosListOutline;
            FontSize = 30;
            WidthRequest = 50;
            BackgroundColor = Color.Transparent;
        }
    }

    public class ShoppingCart : AbsoluteLayout
    {
        private static AbsoluteLayout _absoluteLayout;

        public ShoppingCart()
        {
            _absoluteLayout = this;
            CartCount = App.CurrentCostumer == null ? 0 : App.CurrentCostumer.ShoppingCartItems.Where(v => v.ShoppingCartTypeId == 1).Sum(v => v.Quantity);
        }

        public static readonly BindableProperty CartCountProperty =
            BindableProperty.Create(nameof(CartCount), typeof(int), typeof(ShoppingCart), 0, BindingMode.Default, null, UpdateShoppingCart);

        public int CartCount
        {
            get => (int)GetValue(CartCountProperty);
            set => SetValue(CartCountProperty, value);
        }

        private static void UpdateShoppingCart(BindableObject bindable, object oldvalue, object newvalue)
        {

            _absoluteLayout.Children.Clear();
            _absoluteLayout.VerticalOptions = LayoutOptions.Center;

            var ioniconsFontImage = new Label()
            {
                FontFamily = IoniconsFamilyName.FontFamily,
                Text = IoniconsIcon.Cart,
                FontSize = 30,
                WidthRequest = 50,
                BackgroundColor = Color.Transparent,
            };

            _absoluteLayout.Children.Add(ioniconsFontImage);

            if ((int)newvalue > 0)
            {
                var boxView = new BoxView()
                {
                    BackgroundColor = Color.Red,
                    CornerRadius = 10,
                    WidthRequest = 18,
                    HeightRequest = 18,
                    Margin = new Thickness(17, 0, 0, 0)
                };
                _absoluteLayout.Children.Add(boxView);

                var label = new Label()
                {
                    Text = ((int)newvalue).ToString(),
                    Margin = new Thickness(22,0,0,0),
                    TextColor = Color.White,
                    Padding = 0,
                    VerticalOptions = LayoutOptions.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.End
                };

                _absoluteLayout.Children.Add(label);
            }
        }
    }

    public static class Helpers
    {
        public static string GetValueOrDefault(this string str, string defaultValue)
        {
            if (str.IsNullOrEmpty())
            {
                return defaultValue;
            }

            return str;
        }
    }

    public class SelectListItem
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
