using System;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using nopCommerceMobile.Models.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nopCommerceMobile.Extensions
{
    [ContentProperty("Key")]
    public class TranslateExtension : IMarkupExtension<Binding>, IMarkupExtension
    {
        public string Key { get; set; }
        public static string DefaultCultureName = "en-US";

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateBinding(serviceProvider);
        }

        Binding IMarkupExtension<Binding>.ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateBinding(serviceProvider);
        }

        private Binding CreateBinding(IServiceProvider serviceProvider)
        {
            if (Key == null)
                return null;

            var cultureName = DefaultCultureName;

            if (App.CurrentCostumer != null && App.CustomerAppCulture.IsNullOrEmpty())
            {
                cultureName = App.CustomerAppCulture;
            }

            var resourceManager = new ResourceManager(typeof(LocaleResourceModel));
            BindingSource bindingSource = new BindingSource(resourceManager, Key, cultureName);

            Binding binding = new Binding
            {
                Source = bindingSource,
                Path = "Text"
            };

            return binding;
        }

        private class BindingSource : INotifyPropertyChanged
        {
            private readonly ResourceManager _manager;
            private readonly string _key;
            private readonly string _currentCulture;

            public event PropertyChangedEventHandler PropertyChanged;

            public BindingSource(ResourceManager manager, string key, string currentCulture)
            {
                _manager = manager;
                _key = key;
                _currentCulture = currentCulture;
            }

            //return _manager.GetString(_key) ?? _key;
            public string Text => GetStringValue(_key, _currentCulture).ToString();

            private string GetStringValue(string key, string languageCulture)
            {
                var result = key;

                if (App.LocaleResources != null && App.LocaleResources.Any())
                {
                   var localeResource = App.LocaleResources.FirstOrDefault(v => v.ResourceName.ToLower() == key.ToLower());
                   result = localeResource != null ? localeResource.ResourceValue : key;
                }

                return result;
            }
        }
    }
}
