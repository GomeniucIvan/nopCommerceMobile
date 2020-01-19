using System;
using nopCommerceMobile.Views.Customer.Partial;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nopCommerceMobile.Views.Customer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRegisterView : ContentView
    {
        private View[] _views;
        public static LoginRegisterView View;

        public LoginRegisterView()
        {
            InitializeComponent();
            _views = new View[]
            {
                new LoginView(),
                new RegisterView()
            };

            //Carousel.ItemsSource = _views;
            View = this;
        }

        //internal void SlideToRegisterView()
        //{
        //    Carousel.Position = 1;
        //}

        //internal void SlideToLoginView()
        //{
        //    Carousel.Position = 0;
        //}
    }
}