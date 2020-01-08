using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.Extensions
{
    public static class HtmlExtensions
    {
        public static bool IsNotNull(this ViewModelBase model)
        {
            return model != null;
        }

        public static bool IsNull(this ViewModelBase model)
        {
            return model == null;
        }
    }
}
