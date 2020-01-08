using System.Threading.Tasks;

namespace nopCommerceMobile.Services
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);
    }
}
