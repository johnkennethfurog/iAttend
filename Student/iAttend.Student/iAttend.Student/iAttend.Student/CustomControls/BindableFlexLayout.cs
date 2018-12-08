﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace iAttend.Student.CustomControls
{
    class BindableFlexLayout : FlexLayout
    {
        public static readonly BindableProperty ItemTemplateProperty =
    BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(BindableFlexLayout), null);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindableFlexLayout), propertyChanging: ItemsSourceChanging);

        private static void ItemsSourceChanging(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != null && oldValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)oldValue).CollectionChanged -= ((BindableFlexLayout)bindable).OnCollectionChanged;
            }

            if (newValue != null && newValue is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged)newValue).CollectionChanged += ((BindableFlexLayout)bindable).OnCollectionChanged;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    if (ApplyImageFormat && Children.Count >= 6)
                        break;

                    AddChild(item);
                }


            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {

                if (ApplyImageFormat && e.OldStartingIndex > 5)
                    return;

                Children.RemoveAt(e.OldStartingIndex);

            }

        }


        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public bool ApplyImageFormat { get; set; }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(ItemsSource))
                Populate();
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            Populate();
        }

        private void Populate()
        {
            if (ItemsSource == null)
                return;

            Children.Clear();
            foreach (var item in ItemsSource)
            {
                AddChild(item);
            }
        }

        void AddChild(object item)
        {
            var content = ItemTemplate.CreateContent();

            if (content is ViewCell viewCell)
            {
                Children.Add(viewCell.View);
                viewCell.BindingContext = item;
            }

        }
    }
}
