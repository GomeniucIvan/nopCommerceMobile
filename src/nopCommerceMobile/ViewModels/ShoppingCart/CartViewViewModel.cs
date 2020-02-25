using System.Threading.Tasks;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Models.ShoppingCart;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.ShoppingCart
{
    public class CartViewViewModel : BaseViewModel
    {
        #region Fields

        private readonly ICustomerService _customerService;

        #endregion

        #region Ctor

        public CartViewViewModel()
        {
            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();
        }

        #endregion

        private ShoppingCartModel _shoppingCartModel;
        public ShoppingCartModel ShoppingCartModel
        {
            get => _shoppingCartModel;
            set
            {
                _shoppingCartModel = value;
                RaisePropertyChanged(()=> ShoppingCartModel);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            ShoppingCartModel = await _customerService.GetCartAsync();
            
            Title = $"{TranslateExtension.Translate("Mobile.Cart")}" + (ShoppingCartModel.Items.Count > 0 ? $" ({ShoppingCartModel.Items.Count})" : string.Empty);

            IsBusy = false;
            IsDataLoaded = true;
        }
    }
}
