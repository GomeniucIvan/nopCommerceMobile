using System;
using System.Linq;
using Foundation;
using nopCommerceMobile.Services.Common;
using nopCommerceMobile.ViewModels.Base;
using UIKit;

namespace nopCommerceMobile.iOS.Services.Common
{
    public class ToastPopUpService : IToastPopUpService
    {
        const double LongDelay = 3.5;
        const double ShortDelay = 2.0;

        NSTimer _lastAlertDelay;
        UIAlertController _lastAlert;

        public void ShowToastMessage(string message, NotificationTypeEnum messageType)
        {
            string backgroundHexColor = "#22c064";

            if (messageType == NotificationTypeEnum.Info)
                backgroundHexColor = "#1274d1";

            if (messageType == NotificationTypeEnum.Warning)
                backgroundHexColor = UIColor.SystemRedColor.ToString();

            if (messageType == NotificationTypeEnum.Danger)
                backgroundHexColor = UIColor.Red.ToString();

            var delay = messageType == NotificationTypeEnum.Warning || messageType == NotificationTypeEnum.Danger ? LongDelay : ShortDelay;

            var alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);

            var alertDelay = NSTimer.CreateScheduledTimer(delay, (obj) =>
            {
                DismissMessage(alert, obj, null);
            });

            _lastAlert = alert;
            _lastAlertDelay = alertDelay;

            var tView = alert.View;
            var firstSubView = tView.Subviews?.FirstOrDefault();
            var alertContentView = firstSubView?.Subviews?.FirstOrDefault();
            if (alertContentView != null)
                foreach (UIView uiView in alertContentView.Subviews)
                {
                    uiView.BackgroundColor = FromHexString(UIColor.Clear, backgroundHexColor);
                }

            var attributedString = new NSAttributedString(message, foregroundColor: FromHexString(UIColor.Clear, "#000000"));
            alert.SetValueForKey(attributedString, new NSString("attributedMessage"));
            GetVisibleViewController().PresentViewController(alert, true, null);
        }

        void DismissMessage(UIAlertController alert, NSTimer alertDelay, Action complete)
        {
            alert?.DismissViewController(true, complete);
            alert?.Dispose();
            alertDelay?.Dispose();
            _lastAlertDelay = null;
            _lastAlert = null;
        }

        private UIViewController GetVisibleViewController()
        {
            try
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                switch (rootController.PresentedViewController)
                {
                    case null:
                        return rootController;
                    case UINavigationController controller:
                        return controller.VisibleViewController;
                    case UITabBarController barController:
                        return barController.SelectedViewController;
                    default:
                        return rootController.PresentedViewController;
                }
            }
            catch (Exception)
            {
                return UIApplication.SharedApplication.KeyWindow.RootViewController;
            }
        }

        private static UIColor FromHexString(UIColor color, string hexValue, float alpha = 1.0f)
        {
            var colorString = hexValue.Replace("#", "");
            if (alpha > 1.0f)
            {
                alpha = 1.0f;
            }
            else if (alpha < 0.0f)
            {
                alpha = 0.0f;
            }

            float red, green, blue;

            switch (colorString.Length)
            {
                case 3: // #RGB
                {
                    red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16) / 255f;
                    green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16) / 255f;
                    blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16) / 255f;
                    return UIColor.FromRGBA(red, green, blue, alpha);
                }
                case 6: // #RRGGBB
                {
                    red = Convert.ToInt32(colorString.Substring(0, 2), 16) / 255f;
                    green = Convert.ToInt32(colorString.Substring(2, 2), 16) / 255f;
                    blue = Convert.ToInt32(colorString.Substring(4, 2), 16) / 255f;
                    return UIColor.FromRGBA(red, green, blue, alpha);
                }

                default:
                    throw new ArgumentOutOfRangeException(
                        $"Invalid color value {hexValue} is invalid. It should be a hex value of the form #RBG, #RRGGBB");

            }
        }

    }
}