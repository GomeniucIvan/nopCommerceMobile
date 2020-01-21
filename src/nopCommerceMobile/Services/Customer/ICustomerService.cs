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
    }
}
