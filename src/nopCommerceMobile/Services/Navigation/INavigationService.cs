using nopCommerceMobile.ViewModels.Base;
using System.Threading.Tasks;

namespace nopCommerceMobile.Services.Navigation
{
    public interface INavigationService
    {
		ViewModelBase PreviousPageViewModel { get; }

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}