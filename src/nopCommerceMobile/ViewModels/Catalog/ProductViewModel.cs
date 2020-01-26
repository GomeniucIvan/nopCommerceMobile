using System.Threading.Tasks;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Catalog
{
    public class ProductViewModel : BaseViewModel
    {
        #region Fields

        private IProductService _productService;

        #endregion

        #region Ctor

        public ProductViewModel()
        {
            if (_productService == null)
                _productService = LocatorViewModel.Resolve<IProductService>();
        }

        #endregion

        public int ProductId { get; set; }

        private ProductDetailsModel _product;
        public ProductDetailsModel Product
        {
            get => _product;
            set
            {
                _product = value;
                RaisePropertyChanged(()=> Product);
            }
        }

        public async Task InitializeAsync()
        {
            if (!IsDataLoaded)
            {
                IsBusy = true;

                Product = await _productService.GetProductByIdAsync(ProductId);

                IsBusy = false;
                IsDataLoaded = true;
            }
        }
    }
}
