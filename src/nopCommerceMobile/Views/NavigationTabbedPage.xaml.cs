using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.Views
{
    public abstract class NavigationTabbedPageXaml : ModelBoundTabbedPage<NavigationBaseViewModel> { }
    public partial class NavigationTabbedPage : NavigationTabbedPageXaml
    {
        //[Bug] Hot Reload: TabbedPage adds a new copy of tabs when is reloaded
        //https://github.com/xamarin/Xamarin.Forms/issues/8188
        //TODO fix a bug
        public NavigationTabbedPage()
        {
            InitializeComponent();
            CurrentPage = Children[1]; //set default tab (home page - second tab)
            BindingContext = new NavigationBaseViewModel();
        }
    }
}