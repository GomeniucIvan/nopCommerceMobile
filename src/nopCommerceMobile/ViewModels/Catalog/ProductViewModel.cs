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

        //based on this bool add background black opacity, disable scrolling, and add tapgesture to the grid to close current model
        private bool _isBottomModelVisible;
        public bool IsBottomModelVisible
        {
            get => _isBottomModelVisible;
            set
            {
                _isBottomModelVisible = value;
                RaisePropertyChanged(()=> IsBottomModelVisible);
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
