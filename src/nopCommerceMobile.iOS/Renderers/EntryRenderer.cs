using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace nopCommerceMobile.iOS.Renderers
{
    public class EntryRenderer : Xamarin.Forms.Platform.iOS.EntryRenderer
    {
        public EntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }

}