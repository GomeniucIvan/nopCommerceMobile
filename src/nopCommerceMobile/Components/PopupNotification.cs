using System.Threading.Tasks;
using nopCommerceMobile.Helpers;
using nopCommerceMobile.ViewModels.Base;
using Xamarin.Forms;

namespace nopCommerceMobile.Components
{
    public class PopupNotification : Page
    {
        public string Text { get; set; }

        public NotificationTypeEnum MessageType { get; set; }

        public Color IconColor { get; set; }

        public string IconName { get; set; }

        public PopupNotification()
        {
            //Animation = new MoveAnimation(MoveAnimationOptions.Top, MoveAnimationOptions.Top);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (MessageType == NotificationTypeEnum.Success)
            {
                IconColor = Color.FromHex("#22c064");
                IconName = IoniconsIcon.IonCheckmarkCircled;

                Task.Run(async () =>
                {
                    await Task.Delay(3000); //3 seconds
                    ClosePopup();
                });
            }
            else if (MessageType == NotificationTypeEnum.Info)
            {
                IconColor = Color.FromHex("#1274d1");
                IconName = IoniconsIcon.IonInformationCircled;

                Task.Run(async () =>
                {
                    await Task.Delay(4000); //4 seconds
                    ClosePopup();
                });
            }
            else if (MessageType == NotificationTypeEnum.Warning)
            {
                IconColor = Color.IndianRed;
                IconName = IoniconsIcon.IonAlertCircled;

                Task.Run(async () =>
                {
                    await Task.Delay(5000); //5 seconds
                    ClosePopup();
                });
            }
            else if (MessageType == NotificationTypeEnum.Danger)
            {
                IconColor = Color.Red;
                IconName = IoniconsIcon.IonCloseCircled;

                // do nothing
            }

            var frame = new Frame()
            {
                CornerRadius = 7,
                InputTransparent = true,
                VerticalOptions = LayoutOptions.Start,
                HeightRequest = 60,
                Margin = 10,
                Padding = 5
            };

            var grid = new Grid()
            {
                ColumnSpacing = 10,
                Padding = 10,
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var icon = new Label()
            {
                TextColor = IconColor,
                Text = IconName,
                FontFamily = IoniconsFamilyName.FontFamily,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontSize = 40
            };

            var text = new Label()
            {
                Text = Text,
                FontSize = 16,
                VerticalTextAlignment = TextAlignment.Center,
            };

            grid.Children.Add(icon, 0, 0);
            grid.Children.Add(text, 1, 0);

            frame.Content = grid;
            frame.TranslateTo(0, -300);
            Parent = frame;
        }

        private void ClosePopup()
        {
            //this.Remove
        }

    }
}
