using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.Services.RequestProvider;

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
    }
}
