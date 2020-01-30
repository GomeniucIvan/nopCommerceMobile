using FFImageLoading.Forms;
using Xamarin.Forms;

namespace nopCommerceMobile.Components
{
    public class Rating : StackLayout
    {
        private static StackLayout _stackLayout;

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(Rating), 5.0, BindingMode.Default, null, UpdateRating);

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public Rating()
        {
            _stackLayout = this;

            _stackLayout.Children.Clear();

            _stackLayout.Orientation = StackOrientation.Horizontal;
            _stackLayout.Spacing = 3;

            for (int i = 0; i < 5; i++)
            {
                var nextRating = i + 1;

                if (Value >= nextRating)
                {
                    var image = new CachedImage()
                    {
                        Source = "rating2.png",
                        HeightRequest = 25,
                        WidthRequest = 25

                    };
                    _stackLayout.Children.Add(image);
                }
                else
                {
                    var image = new CachedImage()
                    {
                        Source = "rating1.png",
                        HeightRequest = 25,
                        WidthRequest = 25
                    };
                    _stackLayout.Children.Add(image);
                }
            }
        }

        private static void UpdateRating(BindableObject bindable, object oldvalue, object newvalue)
        {
            _stackLayout.Children.Clear();

            _stackLayout.Orientation = StackOrientation.Horizontal;
            _stackLayout.Spacing = 3;

            for (int i = 0; i <= 5; i++)
            {
                var nextRating = i + 1;

                if ((double)newvalue >= nextRating)
                {
                    var image = new CachedImage()
                    {
                        Source = "rating2.png",
                        HeightRequest = 25,
                        WidthRequest = 25
                    };
                    _stackLayout.Children.Add(image);
                }
                else
                {
                    var image = new CachedImage()
                    {
                        Source = "rating1.png",
                        HeightRequest = 25,
                        WidthRequest = 25
                    };
                    _stackLayout.Children.Add(image);
                }
            }
        }
    }
}
