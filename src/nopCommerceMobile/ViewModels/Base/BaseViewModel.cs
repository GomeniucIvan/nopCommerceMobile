using nopCommerceMobile.Components;
using nopCommerceMobile.Services;
using nopCommerceMobile.Services.Navigation;
using Rg.Plugins.Popup.Services;

namespace nopCommerceMobile.ViewModels.Base
{
    public abstract class BaseViewModel : ExtendedBindableObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;

        protected BaseViewModel()
        {
            DialogService = LocatorViewModel.Resolve<IDialogService>();
            NavigationService = LocatorViewModel.Resolve<INavigationService>();
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

        public async void DisplayPopupNotification(string message, NotificationTypeEnum messageType = NotificationTypeEnum.Success)
        {
            var dialog = new PopupNotification { Text = message, MessageType = messageType };
            await PopupNavigation.Instance.PushAsync(dialog);
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
    }
}