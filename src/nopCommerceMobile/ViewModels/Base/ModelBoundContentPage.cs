using Xamarin.Forms;

namespace nopCommerceMobile.ViewModels.Base
{
    public abstract class ModelBoundContentPage<TViewModel> : ContentPage where TViewModel : ViewModelBase
    {
        public TViewModel ViewModel => BindingContext as TViewModel;
    }
}
