using nopCommerceMobile.ViewModels.Base;

namespace nopCommerceMobile.Models.Base
{
    /// <summary>
    /// Base class for model
    /// </summary>
    public class BaseModel : ExtendedBindableObject
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public int Id { get; set; }
    }
}
