using System;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Customer;

namespace nopCommerceMobile.Views.Customer.Partial
{
    public abstract class LoginViewXaml : ModelBoundContentView<LoginViewModel> { }
    public partial class LoginView : LoginViewXaml
    {
        public LoginView()
        {
            InitializeComponent();

            if (BindingContext == null)
                BindingContext = new LoginViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            await ViewModel.InitializeAsync();
        }

        private void RememberMeLabel_OnTapped(object sender, EventArgs e)
        {
            ViewModel.LoginModel.RememberMe = !ViewModel.LoginModel.RememberMe;
        }

        private void Register_OnClicked(object sender, EventArgs e)
        {
            LoginRegisterPage._page.SlideToRegisterView();
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {

        }
    }
}