using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nopCommerceMobile.Views.Partial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemViewTemplate : ContentView
    {
        public ItemViewTemplate()
        {
            InitializeComponent();
        }

        public bool IsProduct
        {
            set
            {
                if (value)
                {
                    //image
                    ProductImage.IsVisible = true;
                    NotProductImage.IsVisible = false;
                    //details
                    ProductDetailsContainer.IsVisible = true;
                    NotProductDetailsContainer.IsVisible = false;
                }
                else
                {
                    //image
                    ProductImage.IsVisible = false;
                    NotProductImage.IsVisible = true;
                    //details
                    ProductDetailsContainer.IsVisible = false;
                    NotProductDetailsContainer.IsVisible = true;
                }
            }
        }
    }
}