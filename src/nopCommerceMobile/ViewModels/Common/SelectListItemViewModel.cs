using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.ViewModels.Common
{
    public class SelectListItemViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                RaisePropertyChanged(()=> IsSelected);
            }
        }
        public bool IsMultiSelect { get; set; }
        public bool DefaultIsSelected { get; set; }
        public bool CustomerTap { get; set; }
    }
}
