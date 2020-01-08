using nopCommerceMobile.Extensions;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.Views
{
    public abstract class HomePageXaml : ModelBoundContentPage<HomeViewModel> { }
    public partial class HomePage : HomePageXaml
    {
        public static HomeViewModel vm;

        public HomePage()
        {
            InitializeComponent();

            if (vm.IsNull())
                vm = new HomeViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.InitializeAsync();
        }
    }
}