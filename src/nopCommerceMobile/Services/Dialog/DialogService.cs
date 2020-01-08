using System.Threading.Tasks;

namespace nopCommerceMobile.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public async Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            //return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);

            await Task.Run(() => { });
        }
    }
}
