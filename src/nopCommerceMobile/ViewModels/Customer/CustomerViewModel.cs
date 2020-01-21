using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Topics;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Topic;
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
            if (!IsDataLoaded)
            {
                IsBusy = true;

                var customer = await _customerService.GetCurrentCustomerModelAsync();

                CustomerModel = customer;
                IsRegistered = customer.IsRegistered();

                IsBusy = false;
                IsDataLoaded = true;
            }
        }
    }
}
