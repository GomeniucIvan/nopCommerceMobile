using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Media;

namespace nopCommerceMobile.Models.Catalog
{
    public class SubCategoryModel : BaseModel
    {
        public SubCategoryModel()
        {
            PictureModel = new PictureModel();
        }

        public string Name { get; set; }

        public string SeName { get; set; }

        public string Description { get; set; }

        public PictureModel PictureModel { get; set; }
    }
}
