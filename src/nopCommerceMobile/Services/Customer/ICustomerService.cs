using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.ViewModels.Customer;

namespace nopCommerceMobile.Services.Customer
{
    public interface ICustomerService
    {
        Task<LoginModel> GetLoginModelAsync();
        Task<RegisterModel> GetRegisterModelAsync();
        Task<CustomerModel> LoginAsync(LoginModel model);
        Task<CustomerModel> GetCurrentCustomerModelAsync();
        void LogoutCustomer();
        Task SetCurrentCustomer(bool refreshData = false);
        Task CreateOrUpdateCustomerSettings();
        Task CreateOrUpdateShoppingCartItems();
    }
}
