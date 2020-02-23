using System;
using System.Linq;
using nopCommerceMobile.Extensions;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;
using nopCommerceMobile.ViewModels.Navigation;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.Catalog
{
    public abstract class CategoryNavigationViewXaml : ModelBoundContentView<CategoryNavigationViewModel> { }
    public partial class CategoryNavigationView : CategoryNavigationViewXaml
    {
        public CategoryNavigationView()
        {
            InitializeComponent();

            if (BindingContext == null)
                BindingContext = new CategoryNavigationViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            if (AppNavigationPage.Vm.SelectedNavigationPage != NavigationPageEnum.CategoryNavigation)
                return;

            await ViewModel.InitializeAsync();
        }

        private async void Category_OnClick(object sender, EventArgs e)
        {
            var stackLayout = (StackLayout)sender;
            var selectedCategoryNavigation = (CategorySimpleModel)stackLayout.BindingContext;

            if (selectedCategoryNavigation.SubCategories.Any())
            {
                var categoryNavigationModel = new CategoryNavigationModel()
                {
                    Categories = selectedCategoryNavigation.SubCategories
                };

                var categoryPage = new CategoryNavigationPage()
                {
                    BindingContext = new CategoryNavigationViewModel()
                    {
                        Categories = categoryNavigationModel.Categories.ToObservableCollection(),
                        Title = selectedCategoryNavigation.Name,
                    }
                };

                await Navigation.PushAsync(categoryPage);
            }
            else
            {
                var category = await ViewModel.GetCategoryByIdAsync(selectedCategoryNavigation.Id);
                var categoryPage = new CategoryPage()
                {
                    BindingContext = new CategoryViewModel()
                    {
                        CategoryId = selectedCategoryNavigation.Id,
                        GetProductsByCategoryId = true,
                        Title = selectedCategoryNavigation.Name,
                        Category = category
                    }
                };

                await Navigation.PushAsync(categoryPage);
            }
        }
    }
}