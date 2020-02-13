using System.Collections.Generic;
using System.Linq;
using nopCommerceMobile.Components;
using nopCommerceMobile.Models.Localization;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Localization;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Common;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.Common
{
    public abstract class SelectListPageXaml : ModelBoundContentPage<SelectListViewModel> { }
    public partial class SelectListPage : SelectListPageXaml
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public SelectListPage()
        {
            InitializeComponent();

            if (_customerService == null)
                _customerService = LocatorViewModel.Resolve<ICustomerService>();

            if (_localizationService == null)
                _localizationService = LocatorViewModel.Resolve<ILocalizationService>();
        }

        #endregion


        private void Item_OnChange(object sender, ElementEventArgs e)
        {

            var checkbox = (AppCheckBox)sender;
            var selectedItem = ((SelectListItemViewModel)checkbox.BindingContext);
            if (checkbox.IsChecked == selectedItem.DefaultIsSelected && !selectedItem.CustomerTap)
            {
                return;
            }

            if (ViewModel.SelectListPage == SelectListPageEnum.Languages)
            {
                App.CurrentCostumerSettings.LanguageId = selectedItem.Id;
                _customerService.CreateOrUpdateCustomerSettings(true);
                App.LocaleResources = new List<LocaleResourceModel>();
                _localizationService.CreateOrUpdateLocales(true);
                App.SetMainPage();
            }
        }
    }
}