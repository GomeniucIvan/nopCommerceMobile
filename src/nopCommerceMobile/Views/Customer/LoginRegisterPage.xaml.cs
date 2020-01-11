using System;
using nopCommerceMobile.Views.Customer.Partial;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nopCommerceMobile.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRegisterPage : ContentPage
    {
        private View[] _views;

        public LoginRegisterPage()
        {
            InitializeComponent();

            _views = new View[]
            {
                new LoginView(),
                new RegisterView()
            };

            Carousel.ItemsSource = _views;
        }

        private void SlideToRegisterView(object sender, EventArgs e)
        {
            Carousel.Position = 1;
        }

        private void SlideToLoginView(object sender, EventArgs e)
        {
            Carousel.Position = 0;
        }
    }
}