using Xamarin.Forms;

namespace nopCommerceMobile.Helpers
{
    //https://ionicons.com/v2/
    //Version 2.0.1

    public class IoniconsLabel : Label
    {
        public static readonly string FileName = "ionicons";

        public IoniconsLabel(string fontLabel = null)
        {
            Text = fontLabel;
        }
    }

    public class IoniconsFontImage : FontImageSource
    {
        public static readonly string IoniconsFamilyName =
            Device.RuntimePlatform == Device.Android ? "ionicons.ttf#Ionicons" : "ionicons.ttf";

        public IoniconsFontImage()
        {
            FontFamily = IoniconsFamilyName;
        }

        public IoniconsFontImage(string fontLabel = null)
        {
            FontFamily = IoniconsFamilyName;
            Glyph = fontLabel;
            Size = 22;
        }
    }

    public static class IoniconsIcon
    {
        public static string NaviconRound = "\uf20d";
        public static string Navicon = "\uf20e";
        public static string Drag = "\uf130";
        public static string Person = "\uf213";
        public static string Hearth = "\uf443";
        public static string HearthOutline = "\uf442";
        public static string Cart = "\uf3f8";
        public static string CartOutline = "\uf3f7";
        public static string Gear = "\uf43d";
        public static string GearOutline = "\uf43c";
        public static string Settings = "\uf4a7";
        public static string SettingsOutline = "\uf4a6";
        public static string Trash = "\uf4c5";
        public static string TrashOutline = "\uf4c4";
        public static string IosPerson = "\uf47e";
        public static string IosPersonOutline = "\uf47d";
        public static string AndroidMenu = "\uf394";
        public static string AndroidPerson = "\uf3a0";
        public static string Home = "\uf448";
        public static string IosHome = "\uf447";
    }
}
