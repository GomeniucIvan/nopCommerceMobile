using System.Collections.Generic;
using nopCommerceMobile.Models.Base;
using nopCommerceMobile.Models.Catalog;

namespace nopCommerceMobile.Models.Common
{
    public class AddressAttributeModel : BaseModel
    {
        public AddressAttributeModel()
        {
            Values = new List<AddressAttributeValueModel>();
        }

        public string Name { get; set; }

        public bool IsRequired { get; set; }

        /// <summary>
        /// Default value for textboxes
        /// </summary>
        public string DefaultValue { get; set; }

        public AttributeControlType AttributeControlType { get; set; }

        public IList<AddressAttributeValueModel> Values { get; set; }
    }

    public class AddressAttributeValueModel : BaseModel
    {
        public string Name { get; set; }

        public bool IsPreSelected { get; set; }
    }
}
