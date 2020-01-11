using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.Views
{
    public abstract class NavigationTabbedPageXaml : ModelBoundTabbedPage<NavigationBaseViewModel> { }
    public partial class NavigationTabbedPage : NavigationTabbedPageXaml
    {
        public NavigationTabbedPage()
        {
            InitializeComponent();
            CurrentPage = Children[1]; //set default tab (home page - second tab)
            BindingContext = new NavigationBaseViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.InitializeAsync();
        }
    }
}