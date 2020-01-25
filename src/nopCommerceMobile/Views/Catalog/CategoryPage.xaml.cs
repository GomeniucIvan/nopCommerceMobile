using System;
using nopCommerceMobile.ViewModels.Base;
using nopCommerceMobile.ViewModels.Catalog;

namespace nopCommerceMobile.Views.Catalog
{
    public abstract class CategoryPageXaml : ModelBoundContentPage<CategoryViewModel> { }
    public partial class CategoryPage : CategoryPageXaml
    {
        public CategoryPage()
        {
            InitializeComponent();
        }

        private void Product_OnClick(object sender, EventArgs e)
        {
        }
    }
}