using nopCommerceMobile.Models.Base;

namespace nopCommerceMobile.Models.Customer
{
    public class LoginModel : BaseModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public bool UsernamesEnabled { get; set; }
        public UserRegistrationType RegistrationType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private bool _rememberMe;
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                RaisePropertyChanged(() => RememberMe);
            }
        }
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
