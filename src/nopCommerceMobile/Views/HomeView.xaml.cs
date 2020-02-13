using System;
using System.Linq;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.Models.News;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;
using nopCommerceMobile.ViewModels.Navigation;
using nopCommerceMobile.ViewModels.News;
using nopCommerceMobile.Views.Catalog;
using nopCommerceMobile.Views.News;
using Xamarin.Forms;

namespace nopCommerceMobile.Views
{
    public abstract class HomeViewXaml : ModelBoundContentView<HomeBaseViewModel> { }
    public partial class HomeView : HomeViewXaml
    {
        public static HomeView View;
        public HomeView()
        {
            InitializeComponent();
            View = this;
            if (BindingContext == null)
                BindingContext = new HomeBaseViewModel();
        }

        protected override async void OnParentSet()
        {
            base.OnParentSet();
            if (AppNavigationPage.Vm.SelectedNavigationPage != NavigationPageEnum.Home)
                return;

            await ViewModel.InitializeAsync();
        }

        private async void Category_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var selectedCategory = (CategoryModel)view.BindingContext;
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

        private async void Product_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var selectedProduct = (ProductModel)view.BindingContext;

            var productPage = new ProductPage()
            {
                BindingContext = new ProductViewModel()
                {
                    ProductId = selectedProduct.Id
                }
            };

            await Navigation.PushAsync(productPage);
        }

        private async void News_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var selectedNewsItem = (NewsItemModel)view.BindingContext;

            var newsPage = new NewsPage()
            {
                BindingContext = new NewsViewModel()
                {
                    NewsItem = selectedNewsItem,
                    Title = selectedNewsItem.Title
                }
            };

            await Navigation.PushAsync(newsPage);
        }

        private void Slider_OnClick(object sender, EventArgs e)
        {

        }
    }

}