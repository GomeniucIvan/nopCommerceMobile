using System;
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

    public class ToolbarCartButtonStackLayout : StackLayout
    {
        public ToolbarCartButtonStackLayout()
        {
            Spacing = 0;

            //add badge TODO

            var ioniconsFontImage = new Label()
            {
                FontFamily = IoniconsFamilyName.FontFamily,
                Text = IoniconsIcon.Cart,
                FontSize = 30,
                WidthRequest = 50,
                BackgroundColor = Color.Transparent,
            };

            Children.Add(ioniconsFontImage);
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            //TODO display content
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
}
