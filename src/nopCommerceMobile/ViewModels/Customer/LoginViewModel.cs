using System;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Models.Topics;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Topic;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Customer
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields

        private ICustomerService _customerService;
        private ITopicService _topicService;

        #endregion

        #region Ctor

        public LoginViewModel()
        {
            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();

            if (_topicService == null)
                _topicService = LocatorViewModel.Resolve<ITopicService>();
        }

        #endregion

        private LoginModel _loginModel;
        public LoginModel LoginModel
        {
            get => _loginModel;
            set
            {
                _loginModel = value;
                RaisePropertyChanged(() => LoginModel);
            }
        }

        private TopicModel _loginRegistrationInfo;
        public TopicModel LoginRegistrationInfo
        {
            get => _loginRegistrationInfo;
            set
            {
                _loginRegistrationInfo = value;
                RaisePropertyChanged(() => LoginRegistrationInfo);
            }
        }

        public async Task InitializeAsync()
        {
            if (!IsDataLoaded)
            {
                IsBusy = true;

                var genericLoginModel = await _customerService.GetLoginModelAsync();
                if (!genericLoginModel.IsSuccessStatusCode)
                    DisplayToastNotification(genericLoginModel.ErrorMessage, NotificationTypeEnum.Warning);

                else
                    LoginModel = genericLoginModel.Data;

                LoginRegistrationInfo = await _topicService.GetModelBySystemNameAsync("LoginRegistrationInfo");

                IsBusy = false;
                IsDataLoaded = true;
            }
        }

        public async Task<GenericModel<Guid>> LoginCustomer()
        { 
           return await _customerService.LoginAsync(LoginModel);
        }

        public async Task SetCurrentCustomer()
        {
            await _customerService.CreateOrUpdateCustomerSettings(true, true);
            _customerService.SetCurrentCustomer(true);
        }
    }
}
