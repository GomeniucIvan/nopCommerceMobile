using System.Threading.Tasks;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Customer
{
    public class LoginPageViewModel : BaseViewModel
    {
        public async Task InitializeAsync()
        {
            IsBusy = true;



            IsBusy = false;
            IsDataLoaded = true;
        }
    }
}
