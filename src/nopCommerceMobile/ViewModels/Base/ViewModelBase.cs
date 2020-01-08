using nopCommerceMobile.Services;
using nopCommerceMobile.Services.Navigation;
using Xamarin.Forms;

namespace nopCommerceMobile.ViewModels.Base
{
    public abstract class ViewModelBase : ExtendedBindableObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        protected ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();
        }

        private bool _isDataLoaded;
        public bool IsDataLoaded
        {
            get => _isDataLoaded;

            set
            {
                _isDataLoaded = value;
                RaisePropertyChanged(() => IsDataLoaded);
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }
    }
}