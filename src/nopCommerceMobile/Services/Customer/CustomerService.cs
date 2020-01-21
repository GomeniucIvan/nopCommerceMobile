using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Services.RequestProvider;
using nopCommerceMobile.ViewModels.Customer;

namespace nopCommerceMobile.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/customer";

        private readonly IRequestProvider _requestProvider;

        public CustomerService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<LoginModel> GetLoginModelAsync()
        {
            var uri = $"{ApiUrlBase}/login";

            var loginModel = await _requestProvider.GetAsync<LoginModel>(uri);

            if (loginModel != null)
                return loginModel;

            return new LoginModel();
        }

        public async Task<RegisterModel> GetRegisterModelAsync()
        {
            var uri = $"{ApiUrlBase}/register";

            var registerModel = await _requestProvider.GetAsync<RegisterModel>(uri);

            if (registerModel != null)
                return registerModel;

            return new RegisterModel();
        }

        public async Task<CustomerModel> GetCurrentCustomerModelAsync()
        {
            var uri = $"{ApiUrlBase}/currentcustomer";

            var customerModel = await _requestProvider.GetAsync<CustomerModel>(uri);

            if (customerModel != null)
                return customerModel;

            return new CustomerModel();
        }
    }
}
