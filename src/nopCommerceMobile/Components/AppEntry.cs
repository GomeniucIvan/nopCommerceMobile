using System.ComponentModel;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace nopCommerceMobile.Components
{
    // https://xamgirl.com/image-entry-in-xamarin-forms/

    //TODO after adding all required fields change binding options
    public class AppEntry : StackLayout
    {
        public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(AppEntry), string.Empty, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AppEntry), Color.FromHex("1e5474"), BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(int), typeof(AppEntry), 40, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IconFontFamilyProperty = BindableProperty.Create(nameof(IconFontFamily), typeof(string), typeof(AppEntry), string.Empty, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(AppEntry), Color.FromHex("1e5474"), BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty BorderCornerRadiusProperty = BindableProperty.Create(nameof(BorderCornerRadius), typeof(float), typeof(AppEntry), 8f, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AppEntry), string.Empty, BindingMode.Default,null, EntryPropertyChanged);
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(AppEntry), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(AppEntry),false, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(AppEntry), false, BindingMode.Default, null, EntryPropertyChanged);
        public static readonly BindableProperty IsRequiredIconVisibleProperty = BindableProperty.Create(nameof(IsRequiredIconVisible), typeof(bool), typeof(AppEntry), false, BindingMode.Default, null, EntryPropertyChanged);

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

        public bool IsRequired
        {
            get => (bool)GetValue(IsRequiredProperty);
            set => SetValue(IsRequiredProperty, value);
        }

        public bool IsRequiredIconVisible
        {
            get => (bool)GetValue(IsRequiredIconVisibleProperty);
            set => SetValue(IsRequiredIconVisibleProperty, value);
        }

        private static void EntryPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var itemsViewGrid = (AppEntry)bindable;
            itemsViewGrid.ReloadSource();
        }

        private void ReloadSource()
        {
            Children.Clear();

            var pancakeView = new PancakeView
            {
                BorderColor = BorderColor,
                CornerRadius = BorderCornerRadius,
                BorderThickness = 1
            };

            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            if (IsRequired)
                IsRequiredIconVisible = Text.IsNullOrEmpty();

            var requiredLabel = new IoniconsLabel(IoniconsIcon.Alert)
            {
                TextColor = Color.Red,
                FontSize = 16,
                BindingContext = this,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(0, 0, 10, 0),
                IsVisible = IsRequiredIconVisible
            };

            if (IsRequired)
                requiredLabel.SetBinding(Label.IsVisibleProperty, "IsVisible", BindingMode.TwoWay);

            var entry = new Entry
            {
                PlaceholderColor = Color.Gray,
                Placeholder = Placeholder,
                BindingContext = this,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = Text,
                IsPassword = IsPassword,
            };

            entry.SetBinding(Entry.TextProperty, nameof(Text), BindingMode.TwoWay);

            if (IsRequired)
                entry.TextChanged += EntryOnTextChanged;

            if (!Icon.IsNullOrEmpty() && !IconFontFamily.IsNullOrEmpty())
            {
                grid.ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition {Width = IconSize + 10},
                    new ColumnDefinition {Width = GridLength.Star},
                    new ColumnDefinition {Width = GridLength.Auto}
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

                if (IsRequired && Text.IsNullOrEmpty())
                    grid.Children.Add(requiredLabel, 2, 0);
            }
            else
            {
                grid.ColumnDefinitions = new ColumnDefinitionCollection()
                {
                    new ColumnDefinition { Width = GridLength.Star},
                    new ColumnDefinition { Width = GridLength.Auto}
                };

                //display entry
                entry.Margin = new Thickness(5,0,0,0);
                grid.Children.Add(entry);

                if (IsRequired && Text.IsNullOrEmpty())
                    grid.Children.Add(requiredLabel, 1, 0);
            }

            pancakeView.Content = grid;

            Children.Add(pancakeView);
        }

        private void FocusEntry(Entry entry)
        {
            if (!entry.IsFocused)
                entry.Focus();
        }

        private void EntryOnTextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO find a way to hide required icon
            //SetValue(TextProperty, Text);
            //SetValue(IsRequiredIconVisibleProperty, false);
            //IsRequiredIconVisible = false;
            //requiredLabel.IsVisible = e.NewTextValue.IsNullOrEmpty();
        }
    }
}
