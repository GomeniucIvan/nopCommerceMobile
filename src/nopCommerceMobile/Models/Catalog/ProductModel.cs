using System;
using System.Collections.Generic;
using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Media;

namespace nopCommerceMobile.Models.Catalog
{
    public class ProductModel : BaseModel
    {
        public ProductModel()
        {
            SpecificationAttributeModels = new List<ProductSpecificationModel>();
        }

        public string Name { get; set; }

        //price
        public ProductPriceModel ProductPrice { get; set; }
        //picture
        public PictureModel DefaultPictureModel { get; set; }
        //specification attributes
        public IList<ProductSpecificationModel> SpecificationAttributeModels { get; set; }
    }

    #region Nested Classes

    public class ProductPriceModel : BaseModel
    {
        public string OldPrice { get; set; }
        public string Price { get; set; }
        public decimal PriceValue { get; set; }

        public bool DisableBuyButton { get; set; }
        public bool DisableWishlistButton { get; set; }
        public bool DisableAddToCompareListButton { get; set; }

        public bool AvailableForPreOrder { get; set; }
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

        public bool IsRental { get; set; }

        public bool ForceRedirectionAfterAddingToCart { get; set; }

        public bool DisplayTaxShippingInfo { get; set; }
    }

    public class ProductSpecificationModel : BaseModel
    {
        public int SpecificationAttributeId { get; set; }

        public string SpecificationAttributeName { get; set; }

        public string ValueRaw { get; set; }

        public string ColorSquaresRgb { get; set; }

        public int AttributeTypeId { get; set; }
    }

    #endregion
}
