using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.Services.Common
{
    //Based on https://github.com/ishrakland/Toast
    public interface IToastPopUpService
    {
        void ShowToastMessage(string message, NotificationTypeEnum messageType);
    }
}
