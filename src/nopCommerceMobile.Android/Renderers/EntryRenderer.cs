using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace nopCommerceMobile.Droid.Renderers
{
    public class EntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        public EntryRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                Control.Background = gd;
                Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                //Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
    }
}