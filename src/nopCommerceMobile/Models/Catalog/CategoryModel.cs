using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Media;

namespace nopCommerceMobile.Models.Catalog
{
    public class CategoryModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public string SeName { get; set; }

        public PictureModel PictureModel { get; set; }
    }
}
