using nopCommerceMobile.ViewModels.Base;
using System.Threading.Tasks;
using nopCommerceMobile.Views.Catalog;

namespace nopCommerceMobile.Services.Navigation
{
    public interface INavigationService
    {
		BaseViewModel PreviousPageBaseViewModel { get; }

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
        bool PageExist<T>(T categoriesPage);
    }
}