using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.News;

namespace nopCommerceMobile.Views.News
{
    public abstract class NewsPageXaml : ModelBoundContentPage<NewsViewModel> { }
    public partial class NewsPage : NewsPageXaml
    {
        public NewsPage()
        {
            InitializeComponent();
        }
    }
}