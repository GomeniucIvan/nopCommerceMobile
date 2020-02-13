using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Common;
using nopCommerceMobile.Models.Customer;

namespace nopCommerceMobile.Services.Customer
{
    public interface ICustomerService
    {
        Task<GenericModel<LoginModel>> GetLoginModelAsync();
        Task<RegisterModel> GetRegisterModelAsync();
        Task<GenericModel<Guid>> LoginAsync(LoginModel model);
        void LogoutCustomer();
        void SetCurrentCustomer(bool refreshData = false);
        Task CreateOrUpdateCustomerSettings(bool updateTable = false, bool generateNewToken = false);
        Task CreateOrUpdateShoppingCartItems();
        Task RegisterAsync(RegisterModel model);
        Task<ObservableCollection<LanguageModel>> GetLanguagesAsync();
    }
}
