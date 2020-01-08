using nopCommerceMobile.Models.Base;

namespace nopCommerceMobile.Models.Media
{
    public class PictureModel : BaseModel
    {
        public string ImageUrl { get; set; }

        public string ThumbImageUrl { get; set; }

        public string FullSizeImageUrl { get; set; }

        public string Title { get; set; }

        public string AlternateText { get; set; }
    }
}
