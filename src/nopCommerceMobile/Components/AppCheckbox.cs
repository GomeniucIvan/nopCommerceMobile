using System;
using Xamarin.Forms;

namespace nopCommerceMobile.Components
{
    public class AppCheckBox : StackLayout
    {
        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(AppCheckBox), false, BindingMode.Default, null, UpdateCheckedProperty);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public AppCheckBox()
        {
            RefreshCheckbox();

            GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command((Action<object>)delegate { IsChecked = !IsChecked; }) });
        }

        private static void UpdateCheckedProperty(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((AppCheckBox)bindable).RefreshCheckbox(true);
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        private void RefreshCheckbox(bool animate = false)
        {
            Children.Clear();
            Orientation = StackOrientation.Horizontal;

            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(25) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(25) });

            var frame = new Frame()
            {
                CornerRadius = 25,
                BorderColor = Color.Gray,
                HasShadow = false,
                Padding = 10
            };

            grid.Children.Add(frame, 0, 0);

            if (IsChecked) //checked
            {
                var innerFrame = new Frame()
                {
                    CornerRadius = 25,
                    BackgroundColor = Color.FromHex("#1e5474"),
                    HasShadow = false,
                    Padding = 20,
                    Margin = 5,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                };

                if (animate)
                {
                    innerFrame.ScaleTo(1.2, 300);
                }

                grid.Children.Add(innerFrame, 0, 0);
            }

            CheckedChanged?.Invoke(this, new CheckedChangedEventArgs
            {
                IsChecked = IsChecked
            });

            Children.Add(grid);
        }
    }

    public class CheckedChangedEventArgs : EventArgs
    {
        public bool IsChecked { get; set; }
    }
}
