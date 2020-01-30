using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;
using nopCommerceMobile.ViewModels.Customer;

namespace nopCommerceMobile.Services.Customer
{
    public interface ICustomerService
    {
        Task<LoginModel> GetLoginModelAsync();
        Task<RegisterModel> GetRegisterModelAsync();
        Task<CustomerModel> GetCurrentCustomerModelAsync();
        Task<CustomerModel> LoginAsync(LoginModel model);
        void LogoutCustomer();
        Task SetCurrentCustomer(bool refreshData = false);
        Task UpdateViewMode(bool isList);
    }
}
