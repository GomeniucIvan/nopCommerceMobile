using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using nopCommerceMobile.Droid.Services.Common;
using nopCommerceMobile.Services.Common;
using nopCommerceMobile.ViewModels.Base;
using Xamarin.Forms;
using Color = Android.Graphics.Color;
using View = Android.Views.View;

[assembly: Xamarin.Forms.Dependency(typeof(ToastPopUpService))]
namespace nopCommerceMobile.Droid.Services.Common
{
    //Based on https://github.com/ishrakland/Toast
    public class ToastPopUpService : IToastPopUpService
    {
        private static Toast _instance;

        public void ShowToastMessage(string message, NotificationTypeEnum messageType)
        {
            Color backgroundHexColor = Color.ParseColor("#22c064");

            if (messageType == NotificationTypeEnum.Info)
                backgroundHexColor = Color.ParseColor("#1274d1");

            if (messageType == NotificationTypeEnum.Warning)
                backgroundHexColor = Color.IndianRed;

            if (messageType == NotificationTypeEnum.Danger)
                backgroundHexColor = Color.Red;

            var length = messageType == NotificationTypeEnum.Warning || messageType == NotificationTypeEnum.Danger ? ToastLength.Long : ToastLength.Short;
            // To dismiss existing toast, otherwise, the screen will be populated with it if the user do so
            _instance?.Cancel();
            _instance = Toast.MakeText(Android.App.Application.Context, message, length);
            //_instance.SetGravity(GravityFlags.FillHorizontal | GravityFlags.Top, 0, 0);
            _instance.SetGravity(GravityFlags.Top, 0, 0);

            GradientDrawable gradientDrawable = new GradientDrawable(); 
            gradientDrawable.SetCornerRadius(3f); //set corner radius

            //add icon and change background color/text TODO
            View tView = _instance.View;
            tView.SetMinimumHeight(40);

            tView.SetBackgroundDrawable(gradientDrawable);
            tView.Background.SetColorFilter(backgroundHexColor, PorterDuff.Mode.SrcIn);//Gets the actual oval background of the Toast then sets the color filter
            tView.SetPadding(10,10,10,10);
            TextView text = (TextView)tView.FindViewById(Android.Resource.Id.Message);
            text.SetTextColor(Color.White);
            _instance.Show();
        }
    }
}