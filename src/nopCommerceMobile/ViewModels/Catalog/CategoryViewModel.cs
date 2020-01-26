using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Catalog
{
    public class CategoryViewModel : BaseViewModel
    {
        #region Fields

        private ICatalogService _catalogService;

        #endregion

        #region Ctor

        public CategoryViewModel()
        {
            if (_catalogService == null)
                _catalogService = LocatorViewModel.Resolve<ICatalogService>();
        }

        #endregion

        private CategoryModel _category;
        public CategoryModel Category
        {
            get => _category;
            set
            {
                _category = value;
                RaisePropertyChanged(() => Category);
            }
        }

        private bool _isRightModalVisible;
        public bool IsRightModalVisible
        {
            get => _isRightModalVisible;
            set
            {
                _isRightModalVisible = value;
                RaisePropertyChanged(()=> IsRightModalVisible);
            }
        }
    }
}
