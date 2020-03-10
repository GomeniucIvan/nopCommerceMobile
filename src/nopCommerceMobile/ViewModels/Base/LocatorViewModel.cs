using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.Services.Common;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Dependency;
using nopCommerceMobile.Services.Localization;
using nopCommerceMobile.Services.Navigation;
using nopCommerceMobile.Services.RequestProvider;
using nopCommerceMobile.Services.Settings;
using nopCommerceMobile.Services.Topic;
using TinyIoC;

namespace nopCommerceMobile.ViewModels.Base
{
    public static class LocatorViewModel
    {
        private static TinyIoCContainer _container;

        static LocatorViewModel()
        {
            _container = new TinyIoCContainer();

            // Services - by default, TinyIoC will register interface registrations as singletons.
            _container.Register<IDependencyService, DependencyService>();
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IRequestProvider, RequestProvider>();
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<ICatalogService, CatalogService>();
            _container.Register<ICustomerService, CustomerService>();
            _container.Register<ITopicService, TopicService>();
            _container.Register<ILocalizationService, LocalizationService>();
            _container.Register<IProductService, ProductService>();
            _container.Register<IShoppingCartService, ShoppingCartService>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}