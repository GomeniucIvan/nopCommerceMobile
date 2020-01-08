using Android.Graphics;
using nopCommerceMobile.Droid.Renderers;
using nopCommerceMobile.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(FontAwesomeLabel), typeof(FontAwesomeLabelRenderer))]
namespace nopCommerceMobile.Droid.Renderers
{
    public class FontAwesomeLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Typeface = Typeface.CreateFromAsset(Forms.Context.Assets, FontAwesomeLabel.FontAwesomeFileName + ".ttf");
            }
        }
    }
}