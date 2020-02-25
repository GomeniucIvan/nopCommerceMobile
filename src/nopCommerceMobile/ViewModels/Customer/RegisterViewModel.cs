using System;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Customer
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Fields

        private ICustomerService _customerService;

        #endregion

        #region Ctor

        public RegisterViewModel()
        {
            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();
        }

        #endregion

        private RegisterModel _registerModel;
        public RegisterModel RegisterModel
        {
            get => _registerModel;
            set
            {
                _registerModel = value;
                RaisePropertyChanged(() => RegisterModel);
            }
        }

        public async Task InitializeAsync()
        {
            if (!IsDataLoaded)
            {
                IsBusy = true;

                RegisterModel = await _customerService.GetRegisterModelAsync();

                IsBusy = false;
                IsDataLoaded = true;
            }
        }

        public async Task<GenericModel<Guid>> RegisterCustomer()
        {
           return await _customerService.RegisterAsync(RegisterModel);
        }

        public async Task SetCurrentCustomer()
        {
            await _customerService.CreateOrUpdateCustomerSettings(true, true);
            _customerService.SetCurrentCustomer(true);
        }
    }
}
