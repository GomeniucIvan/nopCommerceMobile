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

            var resourceManager = new ResourceManager(typeof(LocaleResourceModel));
            BindingSource bindingSource = new BindingSource(resourceManager, Key);

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
            public event PropertyChangedEventHandler PropertyChanged;

            public BindingSource(ResourceManager manager, string key)
            {
                _manager = manager;
                _key = key;
            }

            public string Text => GetStringValue(_key);

            private string GetStringValue(string key)
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

        public static string Translate(string key)
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
