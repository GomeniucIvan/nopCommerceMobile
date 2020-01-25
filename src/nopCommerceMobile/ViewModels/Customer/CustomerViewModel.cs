using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Customer
{
    public class CustomerViewModel : BaseViewModel
    {
        #region Fields

        private ICustomerService _customerService;

        #endregion

        #region Ctor

        public CustomerViewModel()
        {
            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();
        }

        #endregion

        private CustomerModel _customerModel;
        public CustomerModel CustomerModel
        {
            get => _customerModel;
            set
            {
                _customerModel = value;
                RaisePropertyChanged(() => CustomerModel);
            }
        }

        private bool _isRegistered;
        public bool IsRegistered
        {
            get => _isRegistered;
            set
            {
                _isRegistered = value;
                RaisePropertyChanged(() => IsRegistered);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            CustomerModel = App.CurrentCostumer;
            IsRegistered = CustomerModel.IsRegistered();

            IsBusy = false;
            IsDataLoaded = true;
        }
    }
}
