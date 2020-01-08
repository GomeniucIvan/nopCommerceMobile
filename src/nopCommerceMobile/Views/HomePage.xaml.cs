using System.Threading.Tasks;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.Views
{
    public abstract class HomePageXaml : ModelBoundContentPage<HomeViewModel> { }
    public partial class HomePage : HomePageXaml
    {
        public static HomePage _page;
        public HomePage()
        {
            InitializeComponent();
            _page = this;
            BindingContext = new HomeViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!ViewModel.IsDataLoaded)
            {
                await ViewModel.InitializeAsync();
            }
        }

        public async Task InitializeAsync(bool initializeData = false)
        {
            if (!ViewModel.IsDataLoaded || initializeData)
            {
                await ViewModel.InitializeAsync();
            }
        }
    }
}