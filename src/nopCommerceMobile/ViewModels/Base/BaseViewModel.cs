using nopCommerceMobile.Services.Common;
using nopCommerceMobile.Services.Dependency;

namespace nopCommerceMobile.ViewModels.Base
{
    public abstract class BaseViewModel : ExtendedBindableObject
    {
        protected readonly IDependencyService DependencyService;
        private readonly IToastPopUpService _toastPopUp;

        protected BaseViewModel()
        {
            DependencyService = LocatorViewModel.Resolve<IDependencyService>();
            _toastPopUp = DependencyService.Get<IToastPopUpService>();
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

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                RaisePropertyChanged(()=> Title);
            }
        }

        public void DisplayToastNotification(string message, NotificationTypeEnum messageType = NotificationTypeEnum.Success)
        {
            _toastPopUp.ShowToastMessage(message, messageType);
        }
    }
}