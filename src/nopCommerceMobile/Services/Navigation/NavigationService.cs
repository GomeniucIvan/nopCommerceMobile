using System.Linq;
using System.Threading.Tasks;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.Views.Catalog;
using Xamarin.Forms;

namespace nopCommerceMobile.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public BaseViewModel PreviousPageBaseViewModel
        {
            get
            {
                var mainPage = Application.Current.MainPage as NavigationPage;
                var viewModel = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2].BindingContext;
                return viewModel as BaseViewModel;
            }
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        public bool PageExist<T>(T categoriesPage)
        {
            var mainPage = Application.Current.MainPage as NavigationPage;
            if (mainPage != null)
            {
                return mainPage.Navigation.NavigationStack.Any(v => v.GetType() == categoriesPage.GetType());
            }

            return false;
        }
    }
}