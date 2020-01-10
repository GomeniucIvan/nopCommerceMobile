using Xamarin.Forms;

namespace nopCommerceMobile.Helpers
{
    //https://fontawesome.com/cheatsheet/free/solid
    //version 5.12.0

    public static class FontAwesomeFamilyName
    {
        public static string FontFamily =>
            Device.RuntimePlatform == Device.Android ? "fa-solid-900.ttf#Font Awesome 5 Free Solid" : "fa-solid-900.ttf";
    }

    public class FontAwesomeLabel : Label
    {
        public FontAwesomeLabel()
        {
            FontFamily = FontAwesomeFamilyName.FontFamily;
        }

        public FontAwesomeLabel(string fontAwesomeLabel = null)
        {
            FontFamily = FontAwesomeFamilyName.FontFamily;
            Text = fontAwesomeLabel;
        }
    }

    public class FontAwesomeFontImage : FontImageSource
    {
        public FontAwesomeFontImage()
        {
            FontFamily = FontAwesomeFamilyName.FontFamily;
        }

        public FontAwesomeFontImage(string fontAwesomeLabel = null)
        {
            FontFamily = FontAwesomeFamilyName.FontFamily;
            Glyph = fontAwesomeLabel;
            Size = 22;
        }
    }

    public static class Icon
    {
        //ToDo add pro icons

        // icon code must start with \u
        public static string FaHome = "\uf015";
        public static string FaBars = "\uf0c9";
        public static string FaHearth = "\uf004";
        public static string FaShoppingCart = "\uf07a";
    }
}
