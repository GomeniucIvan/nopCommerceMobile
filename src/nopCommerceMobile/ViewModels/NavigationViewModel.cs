using System.Threading.Tasks;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels
{
    public class NavigationBaseViewModel : BaseViewModel
    {
        private bool _isRegistered;
        public bool IsRegistered
        {
            get => _isRegistered;
            set
            {
                _isRegistered = value;
                RaisePropertyChanged(() => IsRegistered);
            }
        }

        public async Task InitializeAsync()
        {
            IsBusy = true;

            IsBusy = false;
        }
    }
}
