using System.Threading.Tasks;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels
{
    public class NavigationBaseViewModel : BaseViewModel
    {
        private bool _isRegisteredCustomer;
        public bool IsRegisteredCustomer
        {
            get => _isRegisteredCustomer;
            set
            {
                _isRegisteredCustomer = value;
                RaisePropertyChanged(() => IsRegisteredCustomer);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;
            IsRegisteredCustomer = false; //to implement
            IsBusy = false;
        }
    }
}
