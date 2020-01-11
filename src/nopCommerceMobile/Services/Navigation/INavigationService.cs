using nopCommerceMobile.ViewModels.Base;
using System.Threading.Tasks;

namespace nopCommerceMobile.Services.Navigation
{
    public interface INavigationService
    {
		BaseViewModel PreviousPageBaseViewModel { get; }

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}