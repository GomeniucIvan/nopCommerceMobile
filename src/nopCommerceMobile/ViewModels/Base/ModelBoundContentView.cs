using Xamarin.Forms;

namespace nopCommerceMobile.ViewModels.Base
{
    public abstract class ModelBoundContentView<TViewModel> : ContentView where TViewModel : BaseViewModel
    {
        public TViewModel ViewModel => BindingContext as TViewModel;
    }
}
