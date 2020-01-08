using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using nopCommerceMobile.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nopCommerceMobile.Extensions
{
    [ContentProperty("Key")]
    public class TranslateExtension : IMarkupExtension<Binding>, IMarkupExtension
    {
        public string Key { get; set; }
        public string DefaultCultureName = "en-US";

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

            //TODO get language culture from claim
            //Based on web settings display resource in case if not exists in current culture


            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            var manager = new ResourceManager(typeof(AppResource));

            BindingSource bindingSource = new BindingSource(manager, Key);

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

            

            public string Text
            {
                get
                {
                    try
                    {
                       return _manager.GetString(_key) ?? _key;
                    }
                    catch (Exception e)
                    {
                        //TODO add custom getstring in case if key not exists
                        return _key;
                    }
                }
            }
        }
    }
}
