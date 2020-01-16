using System.Threading.Tasks;
using nopCommerceMobile.Models.Customer;

namespace nopCommerceMobile.Services.Customer
{
    public interface ICustomerService
    {
        Task<LoginModel> GetLoginModelAsync();
        Task<RegisterModel> GetRegisterModelAsync();
    }
}
