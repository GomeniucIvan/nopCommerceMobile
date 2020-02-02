using System;
using System.Linq;
using nopCommerceMobile.Helpers;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;
using Xamarin.Forms;

namespace nopCommerceMobile.Views.Catalog
{
    public abstract class CategoriesPageXaml : ModelBoundContentPage<CategoriesViewModel> { }
    public partial class CategoriesPage : CategoriesPageXaml
    {
        public static CategoriesPage Page;
        public CategoriesPage()
        {
            InitializeComponent();
            Page = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshToolbarItems();
        }

        private async void Category_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var selectedCategory = (SubCategoryModel)view.BindingContext;
            var category = await ViewModel.GetCategoryByIdAsync(selectedCategory.Id);

            if (category.SubCategories.Any())
            {
                var categoryPage = new CategoriesPage()
                {
                    Title = category.Name,
                    BindingContext = new CategoriesViewModel()
                    {
                        Category = category,
                        Title = category.Name,
                    }
                };

                await Navigation.PushAsync(categoryPage);
            }
            else
            {
                if (category.Products.Any())
                {
                    var categoryPage = new CategoryPage()
                    {
                        Title = category.Name,
                        BindingContext = new CategoryViewModel()
                        {
                            Category = category,
                            Title = category.Name,
                        }
                    };

                    await Navigation.PushAsync(categoryPage);
                }
                else
                {
                    //add popup notification ToDo
                }
            }
        }

        public void RefreshToolbarItems()
        {
            ToolbarItems.Children.Clear();
            ToolbarItems.Children.Add(new ShoppingCart());
            ToolbarItems.Children.Add(new ToolbarMenuButton());
        }
    }
}