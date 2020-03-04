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

            if (AppNavigationPage.Vm.SelectedNavigationPage != NavigationPageEnum.ShoppingCart || AppNavigationPage.Vm.SelectedNavigationPage != NavigationPageEnum.WishList)
                return;

            if (AppNavigationPage.Vm.SelectedNavigationPage == NavigationPageEnum.ShoppingCart)
                await ViewModel.InitializeCartAsync();

            else if (AppNavigationPage.Vm.SelectedNavigationPage == NavigationPageEnum.WishList)
                await ViewModel.InitializeWishListAsync();
        }

        private void Remove_OnCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkbox = (CheckBox) sender;
            var shoppingCartItem = (ShoppingCartModel.ShoppingCartItemModel) checkbox.BindingContext;
            shoppingCartItem.Remove = checkbox.IsChecked;
        }
    }
}