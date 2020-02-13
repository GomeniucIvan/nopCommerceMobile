using System;
using System.Linq;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using nopCommerceMobile.Components;
using nopCommerceMobile.Models.Catalog;
using nopCommerceMobile.ViewModels;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;
using nopCommerceMobile.Views.Catalog;
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

        private async void Product_OnClick(object sender, EventArgs e)
        {
            var view = (View)sender;
            var selectedProduct = (ProductModel)view.BindingContext;

            var productPage = new ProductPage()
            {
                Title = selectedProduct.Name,
                BindingContext = new ProductViewModel()
                {
                    ProductId = selectedProduct.Id
                }
            };

            await Navigation.PushAsync(productPage);
        }

        private void News_OnClick(object sender, EventArgs e)
        {
           
        }

        private void Slider_OnClick(object sender, EventArgs e)
        {

        }
    }

}