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
        public static LoginRegisterPage _page;

        public LoginRegisterPage()
        {
            InitializeComponent();
            _page = this;
            _views = new View[]
            {
                new LoginView(),
                new RegisterView()
            };

            Carousel.ItemsSource = _views;
        }

        internal void SlideToRegisterView()
        {
            Carousel.Position = 1;
        }

        internal void SlideToLoginView()
        {
            Carousel.Position = 0;
        }
    }
}