using System.Collections.ObjectModel;
using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Common
{
    public class SelectListViewModel : BaseViewModel
    {
        public SelectListViewModel()
        {
            SelectList = new ObservableCollection<SelectListItemViewModel>();
        }

        public ObservableCollection<SelectListItemViewModel> SelectList { get; set; }
        public SelectListPageEnum SelectListPage { get; set; }
    }
}
