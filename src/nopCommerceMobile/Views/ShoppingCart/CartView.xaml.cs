using nopCommerceMobile.Models.ShoppingCart;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Navigation;
using nopCommerceMobile.ViewModels.ShoppingCart;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.ShoppingCart
{
    public abstract class CartViewXaml : ModelBoundContentView<CartViewViewModel> { }
    public partial class CartView : CartViewXaml
    {
        public CartView()
        {
            InitializeComponent();
            if (BindingContext == null)
                BindingContext = new CartViewViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            if (AppNavigationPage.Vm.SelectedNavigationPage != NavigationPageEnum.ShoppingCart)
                return;

            await ViewModel.InitializeAsync();
        }

        private void Remove_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox) sender;
            var shoppingCartItem = (ShoppingCartModel.ShoppingCartItemModel) checkbox.BindingContext;
            shoppingCartItem.Remove = checkbox.IsChecked;
        }
    }
}