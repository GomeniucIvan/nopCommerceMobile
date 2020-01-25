using System.Collections.Generic;
using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Media;

namespace nopCommerceMobile.Models.Catalog
{
    public class CategoryModel : BaseModel
    {
        public CategoryModel()
        {
            SubCategories = new List<SubCategoryModel>();
            Products = new List<ProductModel>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        private PictureModel _pictureModel;
        public PictureModel PictureModel
        {
            get => _pictureModel;
            set
            {
                _pictureModel = value;
                RaisePropertyChanged(() => PictureModel);
            }
        }

        private IList<SubCategoryModel> _subCategories;
        public IList<SubCategoryModel> SubCategories
        {
            get => _subCategories;
            set
            {
                _subCategories = value;
                RaisePropertyChanged(()=> SubCategories);
            }
        }

        private IList<ProductModel> _products;
        public IList<ProductModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                RaisePropertyChanged(() => Products);
            }
        }
    }
}
