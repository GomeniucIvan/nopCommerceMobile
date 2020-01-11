using nopCommerceMobile.Models.Base;

namespace nopCommerceMobile.Models.Customer
{
    class LoginModel : BaseModel
    {
        public string Email { get; set; }
        public bool UsernamesEnabled { get; set; }
        public UserRegistrationType RegistrationType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    #region Nested classes

    public enum UserRegistrationType
    {
        /// <summary>
        /// Standard account creation
        /// </summary>
        Standard = 1,

        /// <summary>
        /// Email validation is required after registration
        /// </summary>
        EmailValidation = 2,

        /// <summary>
        /// A customer should be approved by administrator
        /// </summary>
        AdminApproval = 3,

        /// <summary>
        /// Registration is disabled
        /// </summary>
        Disabled = 4
    }

    #endregion
}
