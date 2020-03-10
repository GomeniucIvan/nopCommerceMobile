using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Models.Orders;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Navigation;
using nopCommerceMobile.Services.RequestProvider;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.Views.Catalog;
using Xamarin.Forms;

namespace nopCommerceMobile.Services.Catalog
{
    public class ShoppingCartService : IShoppingCartService
    {
        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/shoppingcart";

        private readonly IRequestProvider _requestProvider;
        private readonly ICustomerService _customerService;
        private readonly INavigationService _navigationService;

        public ShoppingCartService(
            IRequestProvider requestProvider,
            ICustomerService customerService,
            INavigationService navigationService)
        {
            _requestProvider = requestProvider;
            _customerService = customerService;
            _navigationService = navigationService;
        }

        public async Task AddProductToCartAsync(ProductModel model, int quantity = 1, ShoppingCartType productType = ShoppingCartType.ShoppingCart)
        {
            var uri = $"{ApiUrlBase}/addProduct?productId={model.Id}&shoppingCartTypeId={(int)productType}&quantity={quantity}";

            await _requestProvider.PostAsync<ProductDetailsModel>(uri, null);
            await _customerService.CreateOrUpdateShoppingCartItems();

            var product = new ShoppingCartItemModel
            {
                ProductId = model.Id,
                ShoppingCartTypeId = (int)productType,
                Quantity = quantity
            };
            App.CurrentCostumer.ShoppingCartItems.Add(product);

            bool categoriesPageExist = _navigationService.PageExist(new CategoriesPage());
            if (categoriesPageExist)
            {
                CategoriesPage.Page.RefreshToolbarItems();
            }

            bool categoryPageExist = _navigationService.PageExist(new CategoryPage());
            if (categoryPageExist)
            {
                CategoryPage.Page.RefreshToolbarItems();
            }

            bool productPageExist = _navigationService.PageExist(new ProductPage());
            if (productPageExist)
            {
                ProductPage.Page.RefreshToolbarItems();
            }

            var mainPage = Application.Current.MainPage as NavigationPage;
            if (mainPage != null)
            {
                var viewModel = mainPage.CurrentPage.BindingContext;
                var castModel = viewModel as BaseViewModel;
                castModel?.DisplayToastNotification(TranslateExtension.Translate("Mobile.Products.Added"));
            }
        }
    }
}
