using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace nopCommerceMobile.Components
{
	public class ItemsViewGrid : Grid
	{
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsViewGrid), null, BindingMode.Default);
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(IEnumerable<object>), typeof(ItemsViewGrid), null, BindingMode.Default, null, OnSourceChanged);
        public static readonly BindableProperty IsProductProperty = BindableProperty.Create(nameof(IsProduct), typeof(bool), typeof(ItemsViewGrid), false, BindingMode.Default);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(TapGestureRecognizer), null);

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

        public bool IsProduct
        {
            get => (bool)GetValue(IsProductProperty);
            set => SetValue(IsProductProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public event EventHandler<ItemTappedEventArgs> ItemTapped;

        private void ReloadSource()
		{
            Children.Clear();

			var itemsSource = Source;
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
            var obj = ItemTemplate.CreateContent();
            var view = obj as View;
            view.BindingContext = element;
            SetRow(view, row);
            SetColumn(view, 2 - column - 1);

            //Tapped?.Invoke(view, new TappedEventArgs(CommandParameter));

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

    public class ItemsViewList : StackLayout
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(ItemsViewList), null, BindingMode.Default);
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(nameof(Source), typeof(IEnumerable<object>), typeof(ItemsViewList), null, BindingMode.Default, null, OnSourceChanged);
        public static readonly BindableProperty IsProductProperty = BindableProperty.Create(nameof(IsProduct), typeof(bool), typeof(ItemsViewList), false, BindingMode.Default);

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

        public bool IsProduct
        {
            get => (bool)GetValue(IsProductProperty);
            set => SetValue(IsProductProperty, value);
        }

        private void ReloadSource()
        {
            var itemsSource = Source;
            if (itemsSource == null) return;
            foreach (var item in itemsSource)
            {
                Children.Add(CreateItem(item));
            }
        }

        private View CreateItem(object element)
        {
            var obj = ItemTemplate.CreateContent();
            var view = obj as View;
            view.BindingContext = element;

            return view;
        }

        private static void OnSourceChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            var itemsViewGrid = (ItemsViewList)bindableObject;
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
