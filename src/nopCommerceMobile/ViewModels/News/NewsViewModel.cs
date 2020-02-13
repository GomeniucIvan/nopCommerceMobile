using nopCommerceMobile.Models.News;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.News
{
    public class NewsViewModel : BaseViewModel
    {
        private NewsItemModel _newsItem;
        public NewsItemModel NewsItem
        {
            get => _newsItem;
            set
            {
                _newsItem = value;
                RaisePropertyChanged(()=> NewsItem);
            }
        }
    }
}
