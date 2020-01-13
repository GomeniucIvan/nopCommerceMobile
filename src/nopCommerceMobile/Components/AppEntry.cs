using nopCommerceMobile.Extensions;
using nopCommerceMobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace nopCommerceMobile.Components
{
    // https://xamgirl.com/image-entry-in-xamarin-forms/

    [ContentProperty("Content")]
    public class AppEntry : ContentView
    {
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(AppEntry), string.Empty, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.CreateAttached(nameof(BorderColor), typeof(Color), typeof(AppEntry), Color.FromHex("1e5474"), BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(int), typeof(AppEntry), 40, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IconFontFamilyProperty = BindableProperty.Create(nameof(IconFontFamily), typeof(string), typeof(AppEntry), string.Empty, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(AppEntry), Color.FromHex("1e5474"), BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty BorderCornerRadiusProperty = BindableProperty.CreateAttached(nameof(BorderCornerRadius), typeof(float), typeof(AppEntry), 8f, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.CreateAttached(nameof(Placeholder), typeof(string), typeof(AppEntry), string.Empty, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty TextProperty = BindableProperty.CreateAttached(nameof(Text), typeof(string), typeof(AppEntry), string.Empty, BindingMode.TwoWay, null, EntryPropertyChanged);
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.CreateAttached(nameof(IsPassword), typeof(bool), typeof(AppEntry), false, BindingMode.Default);

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public int IconSize
        {
            get => (int)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string IconFontFamily
        {
            get => (string)GetValue(IconFontFamilyProperty);
            set => SetValue(IconFontFamilyProperty, value);
        }

        public Color IconColor
        {
            get => (Color)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public float BorderCornerRadius
        {
            get => (float)GetValue(BorderCornerRadiusProperty);
            set => SetValue(BorderCornerRadiusProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        private static void EntryPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var itemsViewGrid = (AppEntry)bindable;
            itemsViewGrid.ReloadSource();
        }

        private void ReloadSource()
        {
            var pancakeView = new PancakeView
            {
                BorderColor = BorderColor,
                CornerRadius = BorderCornerRadius,
                BorderThickness = 1
            };

            //display icon
            var entry = new Entry
            {
                PlaceholderColor = Color.Gray,
                Placeholder = Placeholder,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = Text,
                IsPassword = IsPassword
            };

            if (!Icon.IsNullOrEmpty() && !IconFontFamily.IsNullOrEmpty())
            {
                var grid = new Grid
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = IconSize + 10 },
                        new ColumnDefinition { Width = GridLength.Star},
                    }
                };

                if (IconFontFamily == "Ionicons")
                {
                    var iconLabel = new IoniconsLabel(Icon)
                    {
                        Padding = new Thickness(10, 0, 0, 0),
                        TextColor = IconColor,
                        FontSize = IconSize,
                        VerticalOptions = LayoutOptions.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (sender, e) => { FocusEntry(entry); };
                    iconLabel.GestureRecognizers.Add(tapGestureRecognizer);
                    grid.Children.Add(iconLabel, 0, 0);
                }

                if (IconFontFamily == "FontAwesome")
                {
                    var iconLabel = new FontAwesomeLabel(Icon)
                    {
                        Padding = new Thickness(10, 0, 0, 0),
                        TextColor = IconColor,
                        FontSize = IconSize,
                        VerticalOptions = LayoutOptions.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (sender, e) => { FocusEntry(entry); };
                    iconLabel.GestureRecognizers.Add(tapGestureRecognizer);
                    grid.Children.Add(iconLabel, 0, 0);
                }

                grid.Children.Add(entry, 1, 0);
                pancakeView.Content = grid;
            }
            else
                pancakeView.Content = entry;

            Content = pancakeView;
        }

        private void FocusEntry(Entry entry)
        {
            if (!entry.IsFocused)
                entry.Focus();
        }
    }
}
