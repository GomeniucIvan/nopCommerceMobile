using Xamarin.Forms;

namespace nopCommerceMobile.ViewModels.Base
{
    public abstract class ModelBoundTabbedPage<TViewModel> : TabbedPage where TViewModel : BaseViewModel
    {
        public TViewModel ViewModel => BindingContext as TViewModel;
    }
}
