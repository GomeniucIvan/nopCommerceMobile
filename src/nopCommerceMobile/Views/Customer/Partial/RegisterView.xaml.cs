using System;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Customer;

namespace nopCommerceMobile.Views.Customer.Partial
{
    public abstract class RegisterViewXaml : ModelBoundContentView<RegisterViewModel> { }
    public partial class RegisterView : RegisterViewXaml
    {
        public RegisterView()
        {
            InitializeComponent();

            if (BindingContext == null)
                BindingContext = new RegisterViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            await ViewModel.InitializeAsync();
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            LoginRegisterPage._page.SlideToLoginView();
        }

        private void NewsletterLabel_OnTapped(object sender, EventArgs e)
        {
        }

        private void Register_OnClicked(object sender, EventArgs e)
        {
        }
    }
}