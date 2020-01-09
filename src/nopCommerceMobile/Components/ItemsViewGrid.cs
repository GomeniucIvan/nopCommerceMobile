using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace nopCommerceMobile.Components
{
	public class ItemsViewGrid : Grid
	{
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsViewGrid), null, BindingMode.Default);
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(IEnumerable<object>), typeof(ItemsViewGrid), null, BindingMode.Default, null, OnSourceChanged);

        public IEnumerable<object> Source
		{
			get => (IEnumerable<object>)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public DataTemplate ItemTemplate
		{
			get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        private void ReloadSource()
		{
            Children.Clear();

			IEnumerable<object> itemsSource = Source;
            if (itemsSource == null) return;
            var column = 0;
            var row = 0;
            foreach (var item in itemsSource)
            {
                Children.Add(CreateItem(item, column, row));
                column++;
                if (column < 2) continue;
                column = 0;
                row++;
            }
        }

		private View CreateItem(object element, int column, int row)
		{
            DataTemplateSelector dataTemplateSelector = ItemTemplate as DataTemplateSelector;
            var obj = ((dataTemplateSelector == null) ? ItemTemplate.CreateContent() : dataTemplateSelector.SelectTemplate(element, null).CreateContent());

            if (!(obj is View view)) return ((ViewCell) obj).View;
            view.BindingContext = element;
            SetRow(view, row);
            SetColumn(view, 2 - column - 1);
            return view;
        }

		private static void OnSourceChanged(BindableObject bindableObject, object oldValue, object newValue)
		{
			var itemsViewGrid = (ItemsViewGrid)bindableObject;
            if (oldValue is INotifyCollectionChanged notifyCollectionChanged)
			{
				notifyCollectionChanged.CollectionChanged -= itemsViewGrid.OnObservableCollectionChanged;
			}

            if (newValue is INotifyCollectionChanged notifyCollectionChanged2)
			{
				notifyCollectionChanged2.CollectionChanged += itemsViewGrid.OnObservableCollectionChanged;
			}
			itemsViewGrid.ReloadSource();
		}

        private void OnObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			ReloadSource();
		}
    }
}
