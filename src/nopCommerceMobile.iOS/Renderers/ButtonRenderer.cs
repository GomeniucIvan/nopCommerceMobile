using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace nopCommerceMobile.iOS.Renderers
{
    public class ButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        public ButtonRenderer()
        {
            
        }

        //by default button text is not uppercase, change style
        //TODO need to test
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null) 
                return;

            Control.TitleLabel.Text = Control.TitleLabel.Text.ToUpper();
        }
    }
}