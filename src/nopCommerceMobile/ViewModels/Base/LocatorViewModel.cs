using nopCommerceMobile.Services;
using nopCommerceMobile.Services.Catalog;
using nopCommerceMobile.Services.Customer;
using nopCommerceMobile.Services.Dialog;
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

            // View models - by default, TinyIoC will register concrete classes as multi-instance.
            //_container.Register<HomeViewModel>();

            // Services - by default, TinyIoC will register interface registrations as singletons.
            _container.Register<INavigationService, NavigationService>();
            _container.Register<IDialogService, DialogService>();
            _container.Register<IRequestProvider, RequestProvider>();
            _container.Register<ISettingsService, SettingsService>();
            _container.Register<ICatalogService, CatalogService>();
            _container.Register<ICustomerService, CustomerService>();
            _container.Register<ITopicService, TopicService>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }
    }
}