using System.Threading.Tasks;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Navigation;
using nopCommerceMobile.Views;
using Xamarin.Forms;
using NavigationPage = nopCommerceMobile.Views.NavigationPage;

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

        private Color _homePageTabColor;
        public Color HomePageTabColor
        {
            get => _homePageTabColor;
            set
            {
                _homePageTabColor = value;
                RaisePropertyChanged(() => HomePageTabColor);
            }
        }

        private Color _accountTabColor;
        public Color AccountTabColor
        {
            get => _accountTabColor;
            set
            {
                _accountTabColor = value;
                RaisePropertyChanged(() => AccountTabColor);
            }
        }

        private NavigationPageEnum? _selectedNavigationPage;
        public NavigationPageEnum? SelectedNavigationPage
        {
            get => _selectedNavigationPage;
            set
            {
                _selectedNavigationPage = value;
                RaisePropertyChanged(() => SelectedNavigationPage);

                NavigationPage.Page.SetPageContentPage();
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
